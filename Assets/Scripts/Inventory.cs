using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public int count;
        public int max;
        public Collectabletype type;
        public Sprite icon;

        public Slot()
        {
            type = Collectabletype.NONE;
            count = 0;
            max = 99;
        }

        public bool CanAdd()
        {
            if (count < max) return true;
            else return false;
        }

        public void AddItem(Colactable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;

            Debug.Log($"Added item of type: {item.type} to inventory. Count is now {count}.");
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;

                if(count == 0)
                {
                    icon= null;
                    type = Collectabletype.NONE;
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public bool Add(Colactable item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAdd())
            {
                slot.AddItem(item);
                return true;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == Collectabletype.NONE)
            {
                slot.AddItem(item);
                return true;
            }
        }

        return false; // Gdy nie uda�o si� doda� przedmiotu
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }


}
