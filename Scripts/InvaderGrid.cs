using UnityEngine;

public class InvaderGrid : MonoBehaviour
{
    public GameObject invaderPrefab;
    public int rows = 4, cols = 9;
    public float xSpacing = 1.2f, ySpacing = 1f;
    public float moveSpeed = 1.5f;
    public float dropAmount = 0.4f;
    public float loseHeight = -3f; // Invaders reaching this Y = game over

    private float direction = 1f;
    private float edgeLimit = 7f;
    private float edgeCooldown = 0f; // Prevents jitter at edges
    private bool gameEnded = false;

    void Start()
    {
        // Spawn the grid of invaders
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                Vector3 pos = transform.position +
                    new Vector3(c * xSpacing, -r * ySpacing, 0);
                Instantiate(invaderPrefab, pos, Quaternion.identity, transform);
            }
    }

    void Update()
    {
        if (gameEnded) return;

        // WIN: all invaders destroyed
        if (transform.childCount == 0)
        {
            gameEnded = true;
            if (GameManager.instance != null)
                GameManager.instance.Victory();
            return;
        }

        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Tick down the edge cooldown
        if (edgeCooldown > 0f)
        {
            edgeCooldown -= Time.deltaTime;
        }
        else
        {
            // Check if any invader has hit the edge
            foreach (Transform child in transform)
            {
                if (child.position.x > edgeLimit || child.position.x < -edgeLimit)
                {
                    direction *= -1f;
                    transform.Translate(Vector2.down * dropAmount);
                    edgeCooldown = 0.25f; // Don't flip again for 0.25s
                    break;
                }

                // LOSE: invaders have reached the player's level
                if (child.position.y < loseHeight)
                {
                    gameEnded = true;
                    if (GameManager.instance != null)
                        GameManager.instance.GameOver();
                    return;
                }
            }
        }

        // Speed up as invaders are destroyed
        int remaining = transform.childCount;
        moveSpeed = Mathf.Lerp(4f, 1.5f, remaining / (float)(rows * cols));
    }
}
