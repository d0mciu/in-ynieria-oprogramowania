using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    public Tilemap groundTilemap;      // Tilemap dla warstwy ziemi (kafelki jak zaorane pole)
    public Tilemap overlayTilemap;     // Tilemap dla nasion, roœlin itp.
    public Tile tillTile;              // Tile zaorany
    public Tile defaultTile;           // Domyœlny kafelek (niezaorane pole)
    public GameObject Player;          // Odniesienie do gracza
    private Inventory inventory;
    private Inventory inventory1;

    private Inventory_UI inventoryUI;   // Dodajemy odwo³anie do skryptu Inventory_UI

    public Tile tomatoSeedTile;        // Tile nasion pomidora
    public Tile potatoSeedTile;        // Tile nasion ziemniaka
    

    void Start()
    {
        // Znalezienie ekwipunku na obiekcie gracza
        inventory = Player.GetComponent<Player>().inventory;
        inventory1 = Player.GetComponent<Player>().inventory1;

        if (inventory == null || inventory1 == null)
        {
            Debug.LogError("Inventory or Inventory1 is not assigned correctly in FieldManager script.");
        }

        inventoryUI = FindObjectOfType<Inventory_UI>();
        if (inventoryUI == null)
        {
            Debug.LogError("Inventory_UI not found.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Sprawdzanie, czy wciœniêto lewy przycisk myszy
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = groundTilemap.WorldToCell(mouseWorldPos);
            Vector3Int playerPosition = groundTilemap.WorldToCell(Player.transform.position);

            // Obliczenie odleg³oœci miêdzy graczem a klikniêtym polem
            int distance = Mathf.Abs(tilePosition.x - playerPosition.x) + Mathf.Abs(tilePosition.y - playerPosition.y);

            if (distance <= 2) // Sprawdzanie, czy odleg³oœæ jest w granicach 2 pól
            {
                Tile currentTile = groundTilemap.GetTile<Tile>(tilePosition);

                if (currentTile == defaultTile) // Jeœli pole jest niezaorane
                {
                    // Zaoranie pola
                    groundTilemap.SetTile(tilePosition, tillTile);
                    Debug.Log("Pole zosta³o zaorane.");
                }
                else if (currentTile == tillTile && CheckSelectedSeed()) // Jeœli pole jest zaorane i wybrano nasiona
                {
                    // Pobieranie kafelka na podstawie wybranych nasion
                    Tile seedTile = GetSeedTileFromSelectedSlot();

                    if (seedTile != null) // Jeœli wybrano poprawny slot z nasionami
                    {
                        // Zasadzenie wybranych nasion
                        overlayTilemap.SetTile(tilePosition, seedTile);
                        RemoveSeedFromSelectedSlot(); // Usuniêcie nasion po zasadzeniu
                        Debug.Log("Nasiona zasiane.");
                    }
                    else
                    {
                        Debug.Log("Wybrany slot nie zawiera nasion.");
                    }
                }
            }
        }
    }

    // Funkcja sprawdzaj¹ca, czy wybrany slot zawiera nasiona
    bool CheckSelectedSeed()
    {
        if (inventoryUI.selectedSlotID == -1)
        {
            Debug.Log("Nie wybrano ¿adnego slotu.");
            return false;
        }

        Inventory.Slot selectedSlot = GetSelectedSlot();
        if (selectedSlot.type == Collectabletype.TOMATO_SEED || selectedSlot.type == Collectabletype.POTOTO_SEED)
        {
            return true;
        }

        Debug.Log("Wybrany slot nie zawiera nasion.");
        return false;
    }

    // Funkcja pobieraj¹ca slot na podstawie wybranego ID
    Inventory.Slot GetSelectedSlot()
    {
        return inventoryUI.isInventory1Selected? inventory1.slots[inventoryUI.selectedSlotID] : inventory.slots[inventoryUI.selectedSlotID];
    }

    // Funkcja zwracaj¹ca kafelek nasion na podstawie wybranego slotu
    Tile GetSeedTileFromSelectedSlot()
    {
        Inventory.Slot selectedSlot = GetSelectedSlot();

        switch (selectedSlot.type)
        {
            case Collectabletype.TOMATO_SEED:
                return tomatoSeedTile;
            case Collectabletype.POTOTO_SEED:
                return potatoSeedTile;
            default:
                return null;
        }
    }

    // Funkcja usuwaj¹ca nasiona z wybranego slotu po zasadzeniu
    void RemoveSeedFromSelectedSlot()
    {
        Inventory.Slot selectedSlot = GetSelectedSlot();

        if (selectedSlot.count > 0)
        {
            selectedSlot.count--; // Zmniejszenie liczby nasion
            if (selectedSlot.count == 0)
            {
                selectedSlot.type = Collectabletype.NONE; // Ustawienie typu slotu na NONE, jeœli liczba nasion wynosi 0
            }

            Debug.Log($"Zasiano nasiona. Pozosta³o {selectedSlot.count}.");
        }
    }
    
}
