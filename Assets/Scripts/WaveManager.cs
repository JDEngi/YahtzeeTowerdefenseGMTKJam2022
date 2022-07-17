using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] public GameObject WaveNumberUI, WaveCountdownTimeUI;
    [SerializeField] public int BaseWaveTime = 10;
    [SerializeField] public SpawnPoint[] WaveSpawners;

    public static float WaveExponentDivFactor = 100;

    public int WaveNumber {
        get { return _waveNumber; } 
        set 
        {
            _waveNumber = value;
            WaveNumberUI.GetComponent<TMP_Text>().SetText("Wave: {0}", _waveNumber);
        }
    }
    private int _waveNumber;
    public float WaveCountdownTime {
        get { return _waveCountdownTime; } 
        set
        {
            _waveCountdownTime = value;
            WaveCountdownTimeUI.GetComponent<TMP_Text>().SetText("Until next wave: \n{0}", Mathf.RoundToInt(_waveCountdownTime));
        }
    }
    private float _waveCountdownTime;


    public event Action<int> WaveChanged;

    public void Awake()
    {
        GameManager.OnGameStateChanged += OnGameManagerOnStateChanged;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameManagerOnStateChanged;
    }

    public void OnGameManagerOnStateChanged(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.START:
                RestartWaves();
                break;
            case GameStates.RUN:
                break;
            case GameStates.PAUSE:
                break;
            case GameStates.GAMEOVER:
                break;
            default:
                break;
        }
    }

    public void RestartWaves()
    {
        WaveNumber = 0;
        WaveCountdownTime = BaseWaveTime / 2;
    }

    private void NextWave()
    {
        WaveNumber++;
        WaveChanged?.Invoke(WaveNumber);

        // Determine the time that is set for the next wave
        WaveCountdownTime = BaseWaveTime + (0.5f * WaveNumber);

        foreach (SpawnPoint waveSpawner in WaveSpawners)
        {
            Debug.LogFormat("Spawning {0} enemies", WaveNumber);
            waveSpawner.MakeWave(WaveNumber, 0.5f);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        RestartWaves();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep track of the wavetime
        WaveCountdownTime -= Time.deltaTime;


        // Check if the condition for the next wave is met
        if (WaveCountdownTime < 0)
        {
            NextWave();
        }
        
    }
}