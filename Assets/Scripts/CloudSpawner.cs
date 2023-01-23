using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    List<Cloud> listOfClouds = new List<Cloud>();
    [SerializeField] Cloud cloud;
    [SerializeField] int quantityOfClouds = 30;

    [SerializeField] float chanceForForegroundCloudSpawn = 0.2f;

    int spawnWidth = 30;
    int spawnHeight = 20;


    void Start()
    {

        for (int i = 0; i < quantityOfClouds; i++)
        {
            CreateCloud();
        }

    }

    void Update()
    {
        for (int i = 0; i < listOfClouds.Count; i++)
        {
            Cloud cld = listOfClouds[i];
            bool cloudLeftBackgroundLayerArea = !cld.boxCollider.IsTouchingLayers(LayerMask.GetMask("BackgroundImageTrigger"));
            if (cloudLeftBackgroundLayerArea)
            {

                DestroyImmediate(cld.gameObject);
                RemoveCloudFromListAndCreateNew(i);
            }
        }
    }

    void RemoveCloudFromListAndCreateNew(int index)
    {
        listOfClouds.RemoveAt(index);
        CreateCloud();
    }

    void CreateCloud()
    {
        var position = GetRandomCloudPosition();
        var newCloud = Instantiate(cloud, position, Quaternion.identity);
        newCloud.SetSortingLayer(PickProperSortingLayerForNewCloud());
        listOfClouds.Add(newCloud);
    }

    string PickProperSortingLayerForNewCloud()
    {
        return chanceForForegroundCloudSpawn >= Random.value ? "Clouds" : "CloudsFar";
    }

    Vector2 GetRandomCloudPosition()
    {
        var x = Random.Range(-spawnWidth, spawnWidth);
        var y = Random.Range(-spawnHeight, spawnHeight);
        return new Vector2(x, y);
    }
}
