using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float nextShot;

    void Start()
    {
        // Random initial delay so not all invaders fire at once
        nextShot = Time.time + Random.Range(2f, 8f);
    }

    void Update()
    {
        if (Time.time > nextShot)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            nextShot = Time.time + Random.Range(3f, 9f);
        }
    }
}
