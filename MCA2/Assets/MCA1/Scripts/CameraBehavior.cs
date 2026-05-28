using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CameraBehavior : MonoBehaviour
{
    public GameObject zoomTarget;

    private Vector3 initialPosition;
    
    public float camRotation;
    public float zoomSpeed;
    public float minDistance;

    public bool spacePressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get initial camera position to return to after hitting space, keep it chillin in the back pocket
        initialPosition = transform.position;
        

        //If sun not assigned
        if (!zoomTarget)
        {
            if (GameObject.FindGameObjectWithTag("Sun"))
            {
                zoomTarget = GameObject.FindGameObjectWithTag("Sun");
            }

            else
            {
                Debug.Log("Either reference the sun object in the inspector or change its tag");
                return; //should kill the script
            }

            //same check used earlier
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = !spacePressed;
        }

        if (spacePressed == true)
        {
            //rotate around the sun
            transform.RotateAround(zoomTarget.transform.position, Vector3.up, camRotation * Time.deltaTime);

            float currentDistance = Vector3.Distance(transform.position, zoomTarget.transform.position);

            //initally had a really cool vector multiplication thing but it ended up pinning the camera to a position
            //right in front of the sun, canceling the orbit. Instead thia allows the camera to move on its 
            //own x-axis
            if (currentDistance > minDistance)
            {
                float step = Time.deltaTime * zoomSpeed;
                transform.position = Vector3.MoveTowards(transform.position, zoomTarget.transform.position, step);
            } 
        } 

        if (spacePressed == false)
        {
            float step = Time.deltaTime * zoomSpeed;
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);
        }

        //look at the sun every frame
        transform.LookAt(zoomTarget.transform);
    }
}
