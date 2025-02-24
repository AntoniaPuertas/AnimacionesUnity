using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public new Transform camera;
    public float speed = 2;
    public float gravity = -9.8f;
    // Start is called before the first frame update
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;


        if (hor != 0 || ver != 0)
        {
            Vector3 foward = camera.forward;
            foward.y = 0;
            foward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = foward * ver + right * hor;


            direction.Normalize();

            movement = direction * speed  * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        }

        float jump = Input.GetAxis("Jump");
        if (jump != 0)
        {
            animator.SetTrigger("Jump");
        }

        movement.y += gravity * Time.deltaTime;
        characterController.Move(movement);
        animator.SetFloat("XSpeed", hor);
        animator.SetFloat("YSpeed", ver);


    }
}
