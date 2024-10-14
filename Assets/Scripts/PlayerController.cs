using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 m_MoveVector;
    [SerializeField] private float m_MovementSpeed;
    private Rigidbody2D m_Rigidbody2D;

    #region Initialization

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    #endregion
    #region Input Reading

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }
    #endregion
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = new Vector2(m_MoveVector.x, m_MoveVector.y).normalized;
        if (direction.magnitude > 0.1f)
        {
            print(direction);
            m_Rigidbody2D.position += direction * m_MovementSpeed;
        }
    }
}
