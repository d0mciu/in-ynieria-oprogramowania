using UnityEngine;
using UnityEngine.UI;

public class SetProduct : MonoBehaviour
{
    public Button button1; // Przycisk, który ustawia produkt i cenę w button2
    public Button button2; // Przycisk, którego ustawienia będą zmieniane
    public GameObject prefabToSet; // Prefab, który ma być ustawiony
    public Text priceToSet; // Cena, która ma być ustawiona

    public Transform whereToSpawn;

    void Start()
    {
        // Dodaj listenera do button1, który będzie ustawiał prefab i cenę w button2
        button1.onClick.AddListener(SetProductAndPrice);
    }

    void SetProductAndPrice()
    {
        // Ustaw prefab i cenę w skrypcie buyItem przypisanym do button2
        buyItem buyItemScript = button2.GetComponent<buyItem>();
        buyItemScript.player = whereToSpawn;
        buyItemScript.product = prefabToSet;
        buyItemScript.priceText.text = priceToSet.text;
    }
}
