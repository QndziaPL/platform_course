using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessPlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TileBase ruleTile;
    [SerializeField] Tilemap tilemap;

    Tilemap tilemapInstance;
    int width = 13;
    int height = 8;
    int upStep = 3;
    // [SerializeField] Grid grid;
    Grid grid;

    Vector2Int singlePlatform = new Vector2Int(7, 3);

    Vector2 tempStartPointForSpawning = new Vector2(13, -15);

    // Vector2Int endOfLastSpawnedPlatform = TU ZACZĄĆ i na dole


    void Awake()
    {
        grid = new GameObject("EndlessPlatformGrid").AddComponent<Grid>();
        grid.transform.position = new Vector2(tempStartPointForSpawning.x, tempStartPointForSpawning.y);
        tilemapInstance = Instantiate(tilemap, grid.transform.position, Quaternion.identity);
        tilemapInstance.transform.SetParent(grid.transform);

        RenderMap.Render(GenerateMapArray.Generate(singlePlatform.x, singlePlatform.y, false), tilemapInstance, ruleTile);
    }

    void Start()
    {

    }

    void Update()
    {
        // if(player.transform.position >) TU ZACZĄĆ
    }
}
