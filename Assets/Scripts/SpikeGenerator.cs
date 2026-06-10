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

    [Header("Speed Settings")]
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 18f;
    [SerializeField] private float speedMultiplier = 0.12f;
    public float currentSpeed { get; private set; }

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
        currentSpeed = minSpeed;
        SetNextTargetDistance();
    }

    private void Update()
    {
        if (currentSpeed < maxSpeed)
            currentSpeed += speedMultiplier * Time.deltaTime;

        distanceCounter += currentSpeed * Time.deltaTime;

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
            spike.Initialize(this);
    }

    private void SetNextTargetDistance()
    {
        float speedT = currentSpeed / maxSpeed;

        float baseDist = Mathf.Lerp(maxDistance, minDistance, speedT);

        float adjustedRandom = Mathf.Lerp(randomOffset, randomOffset * 0.35f, speedT);
        float offset = Random.Range(-adjustedRandom, adjustedRandom);

        float dist = baseDist + offset;

        float minSafeDistance = Mathf.Lerp(baseMinSafeDistance, maxMinSafeDistance, speedT);

        targetDistance = Mathf.Max(minSafeDistance, dist);
    }
}
