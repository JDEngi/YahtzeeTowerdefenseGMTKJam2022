using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make game manager a singleton instance
    private static GameManager _instance;

    public static event Action<GameStates> OnGameStateChanged;

    private GameStates State;

    // Spawners
    public SpawnPoint[] SpawnPoints;

    // Spawn timer that calls the spawn function
    public float initialCountDownTime = 5;
    private float currentWaveCountDownTime;
    private float currentDiceCountDownTime;

    // Wave number that sets the difficulty (basically)
    public int WaveNumber = 0;
    public float WaveTime = 10;

    public GameObject EnemyGoalReference;

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.Log("More than one instance of GameManager is attempted to be created");
            return;
        }

        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateState(GameStates.START);
        currentWaveCountDownTime = initialCountDownTime;
        currentDiceCountDownTime = initialCountDownTime;

        // Now activated automatically, should be moved to a UI button
        //StartNewGame();
    }

    public void StartNewGame()
    {
        UpdateState(GameStates.RUN);
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        UpdateState(GameStates.GAMEOVER);
    }

    void UpdateState(GameStates newState)
    {
        State = newState;

        switch (newState)
        {
            case GameStates.START:
                // Show Start menu?
                break;
            case GameStates.RUN:
                // Disable menus
                break;
            case GameStates.PAUSE:
                // Show pause menu
                break;
            case GameStates.GAMEOVER:
                // Show gameover screen
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void TogglePause()
    {
        if (State == GameStates.RUN)
        {
            UpdateState(GameStates.PAUSE);
        }
        else if (State == GameStates.PAUSE)
        {
            UpdateState(GameStates.RUN);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameStates.RUN)
        {
            UpdateWave();

            if (EnemyGoalReference.GetComponent<EnemyGoal>().HealthPoints <= 0)
            {
                GameOver();
            }
        }   

        if (Input.GetButtonDown("Menu"))
        {
            Debug.Log("Open menu");
            TogglePause();
        }
    }

    void UpdateDiceAdd()
    {
        currentDiceCountDownTime -= Time.deltaTime;

        if(currentDiceCountDownTime <= 0)
        {
            // Give the player an extra dice.
        }

    }

    void UpdateWave()
    {        
        // Update waves
        currentWaveCountDownTime -= Time.deltaTime;
        if (currentWaveCountDownTime <= 0)
        {
            WaveNumber += 1;

            foreach (SpawnPoint spawnPoint in SpawnPoints)
            {
                spawnPoint.MakeWave(WaveNumber, 0.5f);
            }
            currentWaveCountDownTime = WaveTime;
        }
    }
}


public enum GameStates { START, RUN, PAUSE, GAMEOVER };
