using System;
using UnityEngine;

public enum GameState { Menu, Playing, Paused, GameOver }

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Menu;

    public event Action<GameState> OnStateChanged;

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

    public void SetState(GameState newState)
    {
        if (State == newState) return;
        State = newState;
        OnStateChanged?.Invoke(State);
    }
}
