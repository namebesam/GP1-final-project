using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;

    public float flySpeed;

    public int killCount = 0;
    public int maxKills = 5;

    public AudioSource flySFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        selectTargetDementor();
        flySFX.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flyAtTarget();
    }

    void selectTargetDementor()
    {
        //creates array of all current enemies
        GameObject[] dementors = GameObject.FindGameObjectsWithTag("Dementor");

        //picks one at random
        int targetInt = Random.Range(0, dementors.Length);

        //captures its transform to be targeted
        target = dementors[targetInt].transform;

        //destroy if no more enemies left
        if (dementors.Length == 0) 
        {
            Destroy(gameObject);
        }
    }

    void flyAtTarget()
    {
        Vector3 targetDir = (target.position - transform.position).normalized;
        rb.AddForce(targetDir * flySpeed, ForceMode.VelocityChange);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dementor"))
        {
            killCount++;

            //destroy once we kill the max number of enemies
            if (killCount >= maxKills)
            {
                Destroy(gameObject);
            }

            selectTargetDementor();
        }
    }
}
