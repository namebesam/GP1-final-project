using UnityEngine;

public class FireBarBehavior : MonoBehaviour
{
    public Vector3 rotationDirection = Vector3.forward;
    public float rotationSpeed = 5.0f;

    public GameObject centerPoint;

    Vector3 pivot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pivot = centerPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivot, rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
