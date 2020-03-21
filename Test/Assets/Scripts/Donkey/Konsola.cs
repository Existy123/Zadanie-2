using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Konsola : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag != "Ground")
        {
            Debug.Log("You win!");
        }
    }
}
