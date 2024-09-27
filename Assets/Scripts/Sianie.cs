using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    public Tilemap groundTilemap;      // Tilemap dla warstwy ziemi (kafelki jak zaorane pole)
    public Tilemap overlayTilemap;     // Tilemap dla nasion, ro�lin itp.
    public Tile tillTile;              // Tile zaorany
    public Tile defaultTile;           // Domy�lny kafelek (niezaorane pole)
    public GameObject Player;          // Odniesienie do gracza
    private Inventory inventory;
    private Inventory inventory1;

    private Inventory_UI inventoryUI;   // Dodajemy odwo�anie do skryptu Inventory_UI

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
        if (Input.GetMouseButtonDown(1)) // Sprawdzanie, czy wci�ni�to lewy przycisk myszy
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = groundTilemap.WorldToCell(mouseWorldPos);
            Vector3Int playerPosition = groundTilemap.WorldToCell(Player.transform.position);

            // Obliczenie odleg�o�ci mi�dzy graczem a klikni�tym polem
            int distance = Mathf.Abs(tilePosition.x - playerPosition.x) + Mathf.Abs(tilePosition.y - playerPosition.y);

            if (distance <= 2) // Sprawdzanie, czy odleg�o�� jest w granicach 2 p�l
            {
                Tile currentTile = groundTilemap.GetTile<Tile>(tilePosition);

                if (currentTile == defaultTile) // Je�li pole jest niezaorane
                {
                    // Zaoranie pola
                    groundTilemap.SetTile(tilePosition, tillTile);
                    Debug.Log("Pole zosta�o zaorane.");
                }
                else if (currentTile == tillTile && CheckSelectedSeed()) // Je�li pole jest zaorane i wybrano nasiona
                {
                    // Pobieranie kafelka na podstawie wybranych nasion
                    Tile seedTile = GetSeedTileFromSelectedSlot();

                    if (seedTile != null) // Je�li wybrano poprawny slot z nasionami
                    {
                        // Zasadzenie wybranych nasion
                        overlayTilemap.SetTile(tilePosition, seedTile);
                        RemoveSeedFromSelectedSlot(); // Usuni�cie nasion po zasadzeniu
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

    // Funkcja sprawdzaj�ca, czy wybrany slot zawiera nasiona
    bool CheckSelectedSeed()
    {
        if (inventoryUI.selectedSlotID == -1)
        {
            Debug.Log("Nie wybrano �adnego slotu.");
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

    // Funkcja pobieraj�ca slot na podstawie wybranego ID
    Inventory.Slot GetSelectedSlot()
    {
        return inventoryUI.isInventory1Selected? inventory1.slots[inventoryUI.selectedSlotID] : inventory.slots[inventoryUI.selectedSlotID];
    }

    // Funkcja zwracaj�ca kafelek nasion na podstawie wybranego slotu
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

    // Funkcja usuwaj�ca nasiona z wybranego slotu po zasadzeniu
    void RemoveSeedFromSelectedSlot()
    {
        Inventory.Slot selectedSlot = GetSelectedSlot();

        if (selectedSlot.count > 0)
        {
            selectedSlot.count--; // Zmniejszenie liczby nasion
            if (selectedSlot.count == 0)
            {
                selectedSlot.type = Collectabletype.NONE; // Ustawienie typu slotu na NONE, je�li liczba nasion wynosi 0
            }

            Debug.Log($"Zasiano nasiona. Pozosta�o {selectedSlot.count}.");
        }
    }
    
}
