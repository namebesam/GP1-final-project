using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 0.4f;
    public float gravity = 9.81f;
    public float airControl = 10f;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;

    Animator animator;
    int animState;

    public Transform cameraTransform;

    public float rotationSpeed = 5f;
    public float smoothSpeed;

    float currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //input vector
        //input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input = new Vector3(moveHorizontal, 0f, moveVertical);
        input.Normalize();

        if (controller.isGrounded)
        {
            moveDirection = input;
            animState = 1;

            if (input.magnitude >= 0.1f)
            {
                //rotate the character to match the camera position
                animState = 1;

                float rotationAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                //Quaternion smoothAngle = Quaternion.Euler(0f, rotationAngle, 0f);
                //transform.rotation = Quaternion.Slerp(transform.rotation, smoothAngle, Time.deltaTime * rotationSpeed);

                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref currentVelocity, smoothSpeed);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                //
                Vector3 moveDir = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                moveDirection = moveDir.normalized * rotationSpeed;


                if (Input.GetButton("Fire1"))
                {
                    animState = 3;
                }
            }

            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    animState = 4;
                }
            }

            //jump
            if (Input.GetButton("Jump"))
            {
                animState = 2;
                //moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity); to allow the animation to run its course 
            }

            else
            {
                moveDirection.y = 0.0f;
            }
        }


        else //midair
        {
            //input.y = moveDirection.y;
            //moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        //set the animation state
        animator.SetInteger("animState", animState);

        //moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
