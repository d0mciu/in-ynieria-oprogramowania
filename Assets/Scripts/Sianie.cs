using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sianie : MonoBehaviour
{
    public Tilemap tilemap1;
    public Tile till;
    public GameObject Player;
    Inventory inventory;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button was pressed
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap1.WorldToCell(mouseWorldPos);

            Vector3Int playerPosition = tilemap1.WorldToCell(Player.transform.position);

            // Calculate the distance between the player and the clicked tile position
            int distance = Mathf.Abs(tilePosition.x - playerPosition.x) + Mathf.Abs(tilePosition.y - playerPosition.y);

            if (distance <= 2) // Check if the distance is within 2 tiles
            {
                Tile currentTile = tilemap1.GetTile<Tile>(tilePosition);

                if (currentTile == till) // Check if the tile at the position is the oldTile
                {
                    zasiej(); 
                }
            }
        }
    }

    public void zasiej()
    {
        if (inventory.slots[0].type == Collectabletype.TOMATO_SEED)
        {
            tilemap1.a
        }
    }

}


