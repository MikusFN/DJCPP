using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject menuInit;
    public GameObject MainCamera;
    public GameObject background;
    public GameObject gameplayUI;
    public GameObject settingsUI;
    public GameObject gameOverUI;
    public GameObject soundIconON;
    public GameObject soundIconOFF;
    public Text soundText;
    public GameObject scoreTextUI;
    public GameObject scoreBox;


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

        scoreTextUI.GetComponent<ScoreScript>().Score = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateGameManagerState() {

        switch(GMState) {
            case GameManagerState.Opening:

            settingsUI.SetActive(false);
            gameOverUI.SetActive(false);
            menuInit.SetActive(true);
            background.SetActive(false);
            gameplayUI.SetActive(false);
            scoreBox.SetActive(false);
            scoreTextUI.GetComponent<ScoreScript>().Score = 0;

            
                break;
            case GameManagerState.Gameplay:

            menuInit.SetActive(false);
            background.SetActive(true);
            gameplayUI.SetActive(true);
            settingsUI.SetActive(false);
            gameOverUI.SetActive(false);
            scoreBox.SetActive(true);

            

                break;
            case GameManagerState.Pause:

            menuInit.SetActive(false);
            background.SetActive(false);
            gameplayUI.SetActive(true);
            settingsUI.SetActive(true);
            gameOverUI.SetActive(false);
            scoreBox.SetActive(false);


                break;
            case GameManagerState.GameOver:

            menuInit.SetActive(false);
            background.SetActive(false);
            gameplayUI.SetActive(false);
            settingsUI.SetActive(false);
            gameOverUI.SetActive(true);
            scoreBox.transform.Translate(Vector3.right * 372);
            scoreBox.SetActive(true);


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

    public void ExitGamePlay() {
        GMState = GameManagerState.Opening;
        UpdateGameManagerState();
        scoreBox.transform.position = new Vector3(110,350,0);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SoundON() {
        soundIconON.SetActive(true);
        soundIconOFF.SetActive(false);
        soundText.text = "ON";
    }

    public void SoundOFF() {
        soundIconON.SetActive(false);
        soundIconOFF.SetActive(true);
        soundText.text = "OFF";
    }


}
