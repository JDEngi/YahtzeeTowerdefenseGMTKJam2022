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
    public float diceCountdownTime = 5;
    private float currentWaveCountDownTime;
    private float currentDiceCountDownTime;

    // Wave number that sets the difficulty (basically)
    public int WaveNumber = 0;
    public float WaveTime = 10;

    // Game stats
    public static int score = 0;

    // Max dice
    public int MaxDice = 6;

    // Goal Health
    public int EnemyGoalHealth = 10;

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
        currentDiceCountDownTime = diceCountdownTime;

        // Now activated automatically, should be moved to a UI button
        //StartNewGame();
    }

    public void HandleStateStart()
    {
        Time.timeScale = 0;
        EnemyGoalReference.GetComponent<EnemyGoal>().HealthPoints = EnemyGoalHealth;

    }

    public void StartGameRun()
    {
        UpdateState(GameStates.RUN);

        // Clean up any enemy left on the field from a previous run
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
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
                HandleStateStart();
                break;
            case GameStates.RUN:
                Time.timeScale = 1;
                // Disable menus
                break;
            case GameStates.PAUSE:
                Time.timeScale = 0;
                // Pause all
                break;
            case GameStates.GAMEOVER:
                Time.timeScale = 0;
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

    public void RestartGame()
    {
        UpdateState(GameStates.START);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameStates.RUN)
        {
            UpdateWave();
            UpdateDiceAdd();

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
            DiceRoller diceRoller = GameObject.FindObjectOfType<DiceRoller>();
            if (diceRoller)
            {
                if (diceRoller.GetDiceCount() < MaxDice)
                {
                    diceRoller.AddDice();
                }
            }
            currentDiceCountDownTime = diceCountdownTime;
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

    internal static void AddScore(int killvalue)
    {
        score += killvalue;
    }
}


public enum GameStates { START, RUN, PAUSE, GAMEOVER };
