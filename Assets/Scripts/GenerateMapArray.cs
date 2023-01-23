using Unity;
using UnityEngine;
public static class GenerateMapArray
{

    public static int[,] Generate(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }

        for (int i = 0; i < map.GetLength(0); i++)
        {
            Debug.Log(map[i, 0]);
        }
        return map;
    }
}

