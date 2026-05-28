using UnityEngine;

public class HelloCube : MonoBehaviour
{
    //define variables
    public float rotationAmount = 5.0f;


    [SerializeField]
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Hello, Cube!");
        //transform.Rotate(0, 5, 0); rotates cube 5deg on y-axis on start
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello!");
        //transform.Rotate(0, 5, 0); rotates cube 5deg on y-axis every frame

        //rotate cube rotamount on space key down 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0, rotationAmount, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(0,0,1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(0, 0, -1);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Cube Clicked");
        //Destroy(gameObject); fully destroys object

        gameObject.SetActive(false);
        //Not as crazy, just hides in scene and deactivates running scripts
    }
}
