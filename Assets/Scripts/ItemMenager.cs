using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenager : MonoBehaviour
{
    public Colactable[] collectableitems;

    private Dictionary<Collectabletype, Colactable> collectableItemsDic =   
        new Dictionary<Collectabletype, Colactable>();

    private void Awake()
    {
        foreach(Colactable item in collectableitems)
        {
            AddItem(item);
        }
    }

    private void AddItem(Colactable item)
    {
        if(!collectableItemsDic.ContainsKey(item.type))
        {
            collectableItemsDic.Add(item.type, item);
        }
    }

    public Colactable GetItemByType(Collectabletype type)
    {
        if(collectableItemsDic.ContainsKey(type))
        {
            return collectableItemsDic[type];
        }

        return null;

    }

}
