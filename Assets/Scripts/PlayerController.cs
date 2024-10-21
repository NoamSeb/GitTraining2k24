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
    [SerializeField] private Sprite m_PlayerSpriteToTop;
    [SerializeField] private Sprite m_PlayerSpriteToBottom;
    [SerializeField] private Sprite m_PlayerSpriteToSide;
    
    [Header("Extern Object : ")]
    [SerializeField] private GameObject LampPrefabs;
    [SerializeField] private GameObject RavenDialog;
    [SerializeField] private AudioClip RavenSound;
    [SerializeField] private AudioClip LampSound;
    
    private Vector2 m_MoveVector;
    private Rigidbody2D m_Rigidbody2D;
    private AudioSource m_audioSource;
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField] int LampCounter = 10;
    
    [SerializeField] private TextMeshProUGUI lampCountText;

    public bool OnTriggerBarrel = false;
    public InteractBarrel currentBarrel;

    public bool isPause = false;
    [SerializeField] PauseMenu menuPause;

    #region Initialization

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
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


    public void ReadInteractPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPause)
            {
                print("kakou kakou");
                menuPause.Resume();
                isPause = false;
            }
            else if (!isPause)
            {
                print("Resume");
                menuPause.Pause();
                isPause = true;
            }
        }
    }


    #endregion

    void Move()
    {
        Vector2 direction = new Vector2(m_MoveVector.x, m_MoveVector.y).normalized;
        
        if (direction.magnitude > 0.1f){
            if (direction.y >= 0.1f)
            {
                m_SpriteRenderer.sprite = m_PlayerSpriteToTop;
            }else if (direction.y <= -0.1f)
            {
                m_SpriteRenderer.sprite = m_PlayerSpriteToBottom;
            }

            if (direction.x >= 0.1f)
            {
                m_SpriteRenderer.sprite = m_PlayerSpriteToSide;
                m_SpriteRenderer.flipX = false;
            }else if (direction.x <= -0.1f)
            {
                m_SpriteRenderer.sprite = m_PlayerSpriteToSide;
                m_SpriteRenderer.flipX = true;
            }
            m_Rigidbody2D.position += direction * m_MovementSpeed;
        }
    }

    void CreateLamp()
    {
        if (LampPrefabs != null && LampCounter > 0)
        {
            Vector2 lampPosition = new Vector2(transform.position.x, transform.position.y + 0.2f);
            m_audioSource.PlayOneShot(LampSound, 1.0f);
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
