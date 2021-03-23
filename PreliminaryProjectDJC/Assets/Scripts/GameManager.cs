using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject menuInit;
    public GameObject MainCamera;
    public GameObject background;
    public GameObject player;
    public GameObject gameplayUI;
    public GameObject settingsUI;
    public GameObject settings;
    public GameObject gameOverUI;
    public GameObject soundIconONPause;
    public GameObject soundIconOFFPause;
    public Text soundTextPause;
    public GameObject soundIconONSettings;
    public GameObject soundIconOFFSettings;
    public Text soundTextSettings;
    public GameObject scoreTextUI;
    public GameObject scoreBox;
    public GameObject timeCounter;
    public GameObject timeBox;
    public GameObject backgroundSound;

    bool backMainMenu = false;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Pause,
        GameOver,
        Settings,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;

        scoreTextUI.GetComponent<ScoreScript>().Score = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateGameManagerState()
    {

        switch (GMState)
        {
            case GameManagerState.Opening:

                settingsUI.SetActive(false);
                gameOverUI.SetActive(false);
                menuInit.SetActive(true);
                background.SetActive(false);
                player.SetActive(false);
                gameplayUI.SetActive(false);
                scoreBox.SetActive(false);
                timeBox.SetActive(false);
                settings.SetActive(false);

                if (!backMainMenu)
                {
                    scoreTextUI.GetComponent<ScoreScript>().Score = 0;
                }

                backMainMenu = false;

                break;

            case GameManagerState.Settings:

                settingsUI.SetActive(false);
                gameOverUI.SetActive(false);
                menuInit.SetActive(false);
                background.SetActive(false);
                player.SetActive(false);
                gameplayUI.SetActive(false);
                scoreBox.SetActive(false);
                timeBox.SetActive(false);
                settings.SetActive(true);


                break;
            case GameManagerState.Gameplay:

                menuInit.SetActive(false);
                background.SetActive(true);
                player.SetActive(true);
                gameplayUI.SetActive(true);
                settingsUI.SetActive(false);
                gameOverUI.SetActive(false);
                scoreBox.SetActive(true);
                timeBox.SetActive(true);
                settings.SetActive(false);




                break;
            case GameManagerState.Pause:

                menuInit.SetActive(false);
                background.SetActive(false);
                player.SetActive(false);
                gameplayUI.SetActive(true);
                settingsUI.SetActive(true);
                gameOverUI.SetActive(false);
                scoreBox.SetActive(true);
                timeBox.SetActive(true);
                settings.SetActive(false);


                break;
            case GameManagerState.GameOver:

                menuInit.SetActive(false);
                background.SetActive(false);
                player.SetActive(false);
                gameplayUI.SetActive(false);
                settingsUI.SetActive(false);
                gameOverUI.SetActive(true);
                scoreBox.SetActive(true);
                timeBox.SetActive(true);
                settings.SetActive(false);

                timeCounter.GetComponent<TimeCounter>().StopTimeCounter();
                timeCounter.GetComponent<TimeCounter>().startTime = 0f;
                timeCounter.GetComponent<TimeCounter>().myTime = 0f;


                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        timeCounter.GetComponent<TimeCounter>().StartTimeCounter();
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ResumeGamePlay()
    {
        timeCounter.GetComponent<TimeCounter>().resumeTimeCounter();
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void PauseMenu()
    {
        timeCounter.GetComponent<TimeCounter>().StopTimeCounter();
        GMState = GameManagerState.Pause;
        UpdateGameManagerState();
    }

    public void SettingsMenu()
    {
        GMState = GameManagerState.Settings;
        UpdateGameManagerState();
    }


    public void ExitGamePlay()
    {
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
    }


    public void BackMainMenu()
    {
        backMainMenu = true;
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundON()
    {
        soundIconONPause.SetActive(true);
        soundIconOFFPause.SetActive(false);
        soundTextPause.text = "ON";
        soundIconONSettings.SetActive(true);
        soundIconOFFSettings.SetActive(false);
        soundTextSettings.text = "ON";

        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    public void SoundOFF()
    {

        soundIconONPause.SetActive(false);
        soundIconOFFPause.SetActive(true);
        soundTextPause.text = "OFF";
        soundIconONSettings.SetActive(false);
        soundIconOFFSettings.SetActive(true);
        soundTextSettings.text = "OFF";

        AudioListener.pause = true;
        AudioListener.volume = 0;
    }

}
