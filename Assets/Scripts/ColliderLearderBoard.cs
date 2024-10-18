using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderLearderBoard : MonoBehaviour
{
    [SerializeField] Timer timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer.dansTriggerFinJeu = true;
            //collision.gameObject.GetComponent<PlayerInput>().enabled = false;
        }
    }
}
