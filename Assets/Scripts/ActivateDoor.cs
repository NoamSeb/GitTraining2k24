using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] int speed = 3;

    bool CanMove = false;

    [SerializeField] Timer timer;

    [SerializeField] Sprite spriteGateOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Key>().CanActivateDoor)
        {
            CanMove = true;
            timer.finJeu = true;
            
            this.GetComponent<Animator>().Play("gate");
            this.GetComponent<SpriteRenderer>().sprite = spriteGateOpen;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (CanMove)
        {
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -34, 0), speed * Time.deltaTime);
        }
    }
}
