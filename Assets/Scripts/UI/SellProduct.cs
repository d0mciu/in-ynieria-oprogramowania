using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellProduct : MonoBehaviour
{
    public Text priceText;           // Cena sprzedawanego przedmiotu
    public Inventory_UI inventoryUI; // Odwo³anie do UI Inventory
    private ItemMenager itemManager;  // Mened¿er przedmiotów

    private static Collectabletype itemToSellType = Collectabletype.NONE;

    private void Start()
    {
        // ZnajdŸ ItemMenager w scenie
        itemManager = FindObjectOfType<ItemMenager>();

        if (itemManager == null)
        {
            Debug.LogError("ItemMenager nie zosta³ znaleziony w scenie!");
        }


        if (inventoryUI == null)
        {
            inventoryUI = FindObjectOfType<Inventory_UI>();

            if (inventoryUI == null)
            {
                Debug.LogError("Inventory_UI nie zosta³o znalezione w scenie!");
            }
        }
    }

    // Funkcja ustawiaj¹ca przedmiot do sprzeda¿y (wywo³ywana po klikniêciu przycisku np. "Pomidor")
    public void SetItemToSell(int itemTypeIndex)
    {
        // Konwersja indeksu na Collectabletype
        Collectabletype itemType = (Collectabletype)itemTypeIndex;
        itemToSellType = itemType;
        Debug.Log("Item selected for selling: " + itemType);
    }


    // Funkcja do sprzedania przedmiotu
    public void SellItem()
    {
        Debug.Log($"Attempting to sell item: {itemToSellType}");

        if (itemToSellType != Collectabletype.NONE)
        {
            int price = 0;
            // Konwersja tekstu na liczbê
            if (int.TryParse(priceText.text, out price))
            {
                SellFromInventory(price);  // Sprzedaj z inventory na podstawie typu przedmiotu
            }
            else
            {
                Debug.LogError("Price text is not in correct format: " + priceText.text);
            }
        }
        else
        {
            Debug.Log("No item selected for selling.");
        }
    }

    // Sprzedawanie przedmiotu z ekwipunku na podstawie typu
    private void SellFromInventory(int price)
    {
        if (inventoryUI == null)
        {
            Debug.LogError("inventoryUI is not assigned!");
            return;
        }

        if (inventoryUI.player == null || inventoryUI.player.inventory == null)
        {
            Debug.LogError("Player or Player's inventory is not assigned or initialized!");
            return;
        }

        // Przegl¹daj sloty w ekwipunku w poszukiwaniu przedmiotu tego typu
        for (int i = 0; i < inventoryUI.player.inventory.slots.Count; i++)
        {
            var slot = inventoryUI.player.inventory.slots[i];

            if (slot.type == itemToSellType && slot.count > 0)
            {
                moneyHandler.money += price;
                inventoryUI.player.inventory.Remove(i);

                Debug.Log($"Sold {itemToSellType} from inventory for {price}.");

                itemToSellType = Collectabletype.NONE;
                inventoryUI.setup();
                inventoryUI.setup1();
                return; // Wyjœcie po sprzeda¿y pierwszego znalezionego przedmiotu
            }
        }

        // SprawdŸ inventory1
        for (int i = 0; i < inventoryUI.player.inventory1.slots.Count; i++)
        {
            var slot = inventoryUI.player.inventory1.slots[i];

            if (slot.type == itemToSellType && slot.count > 0)
            {
                moneyHandler.money += price;
                inventoryUI.player.inventory1.Remove(i); // Usuniêcie z inventory1

                Debug.Log($"Sold {itemToSellType} from inventory1 for {price}.");

                itemToSellType = Collectabletype.NONE;
                inventoryUI.setup();
                inventoryUI.setup1();
                return; // Wyjœcie po sprzeda¿y pierwszego znalezionego przedmiotu
            }
        }

        Debug.Log($"No {itemToSellType} found in inventory to sell.");
    }

}
