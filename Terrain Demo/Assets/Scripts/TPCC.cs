using UnityEngine;

public class TPCC : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    public float walkingSpeed = 2f;
    public float sprintSpeed = 8f;
    public float rotationSpeed = 60f;
    public float acceleration = 2.5f;
    private bool isSprinting;
    private float rotation;
    private CharacterController characterController;
    private Animator anim;
    private float interpolatedSpeed;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Rotate with A-D
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * rotationSpeed;
        transform.Rotate(0, turn * Time.deltaTime, 0);

        // Move with W-S
        // Sprint with Left Shift
        float pace = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkingSpeed;
        float speed = pace * Input.GetAxis("Vertical");
        if (speed < -walkingSpeed) speed = -walkingSpeed;
        interpolatedSpeed = Mathf.Lerp(interpolatedSpeed, speed, Time.deltaTime * acceleration);
        Vector3 movement = interpolatedSpeed * transform.forward;
        characterController.SimpleMove(movement);
        anim.SetFloat(Speed, interpolatedSpeed);
    }
}