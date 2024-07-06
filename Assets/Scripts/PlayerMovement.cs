using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private savedPosition lastSavedPosition;
    public float gravity = 9.8f;
    public float jumpForce;
    public float speed;

    private Vector3 _moveVector;
    private float _fallVelocity = 0;


    public bool doublejump;
    public float jumpCount;

    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);

        _fallVelocity += gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);

        if(_characterController.isGrounded)
        {
            _fallVelocity = 0;
        }
        if (transform.position.y <= -10)
        {
           
            transform.position = lastSavedPosition.position + Vector3.down * 0.7f;
        }


    }

    void Update()
    {
        _moveVector = Vector3.zero;
        
        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
        }

        
        
        if (Input.GetKeyDown(KeyCode.Space) && doublejump == true)
        {               
           _fallVelocity = -jumpForce;
            jumpCount += 1;
        }
        
        if (jumpCount == 1)
        {
            doublejump = false;
        }
        

        if (_characterController.isGrounded)
        {
            doublejump = true;
            jumpCount = 0;
        }    
    }

}
