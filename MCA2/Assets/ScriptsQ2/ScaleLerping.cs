using UnityEngine;
using UnityEngine.Rendering;

public class ScaleLerping : MonoBehaviour
{
    public int stepVersion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 1; 

        // 4 different versions of steps used in scale lerp
        if (stepVersion == 0)
        {
            step = Time.deltaTime * 5f;
           
        }

        else if (stepVersion == 1)
        {
            step = Time.time * 5f;
            
        }

        else if (stepVersion == 2)
        {
            step = Mathf.PingPong(Time.time, 1);
            Debug.Log("step" + step);
        }

        else if (stepVersion == 3)
        {
            //normalizes
            step = (Mathf.Sin(Time.time) + 1) / 2;
        }

        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 3, step);
    }
}
