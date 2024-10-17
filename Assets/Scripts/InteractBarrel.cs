using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractBarrel : MonoBehaviour
{
    public bool jumpscare = false;
    public bool key = true;

    PlayerController playerController;
    PlayerInput playerInput;

    [SerializeField] GameObject keyPrefab;

    public GameObject imgJumpscare;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerInput = collision.gameObject.GetComponent<PlayerInput>();

            playerController.OnTriggerBarrel = true;
            playerController.currentBarrel = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.GetComponent<PlayerController>().OnTriggerBarrel = false;
        playerController.currentBarrel = null;
    }

    public void OpenBarrel()
    {
        if (jumpscare && imgJumpscare != null)
        {
            StartCoroutine(ActivateJumpscare());
        } else if (key && keyPrefab != null)
        {
            Vector2 keyPosition = new Vector2(transform.position.x, transform.position.y);

            GameObject newLamp = Instantiate(keyPrefab, keyPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator ActivateJumpscare()
    {
        imgJumpscare.SetActive(true);
        playerController.enabled = false;
        playerInput.enabled = false;
        yield return new WaitForSeconds(1);
        imgJumpscare.SetActive(false);
        playerController.enabled = true;
        playerInput.enabled = true;
        Destroy(this.gameObject);
    }
}
