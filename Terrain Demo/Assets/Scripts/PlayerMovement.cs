using System.Linq;
using UnityEditor.Embree;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 2f;
    public float sprintSpeed = 8f;
    public float rotationSpeed = 60f;
    private bool isSprinting;
    private float rotation;
    private CharacterController _characterController;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * rotationSpeed;
        bool isTurning = Mathf.Approximately(turnInput, 0);
        anim.SetBool("isTurning", !isTurning);
        transform.Rotate(0, turn * Time.deltaTime, 0);
        anim.SetFloat("Rotation", turnInput);
        Debug.Log(isTurning);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float pace = isSprinting ? sprintSpeed : walkingSpeed;
        float speed = pace * Input.GetAxis("Vertical");
        if (speed < -walkingSpeed) speed = -walkingSpeed;
        Vector3 movement = speed * transform.forward;
        _characterController.SimpleMove(movement);
        anim.SetFloat("Speed", speed);
    }
}