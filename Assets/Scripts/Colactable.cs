using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Collectabletype
{
    NONE, TOMATO_SEED
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

            player.inventory.Add(this);
            player.inventory1.Add(this);
            Destroy(gameObject);            
        }
            
    }
  
}

