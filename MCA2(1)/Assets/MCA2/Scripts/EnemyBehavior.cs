using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;
    public float stopDistance = 0.1f;

    public AudioClip enemySFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();

        //enemy deletion for grading purposes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    void FollowTarget()
    {
        if(target && LevelManager.IsPlaying)
        {
            //Should hopefully keep the podaboo grounded by ignoring the target's Y-position in following
            Vector3 groundedTarget = new Vector3(target.position.x, transform.position.y, target.position.z);

            //establish a min distance to fix weird slingshotting problem
            float distance = Vector3.Distance(transform.position, groundedTarget);

            transform.LookAt(groundedTarget);

            if (distance > stopDistance)
            transform.position = Vector3.MoveTowards(transform.position, groundedTarget, speed * Time.deltaTime);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position);

            Animator animator = GetComponent<Animator>();

            //turn collider off so that you can't be hit during bump animation
            //BoxCollider boxColliderEnemy = GetComponent<BoxCollider>();
            //boxColliderEnemy.enabled = false;

            animator.SetTrigger("PodobooDead");
            Destroy(gameObject, 1);
        }
    }
}
