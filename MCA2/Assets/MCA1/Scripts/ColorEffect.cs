using UnityEngine;

public class ColorEffect : MonoBehaviour
{
    public Color sunInitialColor;

    public Color sunTargetColor;

    private Renderer sunRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sunRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //taking the ping pong step from colorlerper since it looked decent
        float step = Mathf.PingPong(Time.time, 1);
        sunRenderer.material.color = Color.Lerp(sunInitialColor, sunTargetColor, step);
    }
}
