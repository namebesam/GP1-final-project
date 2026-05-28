using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RevolveAround : MonoBehaviour
{
    public GameObject sun;
    public float rotationAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //If sun not assigned
        if (!sun)
        {
            if (GameObject.FindGameObjectWithTag("Sun"))
            {
                sun = GameObject.FindGameObjectWithTag("Sun");
            }

            else
            {
                Debug.Log("Either reference the sun object in the inspector or change its tag");
                return; //should kill the script
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(sun.transform.position, Vector3.up, rotationAmount * Time.deltaTime);
    }
}
