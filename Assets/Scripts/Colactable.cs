using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Collectabletype
{
    NONE, TOMATO_SEED, TOMATO, POTOTO_SEED, POTATO, STONE, PLUM, FLOWER
}
public class Colactable : MonoBehaviour
{

    private Player player;
    public Collectabletype type;
    public Sprite icon;

    private void Awake() 
    {   
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Collected item of type: {type}");
            if (!player.inventory1.Add(this))
            {
                player.inventory.Add(this);
            }
            Destroy(gameObject);
        }
    }


}

