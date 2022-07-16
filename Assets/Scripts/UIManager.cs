using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _StartScreen, _GameScreen, _PauseScreen, _GameOverScreen;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameMananagerGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameMananagerGameStateChanged;
    }

    void OnGameMananagerGameStateChanged(GameStates newState)
    {
        _StartScreen.SetActive(newState == GameStates.START);
        _GameScreen.SetActive(newState == GameStates.RUN);
        _PauseScreen.SetActive(newState == GameStates.PAUSE);
        _GameOverScreen.SetActive(newState == GameStates.GAMEOVER);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
