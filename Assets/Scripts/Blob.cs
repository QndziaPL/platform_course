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
                Destroy(gameObject);
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
        var instance = Instantiate(gameObject, transform, true);
        instance.transform.localScale = new Vector3((maxSplits - alreadySplitted)/3,(maxSplits - alreadySplitted)/3);
        Blob blobInstance = instance.GetComponent<Blob>();
        blobInstance.alreadySplitted = alreadySplitted;
    }
}

