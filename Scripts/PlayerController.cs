using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public GameObject bulletPrefab;
    public float fireRate = 0.4f;
    private float nextFire = 0f;
    private float screenBound = 8.5f;

    void Update()
    {
        // Get left/right input (arrow keys or A/D)
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * move * speed * Time.deltaTime);

        // Clamp so the ship can't leave the screen
        float clampedX = Mathf.Clamp(transform.position.x, -screenBound, screenBound);
        transform.position = new Vector3(clampedX, transform.position.y, 0);

        // Fire on spacebar, limited by fire rate
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // Spawn bullet slightly above the player so it doesn't overlap
            Vector3 spawnPos = transform.position + Vector3.up * 0.6f;
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

            // Play shoot sound if GameManager exists
            if (GameManager.instance != null)
                GameManager.instance.PlayShoot();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to enemy bullets
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            if (GameManager.instance != null)
                GameManager.instance.PlayerHit();
        }
    }
}
