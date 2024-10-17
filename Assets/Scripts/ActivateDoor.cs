using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] int speed = 3;

    bool CanMove = false;

    [SerializeField] Timer timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Key>().CanActivateDoor)
        {
            CanMove = true;
            timer.finJeu = true;
            //play animation ouverture ?
        }
    }

    private void Update()
    {
        if (CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 3.5f, 0), speed * Time.deltaTime);
        }
    }
}
