using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] int maxSplits = 3;
    public int alreadySplitted = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);

            if (alreadySplitted < maxSplits)
            {
                CreateCloneOnDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    void CreateCloneOnDeath()
    {
        alreadySplitted++;
        var instance = Instantiate(gameObject);
        Blob blobInstance = instance.GetComponent<Blob>();
        blobInstance.alreadySplitted = alreadySplitted;
    }
}

