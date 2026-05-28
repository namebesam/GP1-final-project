using UnityEngine;
using UnityEngine.Rendering;

public class PickUpBehavior : MonoBehaviour
{
    public int scoreValue = 1;

    public float rotationSpeed = 5;

    public static int pickupCount = 0;

    public static int totalScore = 0;

    public AudioClip pickupSFX;

    public LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupCount++;
        Debug.Log("Pickup count from" + transform.name + " " + pickupCount);

        levelManager = FindAnyObjectByType<LevelManager>();
        Debug.Log("Found level manager" + levelManager.name);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
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

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("PickupDestroyed");

        Destroy(gameObject, 2);
    }

    void PlayAudioEffect()
    {
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        /*
         * Debug.Log("Remaining pickups: " + pickupCount);

        if (pickupCount <= 0)
        {
            Debug.Log("You Win!");
        }
         */

    }

    public static void ResetPickups()
    {
        totalScore = 0;
        pickupCount = 0;
    }

 
}
