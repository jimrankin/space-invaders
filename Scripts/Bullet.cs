using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        // Move upward
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Destroy bullet when it leaves the top of screen
        if (transform.position.y > 6f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy invader on hit
        if (other.CompareTag("Invader"))
        {
            // Play explosion sound
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
                GameManager.instance.PlayExplosion();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
