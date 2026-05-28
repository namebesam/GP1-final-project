using UnityEngine;

public class InterpolationDemo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    public float speed = 5.0f;

    void Start()
    {
        //if we forget to assign a player in the inspector, we can search for the player thru tags
        if (!player)
        {
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
        {
            return;
        }
   
        float step = Time.deltaTime * speed;
        //actual lerp
        //transform.position = Vector3.Lerp(transform.position, player.position, step);
        
        //same idea, Unity has a built in function for this
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }
}
