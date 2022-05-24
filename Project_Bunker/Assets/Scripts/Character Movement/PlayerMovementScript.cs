using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundcheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public bool Active = true;
    Vector3 velocity;
    bool isGrounded;

 
    public void SetActive(bool newBool)
    {
        Active = newBool;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Active == true)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Debug.isDebugBuild)
            {
                Debug.Log($"X Move Input {x}");
                Debug.Log($"Z Move Input {z}");
            }
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity);

    }
}






