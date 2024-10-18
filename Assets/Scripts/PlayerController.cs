using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attribute : ")]
    [SerializeField] private float m_MovementSpeed;
    
    [Header("Extern Object : ")]
    [SerializeField] private GameObject LampPrefabs;
    [SerializeField] private GameObject RavenDialog;
    [SerializeField] private AudioClip RavenSound;
    
    private Vector2 m_MoveVector;
    private Rigidbody2D m_Rigidbody2D;
    private AudioSource m_audioSource;
    [SerializeField] int LampCounter = 10;
    
    [SerializeField] private TextMeshProUGUI lampCountText;

    public bool OnTriggerBarrel = false;
    public InteractBarrel currentBarrel;

    #region Initialization

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
    }
    #endregion
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (lampCountText != null)
        {
            lampCountText.text = LampCounter.ToString();
        }
    }

    
    #region Input Reading

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }

    public void ReadCreateLampInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CreateLamp();
        }
    }


    public void ReadInteractBarrel(InputAction.CallbackContext context)
    {
        if (context.performed && OnTriggerBarrel)
        {
            if (currentBarrel != null)
            {
                currentBarrel.OpenBarrel();
            }
        }
    }


    #endregion
   
    void Move()
    {
        Vector2 direction = new Vector2(m_MoveVector.x, m_MoveVector.y).normalized;
        
        if (direction.magnitude > 0.1f){
            m_Rigidbody2D.position += direction * m_MovementSpeed;
        }
    }

    void CreateLamp()
    {
        if (LampPrefabs != null && LampCounter > 0)
        {
            Vector2 lampPosition = new Vector2(transform.position.x, transform.position.y);

            GameObject newLamp = Instantiate(LampPrefabs, lampPosition, Quaternion.identity);
            LampCounter--;
            print(LampCounter);
        }
    }


    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.gameObject.tag == "Raven")
        {
            RavenDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collidedObject){
        if (collidedObject.gameObject.tag == "Raven")
        {
            RavenDialog.SetActive(false);
            m_audioSource.PlayOneShot(RavenSound, 1.0f);
        }
    }
}
