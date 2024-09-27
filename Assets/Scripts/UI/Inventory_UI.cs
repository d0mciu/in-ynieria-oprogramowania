using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventory;
    public GameObject eq;
    public Player player;
    private ItemMenager itemmenager;

    public int selectedSlotID = -1; // -1 oznacza brak zaznaczonego slotu
    public bool isInventory1Selected = false; // Flaga wskazujπca, czy zaznaczony slot pochodzi z inventory1

    public List<Slots_UI> slots = new List<Slots_UI>();   // Sloty z inventory
    public List<Slots_UI> slots1 = new List<Slots_UI>();  // Sloty z inventory1


    void Start()
    {
        // Inicjalizacja itemmenager - znajdü go w scenie
        itemmenager = FindObjectOfType<ItemMenager>();

        if (itemmenager == null)
        {
            Debug.LogError("ItemMenager nie zosta≥ znaleziony w scenie!");
        }
    }
    void Update()
    {
        setup1();
        setup();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        // Usuwanie przedmiotu z zaznaczonego slota po naciúniÍciu "C"
        if (Input.GetKeyDown(KeyCode.C) && selectedSlotID != -1)
        {
            if (isInventory1Selected)  // Jeúli slot pochodzi z inventory1
            {
                if (player.inventory1.slots[selectedSlotID].count > 0)
                {
                    RemoveFromInventory1(selectedSlotID);
                }
                else
                {
                    selectedSlotID = -1;
                    Debug.Log("Slot from inventory1 is empty, selection cleared.");
                }
            }
            else  // Jeúli slot pochodzi z inventory
            {
                if (player.inventory.slots[selectedSlotID].count > 0)
                {
                    Remove(selectedSlotID);
                }
                else
                {
                    selectedSlotID = -1;
                    Debug.Log("Slot from inventory is empty, selection cleared.");
                }
            }
        }
    }

    public void ToggleInventory()
    {
        if (!inventory.activeSelf)
        {
            setup();
            setup1();
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    // Setup dla inventory
    public void setup()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
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

    // Setup dla inventory1
    public void setup1()
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

    // Usuwanie przedmiotu z inventory
    public void Remove(int slotID)
    {
        Colactable itemtodrop = itemmenager.GetItemByType(player.inventory.slots[slotID].type);

        if (itemtodrop != null)
        {
            player.dropitem(itemtodrop);
            player.inventory.Remove(slotID);
            setup();
            setup1();

        }
       
    }

    // Usuwanie przedmiotu z inventory1
    public void RemoveFromInventory1(int slotID)
    {
        Colactable itemtodrop = itemmenager.GetItemByType(player.inventory1.slots[slotID].type);

        if(itemtodrop != null)
        {
            player.dropitem(itemtodrop);
            player.inventory1.Remove(slotID);
            setup();
            setup1();

        }

      
    }

    // Wybieranie slota z inventory
    public void SelectSlotFromInventory(int slotID)
    {
        selectedSlotID = slotID;
        isInventory1Selected = false;
        Debug.Log($"Slot {slotID} selected from inventory.");
    }

    public void SelectSlotFromInventory1(int slotID)
    {
        selectedSlotID = slotID;
        isInventory1Selected = true;
        Debug.Log($"Slot {slotID} selected from inventory1.");
    }

}