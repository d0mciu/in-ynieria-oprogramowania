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

   
}
