using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public Inventory inventory;
    public Inventory inventory1;
    void Awake()
    {
        inventory = new Inventory(24);
        inventory1 = new Inventory(8);
    }

    
    public void dropitem(Colactable item)
    {
        Vector3 spawnlocation= transform.position;

        float randx = Random.Range(-1f, 1f);
        float randy = Random.Range(-1f, 1f);

        Vector3 spawnoffset = new Vector3(randx, randy, 0f).normalized;

        Instantiate(item, spawnlocation + spawnoffset, Quaternion.identity);

    }


}
