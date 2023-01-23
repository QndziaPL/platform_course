using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public static class RenderMap
{
    public static void Render(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= map.GetUpperBound(1); y++)
            {
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}
