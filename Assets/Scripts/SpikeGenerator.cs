using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    [Header("Spike Prefabs")]
    [SerializeField] private SpikeScript bottomSpikePrefab;
    [SerializeField] private SpikeScript topSpikePrefab;

    [Header("Spike Settings")]
    [SerializeField] private float topSpikeY = -3f;
    [SerializeField] private float bottomSpikeY = -4f;
    [SerializeField] private float chanceForTopSpawn = 0.3f;

    // Speed is now managed by SpeedManager

    [Header("Distance Settings")]
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 13f;
    [SerializeField] private float randomOffset = 0.8f;

    [SerializeField] private float baseMinSafeDistance = 6f;
    [SerializeField] private float maxMinSafeDistance = 10f;

    private float distanceCounter;
    private float targetDistance;

    private void Awake()
    {
        SetNextTargetDistance();
    }

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.State != GameState.Playing)
            return;

        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 5f;
        distanceCounter += speed * Time.deltaTime;

        if (distanceCounter >= targetDistance)
        {
            SpawnSpike();
            SetNextTargetDistance();
            distanceCounter = 0f;
        }
    }

    private void SpawnSpike()
    {
        bool top = Random.value < chanceForTopSpawn;

        SpikeScript prefab = top ? topSpikePrefab : bottomSpikePrefab;

        Vector3 pos = transform.position;
        pos.y = top ? topSpikeY : bottomSpikeY;

        SpikeScript spike = null;
        if (PoolManager.Instance != null)
        {
            spike = PoolManager.Instance.Spawn(prefab, pos);
        }
        else
        {
            spike = Instantiate(prefab, pos, Quaternion.identity);
            spike.prefabSource = prefab;
        }

        if (spike != null)
            spike.Initialize();
    }

    private void SetNextTargetDistance()
    {
        float current = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 5f;
        float maxSpeedLocal = SpeedManager.Instance != null ? SpeedManager.Instance.maxSpeed : 18f; // fallback

        float speedT = maxSpeedLocal > 0f ? current / maxSpeedLocal : 0f;

        float baseDist = Mathf.Lerp(maxDistance, minDistance, speedT);

        float adjustedRandom = Mathf.Lerp(randomOffset, randomOffset * 0.35f, speedT);
        float offset = Random.Range(-adjustedRandom, adjustedRandom);

        float dist = baseDist + offset;

        float minSafeDistance = Mathf.Lerp(baseMinSafeDistance, maxMinSafeDistance, speedT);

        targetDistance = Mathf.Max(minSafeDistance, dist);
    }
}
