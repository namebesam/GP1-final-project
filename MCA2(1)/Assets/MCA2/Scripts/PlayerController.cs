using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    Rigidbody rb;

    //gonna use this in levelmanager as a win condition
    public bool flagHit = false;

    public GameObject bowserFlag;
    public GameObject marioFlag;

    public AudioClip jumpSFX;

    public LevelManager levelManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gonna do a switcherooni of two flag objects when the player collides
        if (bowserFlag && marioFlag)
        {
            bowserFlag.SetActive(true);
            marioFlag.SetActive(false);
        }
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.IsPlaying)
            return;

        Jump();
    }

    void FixedUpdate()
    {
        if (LevelManager.IsPlaying)
        {
            Move();
        }

        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log("Horizontal: " + horizontal);
        //Debug.Log("Vertical: " + vertical);

        // compute a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        // apply force 
        rb.AddForce(movement * speed);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlaySound();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //disable when in midair
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with: " + collision.transform.name);
        ContactPoint contact = collision.contacts[0];
        //Debug.Log("Collided with floor at position: " + contact.point);
        //Debug.Log("Contact normal: " + contact.normal);
        
        //anything above .5 generally represents a horizontal surface
        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

        //could also do where you collide with anthing tagged with Ground (how I usually do it)

        if (collision.gameObject.CompareTag("Hazard"))
        {
            levelManager.LevelLost();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Flagpole"))
        {
            flagHit = true;
            bowserFlag.SetActive(false);
            marioFlag.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collided with: " + collision.transform.name);
    }

    void PlaySound()
    {
        if (jumpSFX)
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource.clip)
            {
                audioSource.Play();
            }
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }
        
    }
}
