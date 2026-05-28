using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter(Collider other)
    {
        if (audioSource.clip)
        {
            if (other.CompareTag("Dementor"))
            {
                audioSource.Play();
            }
        }
       
    }
}
