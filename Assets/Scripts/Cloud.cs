using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] List<Sprite> cloudSprites;
    SpriteRenderer spriteRenderer;
    [SerializeField] float cloudMinSize = 0.5f;

    [SerializeField] float cloudMaxSize = 3f;
    [SerializeField] float cloudMinSpeed = 0.1f;
    [SerializeField] float cloudMaxSpeed = 2f;
    int direction;
    float speed;
    float speedFactor = 0.001f;
    public BoxCollider2D boxCollider;

    void Awake()
    {
        SetDirection();
        SetSpeed();
        var sprite = cloudSprites[Random.Range(0, cloudSprites.Count)];
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.transform.localScale = GetRandomSize();
    }

    void Update()
    {
        var newPositionX = transform.position.x + speed * direction * speedFactor;
        transform.position = new Vector2(newPositionX, transform.position.y);
    }

    void SetDirection()
    {
        direction = Random.value > 0.5f ? 1 : -1;
    }

    void SetSpeed()
    {
        speed = Random.Range(cloudMinSpeed, cloudMaxSpeed);
    }

    Vector2 GetRandomSize()
    {
        var size = Random.Range(cloudMinSize, cloudMaxSize);
        return new Vector2(size, size);
    }

    public void SetSortingLayer(string layer)
    {
        spriteRenderer.sortingLayerName = layer;
    }
}
