using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image customImage;
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = false;
        }
    }
}
