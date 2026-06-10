using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private SpikeGenerator generator;
    public SpikeScript prefabSource;

    public void Initialize(SpikeGenerator gen)
    {
        generator = gen;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * generator.currentSpeed * Time.deltaTime);
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
