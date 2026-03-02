using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private Animator anim;
    private bool isOpened = false;

    public GameObject AfterGamePanel;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) anim.SetTrigger("Open");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) anim.SetTrigger("Closed");
    }


    public void ShowAfterGameMenu()
    {
        AfterGamePanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
