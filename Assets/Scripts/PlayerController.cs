using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attribute : ")]
    [SerializeField] private float m_MovementSpeed;
    
    [Header("Extern Object : ")]
    [SerializeField] private GameObject LampPrefabs;
    [SerializeField] private int m_LampCount = 10;
    
    private Vector2 m_MoveVector;
    private Rigidbody2D m_Rigidbody2D;

    #region Initialization

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    #endregion
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
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
        print("Create lamp");
        if (LampPrefabs != null && m_LampCount > 0)
        {
            Vector2 lampPosition = new Vector2(transform.position.x, transform.position.y);

            GameObject newLamp = Instantiate(LampPrefabs, lampPosition, Quaternion.identity);
        }
    }
}
