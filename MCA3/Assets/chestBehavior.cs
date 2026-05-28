using UnityEngine;

public class chestBehavior : MonoBehaviour
{
    public Animator lidAnimator;
    public AudioSource lidOpenSFX;

    //to be able to stop spawning enemies 
    private GameObject EnemySpawn;
    private EnemySpawner spawnScript;

    //RELEASE THE BALLS
    public GameObject balls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemySpawn = GameObject.FindWithTag("Spawner");
        spawnScript = EnemySpawn.GetComponent<EnemySpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alohomora"))
        {
            lidAnimator.SetTrigger("openLid");
            lidOpenSFX.Play();
            spawnScript.chestOpen = true;
            balls.SetActive(true);
        }
    }
}
