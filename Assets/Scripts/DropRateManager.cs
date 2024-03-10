using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        //public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        if(!gameObject.scene.isLoaded) return;

        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops d in drops)
        {
            if(randomNumber <= d.dropRate)
            {
                //Instantiate(d.itemPrefab, transform.position, Quaternion.identity);
                possibleDrops.Add(d);
            }
        }

        if (possibleDrops.Count > 3)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(3, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
        else if (possibleDrops.Count > 2)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(2, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
        else if (possibleDrops.Count > 1)
        {//these greater 1 and greater than 2 if statements try to spawn the rarer items since I set the least rare items as the first ones
            Drops drops = possibleDrops[UnityEngine.Random.Range(1, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
        else if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
