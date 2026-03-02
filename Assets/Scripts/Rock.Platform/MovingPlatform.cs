using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingPlatform : MonoBehaviour
{
    // стартовые позиции платформ (сохраняем в Start)
    private Vector3 startPositionPlatform;
    private Vector3 startPositionInvisPlatform;

    [Header("References")]
    [SerializeField] Transform platform1; // та, на которой стоит игрок
    [SerializeField] Transform platform2; // невидимая/вторая
    [SerializeField] Transform pointA;    // точка A (маятник с платформы1)
    [SerializeField] Transform pointB;    // целевая позиция B (финал для платформы1, маятник для платформы2)

    [Header("Motion")]
    [SerializeField] float speed = 3f;
    [SerializeField] float arriveTolerance = 0.02f;

    [Header("Pause logic")]
    [SerializeField] bool needAPause = true;   // если true — докрутка A→старт, пауза, потом к B
    [SerializeField] float pauseDuration = 2f; // сколько стоим на старте
    private float pauseTimer = 0f;

    // общие флаги
    private bool isPlayerOnPlatform = false;

    // платформа1: маятник между стартом и A (когда игрока нет)
    private bool goingToA = true;

    // платформа2: маятник между стартом и B (когда нет сценария)
    private bool goingToB = true;

    // для режима "без паузы": сначала докрутить до A, потом сразу к B
    private bool finishedMovement = false;

    // для режима "с паузой": A -> старт -> пауза -> B
    private bool reachedA = false;   // доехали ли до A
    private bool finishedLoop = false; // завершили ли A -> старт
    private bool isPausing = false;    // сейчас стоим на паузе

    private void Start()
    {
        startPositionPlatform = platform1.position;
        startPositionInvisPlatform = platform2.position;
    }

    private void FixedUpdate()
    {
        if (!isPlayerOnPlatform)
        {
            // ----- ПЛАТФОРМА 1: обычный маятник старт <-> A -----
            Vector3 target1 = goingToA
                ? new Vector3(pointA.position.x, platform1.position.y, platform1.position.z)
                : new Vector3(startPositionPlatform.x, platform1.position.y, platform1.position.z);

            platform1.position = Vector3.MoveTowards(platform1.position, target1, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(platform1.position, target1) <= arriveTolerance)
                goingToA = !goingToA;
        }
        else if (!needAPause)
        {
            // ----- РЕЖИМ БЕЗ ПАУЗЫ: докрутить до A, потом к B -----
            if (!finishedMovement)
            {
                var toA = new Vector3(pointA.position.x, platform1.position.y, platform1.position.z);
                platform1.position = Vector3.MoveTowards(platform1.position, toA, speed * Time.fixedDeltaTime);

                if (Vector3.Distance(platform1.position, toA) <= arriveTolerance)
                    finishedMovement = true;
            }
            else
            {
                var toB = new Vector3(pointB.position.x, platform1.position.y, platform1.position.z);
                platform1.position = Vector3.MoveTowards(platform1.position, toB, speed * Time.fixedDeltaTime);
            }
        }
        else
        {
            // ----- РЕЖИМ С ПАУЗОЙ: A -> старт -> пауза -> B -----
            if (!finishedLoop)
            {
                // 1) сначала реально доезжаем до A
                if (!reachedA)
                {
                    Vector3 toA = new Vector3(pointA.position.x, platform1.position.y, platform1.position.z);
                    platform1.position = Vector3.MoveTowards(platform1.position, toA, speed * Time.fixedDeltaTime);

                    if (Vector3.Distance(platform1.position, toA) <= arriveTolerance)
                        reachedA = true; // теперь можно к старту
                }
                // 2) потом возвращаемся на старт и ставим паузу
                else
                {
                    Vector3 toStart = new Vector3(startPositionPlatform.x, platform1.position.y, platform1.position.z);
                    platform1.position = Vector3.MoveTowards(platform1.position, toStart, speed * Time.fixedDeltaTime);

                    // вторую платформу тоже тянем в её старт (один раз за кадр)
                    Vector3 toStart2 = new Vector3(startPositionInvisPlatform.x, platform2.position.y, platform2.position.z);
                    platform2.position = Vector3.MoveTowards(platform2.position, toStart2, speed * Time.fixedDeltaTime);

                    if (Vector3.Distance(platform1.position, toStart) <= arriveTolerance)
                    {
                        finishedLoop = true;     // A -> старт завершён
                        isPausing = true;        // начинаем паузу
                        pauseTimer = pauseDuration;
                    }
                }
            }
            else if (isPausing)
            {
                // 3) пауза
                pauseTimer -= Time.fixedDeltaTime;
                if (pauseTimer <= 0f)
                {
                    isPausing = false;

                    // после паузы скрываем вторую платформу
                    platform2.gameObject.SetActive(false);
                }
            }
            else
            {
                // 4) едем к B
                var toB = new Vector3(pointB.position.x, platform1.position.y, platform1.position.z);
                platform1.position = Vector3.MoveTowards(platform1.position, toB, speed * Time.fixedDeltaTime);
            }
        }

        // ----- ПЛАТФОРМА 2: маятник только когда нет сценария -----
        bool inSequence = isPlayerOnPlatform && needAPause && (!finishedLoop || isPausing);
        if (!inSequence && platform2.gameObject.activeSelf)
        {
            Vector3 target2 = goingToB
                ? new Vector3(pointB.position.x, platform2.position.y, platform2.position.z)
                : new Vector3(startPositionInvisPlatform.x, platform2.position.y, platform2.position.z);

            platform2.position = Vector3.MoveTowards(platform2.position, target2, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(platform2.position, target2) <= arriveTolerance)
                goingToB = !goingToB;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;

            // сбрасываем одноразовые флаги
            finishedMovement = false;

            reachedA = false;
            finishedLoop = false;
            isPausing = false;
            pauseTimer = 0f;

            // убедимся, что platform2 видима при старте сценария (если надо)
            if (!platform2.gameObject.activeSelf)
                platform2.gameObject.SetActive(true);

            // родим игрока к платформе1, чтобы его возило вместе
            other.transform.SetParent(platform1);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            other.transform.SetParent(null);

            // после ухода игрока можно вернуть platform2 (если ты её скрывал)
            if (!platform2.gameObject.activeSelf)
                platform2.gameObject.SetActive(true);
        }
    }
}
    
