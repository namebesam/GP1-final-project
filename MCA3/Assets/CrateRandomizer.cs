using UnityEngine;

public class CrateRandomizer : MonoBehaviour
{ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeCrates();
    }

    void RandomizeCrates()
    {
        //create list of all crates under cratemanager INCLUDING ITSELF, so just have the cratemanager also be a crate lol
        //using the script itself since switching to bool approach
        Breakable[] crates = GetComponentsInChildren<Breakable>(); 

        //check if empty
        if (crates.Length == 0)
        {
            Debug.Log("no crates assigned under CrateManager");
            return;
        }

        int chestIndex = Random.Range(0, crates.Length);
        crates[chestIndex].isChestCrate = true;

        //scrap the foreach loop since it became super redundant
    }
}
