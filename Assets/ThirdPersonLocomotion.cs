using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonLocomotion : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    public Transform cam;

    public Animator animator;
 

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalMovement, 0f, verticalMovement).normalized; //Stops diagonal movement from making the player go faster;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //Smooth out the player's rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 

            //Makes the movement frame rate independent
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            animator.SetBool("IsMoving", true);

            Debug.Log(animator.GetBool("IsMoving"));
        }

    }
}
