using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelProduct : MonoBehaviour
{
    public Button button1; // Przycisk, kt�ry ustawia produkt i cen� w button2
    public Button button2; // Przycisk, kt�rego ustawienia b�d� zmieniane
    public Text priceToSet; // Cena, kt�ra ma by� ustawiona

    void Start()
    {
        // Dodaj listenera do button1, kt�ry b�dzie ustawia� prefab i cen� w button2
        button1.onClick.AddListener(SetProductAndPrice);
    }

    void SetProductAndPrice()
    {
        // Ustaw prefab i cen� w skrypcie buyItem przypisanym do button2
        SellProduct sellscript = button2.GetComponent<SellProduct>();
        sellscript.priceText.text = priceToSet.text;
    }

}