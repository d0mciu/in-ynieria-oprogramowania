using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rosniecie : MonoBehaviour
{
    // Definicja klasy Vegetable dla ró¿nych roœlin
    [System.Serializable]
    public class Vegetable
    {
        public string name; // Nazwa warzywa (opcjonalnie)
        public List<Tile> growthStages; // Lista kafelków dla poszczególnych etapów wzrostu
        public GameObject harvestablePrefab; // Prefab, który pojawi siê po zakoñczeniu cyklu
        public float growthInterval = 2f; // Interwa³ czasowy miêdzy zmianami etapów
    }

    public Tilemap overlayTilemap; // Tilemap, na której s¹ umieszczone roœliny
    public List<Vegetable> vegetables; // Lista roœlin do sadzenia
    private HashSet<Vector3Int> processedTiles = new HashSet<Vector3Int>(); // Zestaw przetworzonych kafelków

    void Update()
    {
        // Iterujemy po wszystkich kafelkach na Tilemap
        foreach (Vector3Int pos in overlayTilemap.cellBounds.allPositionsWithin)
        {
            TileBase currentTile = overlayTilemap.GetTile(pos);

            // Sprawdzamy ka¿d¹ roœlinê z listy
            foreach (var vegetable in vegetables)
            {
                // Sprawdzamy, czy obecny kafelek to pierwszy etap wzrostu roœliny i czy nie zosta³ jeszcze przetworzony
                if (currentTile == vegetable.growthStages[0] && !processedTiles.Contains(pos))
                {
                    // Rozpoczynamy procedurê wzrostu roœliny
                    StartCoroutine(GrowVegetable(pos, vegetable));
                    processedTiles.Add(pos); // Dodajemy pozycjê kafelka do przetworzonych
                }
            }
        }
    }

    // Korutyna do wzrostu roœliny
    IEnumerator GrowVegetable(Vector3Int tilePosition, Vegetable vegetable)
    {
        Tilemap tilemap = overlayTilemap;
        for (int i = 1; i < vegetable.growthStages.Count; i++)
        {
            yield return new WaitForSeconds(vegetable.growthInterval); // Czekaj na interwa³
            tilemap.SetTile(tilePosition, vegetable.growthStages[i]); // Zmieñ kafelek na kolejny etap wzrostu
            Debug.Log($"{vegetable.name} has grown to stage {i} at position {tilePosition}.");
        }

        // Po zakoñczeniu wzrostu usuwamy kafelek i instancjujemy obiekt warzywa
        yield return new WaitForSeconds(vegetable.growthInterval);
        tilemap.SetTile(tilePosition, null);
        Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition); // Zamiana pozycji kafelka na pozycjê w œwiecie
        Instantiate(vegetable.harvestablePrefab, worldPosition, Quaternion.identity); // Tworzymy obiekt warzywa na pozycji

        Debug.Log($"{vegetable.name} has fully grown and is ready for harvest at position {worldPosition}.");

        // Usuniêcie przetworzonego kafelka z listy, aby móc ponownie zasadziæ w tym samym miejscu
        processedTiles.Remove(tilePosition);
    }
}
