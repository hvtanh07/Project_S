using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        UpdateGameState(GameState.GameStart);
    }
   
    public void UpdateGameState(GameState newState){
        state = newState;
        switch(newState){
            case GameState.Pausing:
                break;
            case GameState.GameOver:
                break;
            case GameState.GameStart:
                break;
            case GameState.GameActive:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState {
    Pausing,
    GameOver,
    GameStart,
    GameActive
}