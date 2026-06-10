using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<SpikeScript, Queue<SpikeScript>> pools = new Dictionary<SpikeScript, Queue<SpikeScript>>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public SpikeScript Spawn(SpikeScript prefab, Vector3 position)
    {
        if (prefab == null)
            return null;

        if (pools.TryGetValue(prefab, out var q) && q.Count > 0)
        {
            while (q.Count > 0)
            {
                var inst = q.Dequeue();
                if (inst == null)
                    continue;

                inst.transform.position = position;
                inst.gameObject.SetActive(true);
                return inst;
            }
        }

        var obj = Instantiate(prefab, position, Quaternion.identity);
        obj.prefabSource = prefab;
        return obj;
    }

    public void Return(SpikeScript instance)
    {
        if (instance == null)
            return;

        var prefab = instance.prefabSource;
        if (prefab == null)
        {
            Destroy(instance.gameObject);
            return;
        }

        instance.gameObject.SetActive(false);

        if (!pools.TryGetValue(prefab, out var q))
        {
            q = new Queue<SpikeScript>();
            pools[prefab] = q;
        }

        q.Enqueue(instance);
    }
}
