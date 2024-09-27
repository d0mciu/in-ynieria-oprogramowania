using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rosniecie : MonoBehaviour
{
    // Definicja klasy Vegetable dla r�nych ro�lin
    [System.Serializable]
    public class Vegetable
    {
        public string name; // Nazwa warzywa (opcjonalnie)
        public List<Tile> growthStages; // Lista kafelk�w dla poszczeg�lnych etap�w wzrostu
        public GameObject harvestablePrefab; // Prefab, kt�ry pojawi si� po zako�czeniu cyklu
        public float growthInterval = 2f; // Interwa� czasowy mi�dzy zmianami etap�w
    }

    public Tilemap overlayTilemap; // Tilemap, na kt�rej s� umieszczone ro�liny
    public List<Vegetable> vegetables; // Lista ro�lin do sadzenia
    private HashSet<Vector3Int> processedTiles = new HashSet<Vector3Int>(); // Zestaw przetworzonych kafelk�w

    void Update()
    {
        // Iterujemy po wszystkich kafelkach na Tilemap
        foreach (Vector3Int pos in overlayTilemap.cellBounds.allPositionsWithin)
        {
            TileBase currentTile = overlayTilemap.GetTile(pos);

            // Sprawdzamy ka�d� ro�lin� z listy
            foreach (var vegetable in vegetables)
            {
                // Sprawdzamy, czy obecny kafelek to pierwszy etap wzrostu ro�liny i czy nie zosta� jeszcze przetworzony
                if (currentTile == vegetable.growthStages[0] && !processedTiles.Contains(pos))
                {
                    // Rozpoczynamy procedur� wzrostu ro�liny
                    StartCoroutine(GrowVegetable(pos, vegetable));
                    processedTiles.Add(pos); // Dodajemy pozycj� kafelka do przetworzonych
                }
            }
        }
    }

    // Korutyna do wzrostu ro�liny
    IEnumerator GrowVegetable(Vector3Int tilePosition, Vegetable vegetable)
    {
        Tilemap tilemap = overlayTilemap;
        for (int i = 1; i < vegetable.growthStages.Count; i++)
        {
            yield return new WaitForSeconds(vegetable.growthInterval); // Czekaj na interwa�
            tilemap.SetTile(tilePosition, vegetable.growthStages[i]); // Zmie� kafelek na kolejny etap wzrostu
            Debug.Log($"{vegetable.name} has grown to stage {i} at position {tilePosition}.");
        }

        // Po zako�czeniu wzrostu usuwamy kafelek i instancjujemy obiekt warzywa
        yield return new WaitForSeconds(vegetable.growthInterval);
        tilemap.SetTile(tilePosition, null);
        Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition); // Zamiana pozycji kafelka na pozycj� w �wiecie
        Instantiate(vegetable.harvestablePrefab, worldPosition, Quaternion.identity); // Tworzymy obiekt warzywa na pozycji

        Debug.Log($"{vegetable.name} has fully grown and is ready for harvest at position {worldPosition}.");

        // Usuni�cie przetworzonego kafelka z listy, aby m�c ponownie zasadzi� w tym samym miejscu
        processedTiles.Remove(tilePosition);
    }
}
