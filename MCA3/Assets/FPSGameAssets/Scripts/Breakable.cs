using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject cratePieces;
    public GameObject chestPrefab;

    public bool isChestCrate = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isChestCrate && chestPrefab)
        {
            Instantiate(chestPrefab, transform.position, transform.rotation);
        }

        Instantiate(cratePieces, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
