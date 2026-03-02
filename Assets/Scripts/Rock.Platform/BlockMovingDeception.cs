using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class BlockMovingDeception: MonoBehaviour
{
    [SerializeField] Transform block;
    [Header("BlockPop")]
    [SerializeField] Transform blockPop;
    [SerializeField] float speedForPop;
    [Header("BlockMove")]
    [SerializeField] Transform blockMove;
    [SerializeField] float speedForMove;
    [Header("BlockHidden")]
    [SerializeField] Transform blockHidden;
    [SerializeField] float speedForHidden;


    bool fired = false;
    enum Phase { Idle, Pop, Move, Hidden}
    Phase phase = Phase.Idle;
    const float EPS = 0.01f;


    private void Update()
    {

        switch (phase)
        {
            case Phase.Pop:
                DoPop(); 
                break;
            case Phase.Move:
                DoMove();
                break;
            case Phase.Hidden:
                DoHidden();
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Player")) return;
        if (fired) return;

        fired = true;
        phase = Phase.Pop;
    }

    void DoPop()
    {
        float goalY = blockPop.position.y;
        Vector3 goal = new Vector3 (block.position.x, goalY, block.position.z);
        block.position = Vector3.MoveTowards(block.position, goal, speedForPop * Time.deltaTime);

        if (Mathf.Abs(block.position.y - goalY) <= EPS)
        {
            block.position = goal;   // жёсткая фиксация
            phase = Phase.Move;      // ← ВАЖНО
        }

    }

    void DoMove()
    {
        float goalX = blockMove.position.x;
        Vector3 goal = new Vector3(goalX, block.position.y, block.position.z);
        block.position = Vector3.MoveTowards(block.position, goal, speedForMove * Time.deltaTime);

        if (Mathf.Abs(block.position.x - goalX) <= EPS)
        {
            block.position = goal;
            phase = Phase.Hidden;
        }
    }

    void DoHidden()
    {
        float goalY = blockHidden.position.y;
        Vector3 goal = new Vector3(block.position.x, goalY, block.position.z);
        block.position = Vector3.MoveTowards(block.position, goal, speedForHidden * Time.deltaTime);

        if(Mathf.Abs(block.position.y - goalY) <= EPS)
        {
            block.position = goal;
            phase = Phase.Idle;
        }
    }

}
