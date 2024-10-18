using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] Image img_key1;
    [SerializeField] Image img_key2;

    int compteur = 0;

    public bool CanActivateDoor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key") && compteur != 2)
        {
            if (compteur == 0)
            {
                compteur += 1;

                img_key1.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                img_key1.color = collision.gameObject.GetComponent<SpriteRenderer>().color;

                collision.gameObject.SetActive(false);
            }
            else if (compteur == 1)
            {
                compteur += 1;

                img_key2.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                img_key2.color = collision.gameObject.GetComponent<SpriteRenderer>().color;

                collision.gameObject.SetActive(false);
                CanActivateDoor = true;
            }
        }
    }
}
