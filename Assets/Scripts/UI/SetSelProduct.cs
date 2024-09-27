using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelProduct : MonoBehaviour
{
    public Button button1; // Przycisk, który ustawia produkt i cenê w button2
    public Button button2; // Przycisk, którego ustawienia bêd¹ zmieniane
    public Text priceToSet; // Cena, która ma byæ ustawiona

    void Start()
    {
        // Dodaj listenera do button1, który bêdzie ustawia³ prefab i cenê w button2
        button1.onClick.AddListener(SetProductAndPrice);
    }

    void SetProductAndPrice()
    {
        // Ustaw prefab i cenê w skrypcie buyItem przypisanym do button2
        SellProduct sellscript = button2.GetComponent<SellProduct>();
        sellscript.priceText.text = priceToSet.text;
    }

}