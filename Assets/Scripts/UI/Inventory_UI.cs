using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
   
    public GameObject inventory;
    public GameObject eq;
    public Player player;

    public List<Slots_UI> slots = new List<Slots_UI>();

    public List<Slots_UI> slots1 = new List<Slots_UI>();

    void Update()
    {
        setup1();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
            
        }
    }

    public void ToggleInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            setup();
            
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    void setup()
    {
        if(slots.Count == player.inventory.slots.Count)
        {
            for(int i=0; i<slots.Count; i++)
            {
                if (player.inventory.slots[i].type != Collectabletype.NONE)
                {
                    slots[i].Setitem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }


    void setup1()
    {
        if (slots1.Count == player.inventory1.slots.Count)
        {
            for (int i = 0; i < slots1.Count; i++)
            {
                if (player.inventory1.slots[i].type != Collectabletype.NONE)
                {
                    slots1[i].Setitem(player.inventory1.slots[i]);
                }
                else
                {
                    slots1[i].SetEmpty();
                }
            }
        }
    }

}
