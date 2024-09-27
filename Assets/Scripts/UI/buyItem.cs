using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buyItem : MonoBehaviour
{
    public bool canBuy = true;
    public Transform player;
    public GameObject product;
    moneyHandler money;
    public Text priceText;
    void Update()
    {
        // Deklaracja zmiennej na przechowanie wyniku konwersji
        int price = 0;
        
        // Bezpieczna próba konwersji tekstu na liczbę
        if (int.TryParse(priceText.text, out price))
        {
            if (moneyHandler.money - price >= 0)
            {   
                canBuy = true;
            }
            else
            {
                canBuy = false; 
            }
        }
        else
        {
            canBuy = false; // Ustawiamy canBuy na false, gdy konwersja się nie powiedzie
            Debug.LogError("Price text is not in correct format: " + priceText.text);
        }
    }
    public void itemBuy(){
        if(canBuy){
            Instantiate(product, player.position, Quaternion.identity);
            moneyHandler.money -= int.Parse(priceText.text);
        }
    }
}
