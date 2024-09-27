using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilechange : MonoBehaviour
{
    public Tilemap tilemap;  // Tilemap, on which we will work
    public Tile newTile;     // New tile that we will replace with
    public Tile oldTile;     // Tile to be replaced
    public GameObject Player; // Reference to the player object

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button was pressed
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

            Vector3Int playerPosition = tilemap.WorldToCell(Player.transform.position);

            // Calculate the distance between the player and the clicked tile position
            int distance = Mathf.Abs(tilePosition.x - playerPosition.x) + Mathf.Abs(tilePosition.y - playerPosition.y);

            if (distance <= 2) // Check if the distance is within 2 tiles
            {
                Tile currentTile = tilemap.GetTile<Tile>(tilePosition);

                if (currentTile == oldTile) // Check if the tile at the position is the oldTile
                {
                    tilemap.SetTile(tilePosition, newTile); // Replace the tile with the new one
                }
            }
        }
    }
}
