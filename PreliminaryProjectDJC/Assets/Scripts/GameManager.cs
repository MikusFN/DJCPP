using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject menuInit;
    public GameObject MainCamera;
    public GameObject background;
    public GameObject gameplayUI;
    public GameObject settingsUI;
    public GameObject gameOverUI;

    public enum GameManagerState {
        Opening,
        Gameplay,
        Pause,
        GameOver,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateGameManagerState() {

        switch(GMState) {
            case GameManagerState.Opening:

            gameplayUI.SetActive(false);
            settingsUI.SetActive(false);
            gameOverUI.SetActive(false);
            
                break;
            case GameManagerState.Gameplay:

            menuInit.SetActive(false);
            background.SetActive(true);
            gameplayUI.SetActive(true);
            settingsUI.SetActive(false);
            gameOverUI.SetActive(false);

                break;
            case GameManagerState.Pause:

            menuInit.SetActive(false);
            background.SetActive(false);
            gameplayUI.SetActive(true);
            settingsUI.SetActive(true);
            gameOverUI.SetActive(false);


                break;
            case GameManagerState.GameOver:

            menuInit.SetActive(false);
            background.SetActive(true);
            gameplayUI.SetActive(false);
            settingsUI.SetActive(false);
            gameOverUI.SetActive(true);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state) {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay() {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void SettingsMenu() {
        GMState = GameManagerState.Pause;
        UpdateGameManagerState();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
