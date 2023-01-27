using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] float maxTilt = 0.5f;
    [SerializeField] float tiltSpeed = 0.01f;

    [SerializeField] int pointsForPickup = 100;

    bool collected = false;
    float tilt = 0f;
    Vector2 startPosition;
    int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (tilt > maxTilt || tilt < 0)
        {
            direction *= -1;
        }
        tilt += tiltSpeed / 60 * direction;
        Vector2 newPosition = new Vector2(transform.position.x, startPosition.y + tilt);
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collected)
        {
            collected = true;
            FindObjectOfType<GameSession>().IncreaseScore(pointsForPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
