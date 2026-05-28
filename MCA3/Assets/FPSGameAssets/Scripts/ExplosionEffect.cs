using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionRadius = 5;
    public float forceMagnitude = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reducto();
    }

    void Reducto()
    {
        Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>(); //returns an array of our pieces


        foreach (Rigidbody rb in pieces) {
            rb.AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
            Debug.Log("Exploding: " + rb.name);
        }
        
        //Debug.Log("Rigidbodies: " + pieces.Length);
    }
}
