using UnityEngine;

public class MathDemo : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;

    void Start()
    {
        Vector3 up = Vector3.up; //(0, 1, 0)
        Vector3 down = Vector3.down; //(0, -1, 0)
        
        //var is cool, lets the right hand side assign variable type on the left
        var forward = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.rotate(0, 1, 0) rotates on the y axis, since is only 1 deg by default we're gonna multiply this
            transform.Rotate(Vector3.up * 90);
        }

        transform.Rotate(Vector3.up * 90 * Time.deltaTime);
    }
}
