using UnityEngine;

public static class ManagerBootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureManagers()
    {
        // GameStateManager
        if (GameStateManager.Instance == null)
        {
            var go = new GameObject("GameStateManager");
            go.AddComponent<GameStateManager>();
            Object.DontDestroyOnLoad(go);
        }

        // ScoreManager
        if (ScoreManager.Instance == null)
        {
            var go = new GameObject("ScoreManager");
            go.AddComponent<ScoreManager>();
            Object.DontDestroyOnLoad(go);
        }

        // SpeedManager
        if (SpeedManager.Instance == null)
        {
            var go = new GameObject("SpeedManager");
            go.AddComponent<SpeedManager>();
            Object.DontDestroyOnLoad(go);
        }

        // PoolManager
        if (PoolManager.Instance == null)
        {
            var go = new GameObject("PoolManager");
            go.AddComponent<PoolManager>();
            Object.DontDestroyOnLoad(go);
        }
    }
}
