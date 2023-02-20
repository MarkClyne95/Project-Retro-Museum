using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonLocomotion : MonoBehaviour
{

    private GameManager gm;

    public CharacterController controller;

    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    public Transform cam;

    private Animator animator;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [SerializeField] private Vector3 velocity;
    [SerializeField] public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void Awake()
    {
        //gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        gravity = -20f;
        groundMask = LayerMask.GetMask("Ground");

        //transform.position = gm.playerStart.transform.position;
        //Invoke("SetStartPos", 0.1f);
    }

    public void SetPlayerPos()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        transform.position = gm.lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        //----------------Jumping Functionality----------------//

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("GROUNDED");

        if(isGrounded && velocity.y <= 0)
        {
            Debug.Log("DISABLE JUMPING");
            velocity.y = -7f;
            animator.SetBool("IsJumping", false);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("JUMPING");
            animator.SetBool("IsJumping", true);
            Invoke("Jump", 0.1f);
        }

        //Control Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //----------------Walking Functionality----------------//

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

        if(direction.magnitude <= 0f)
        {
            animator.SetBool("IsMoving", false);

            Debug.Log(animator.GetBool("IsMoving"));
        }

    }

    public void Jump()
    {
        isGrounded = false;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        Invoke("resetJumpHeight", 0.3f);
    }

    //public void untranstitionJump()
    //{
    //    animator.SetBool("IsJumping", false);
    //}

    private void resetJumpHeight()
    {
        jumpHeight = 3f;
    }

    public void plummet()
    {
        groundMask = LayerMask.GetMask("Nothing");
    }
}
