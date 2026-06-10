using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public SpikeScript prefabSource;

    public void Initialize()
    {
        // kept for API compatibility; no per-spike generator needed anymore
    }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }
    }

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.State != GameState.Playing)
            return;

        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 5f;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Finish"))
        {
            if (PoolManager.Instance != null)
                PoolManager.Instance.Return(this);
            else
                Destroy(gameObject);
        }
    }
}
