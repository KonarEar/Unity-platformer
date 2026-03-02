using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDeception : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] private string text;

    private bool once = true;
    [SerializeField] bool needToRemove;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (once)
        {
            myText.text = text;
            once = false;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(needToRemove && myText != null) Destroy(myText.gameObject);
    }

}
