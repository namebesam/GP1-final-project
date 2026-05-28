using UnityEngine;
using UnityEngine.Rendering;

public class PickUpBehavior : MonoBehaviour
{
    public int scoreValue = 5;

    public float rotationSpeed = 5;

    public static int pickupCount = 0;

    public static int totalScore = 0;

    public AudioClip pickupSFX;

    public LevelManager levelManager;

    private BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupCount++;
        Debug.Log("Pickup count from" + transform.name + " " + pickupCount);

        levelManager = FindAnyObjectByType<LevelManager>();
        Debug.Log("Found level manager" + levelManager.name);

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); replaced with Animation for now
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyPickup();
        }
        
    }

    void DestroyPickup()
    {
        totalScore += scoreValue;
        Debug.Log("Current Total Score: " + totalScore);

        if (levelManager)
        {
            levelManager.SetScoreText(totalScore);
        }

        pickupCount--; 

        PlayAudioEffect();

       //turns collider off to prevent repeat interactions before gameobject is destroyed
       boxCollider.enabled = false;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("PickupDestroyed");

        Destroy(gameObject, 2);
    }

    void PlayAudioEffect()
    {
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
    }

    public static void ResetPickups()
    {
        totalScore = 0;
        pickupCount = 0;
    }
}
