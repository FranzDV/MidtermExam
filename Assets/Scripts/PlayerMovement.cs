using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float speed;
    Vector2 moveInput;
    Vector3 velocity;
    Vector3 move;
    public Camera mainCam;
    public Animator anim;
    
    //Jump
    [SerializeField] public float jumpHeight = 3.5f;
    public float gravity = -9.81f * 4;

    //Ground Checks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public PlayerInput controls;
    InputAction moveAction;

    void Start(){
        moveAction = controls.actions["Movement"];
    }

    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){velocity.y = -2f;}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void OnMovement(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnJump(){
        if(isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.CompareTag("FloorKill")){
            hit.gameObject.GetComponent<FloorKill>().ChangeSkyBox();
            mainCam.gameObject.SetActive(false);
            anim.SetBool("PlayerDied", true);
        }
    }
}
