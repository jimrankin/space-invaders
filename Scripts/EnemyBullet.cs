using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        // Move downward (toward the player)
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Destroy when it leaves the bottom of screen
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    // No OnTriggerEnter2D needed here —
    // the PlayerController handles the collision from its side
}
