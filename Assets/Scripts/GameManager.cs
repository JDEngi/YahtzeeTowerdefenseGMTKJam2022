using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameStates { START, RUN, PAUSE, GAMEOVER }
    private GameStates State;

    // Spawners
    public SpawnPoint[] SpawnPoints;

    // Spawn timer that calls the spawn function
    public float initialCountDownTime = 5;
    private float currentWaveCountDownTime;

    // Wave number that sets the difficulty (basically)
    public int WaveNumber = 0;
    public float WaveTime = 10;


    // Start is called before the first frame update
    void Start()
    {
        State = GameStates.START;
        currentWaveCountDownTime = initialCountDownTime;

        // Now activated automatically, should be moved to a UI button
        NewGame();
    }

    void NewGame()
    {
        State = GameStates.RUN;
    }

    void GameOver()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameStates.RUN)
        {
            UpdateWave();
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
