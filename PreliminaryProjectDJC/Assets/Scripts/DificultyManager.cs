using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Dificulty
{
    VeryEasy = 1,
    Easy,
    Normal,
    Hard,
    VeryHard,
    Impossible
}

public class DificultyManager : MonoBehaviour
{
    public GameObject mainCamera;
    private ObstaclesManager obstacleManager;
    private PickablesManager pickUpManager;
    private EnemySpawner enemySpawner;

    private float currentTime;
    private int currentMin;
    private int lastMin;
    private Dificulty currentSetting;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
        currentMin = 0;
        currentSetting = Dificulty.VeryEasy;
        obstacleManager = GetComponentInChildren<ObstaclesManager>();
        pickUpManager = GetComponentInChildren<PickablesManager>();
        enemySpawner = GetComponentInChildren<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.fixedDeltaTime;
        currentMin = (int)(currentTime / 60);
        SetDificulty();
        lastMin = currentMin;
    }

    private void SetDificulty()
    {

        if (currentMin != lastMin)
        {
            if (currentSetting <= Dificulty.Impossible)
            {
                int aux = (int)currentSetting;
                aux++;
                currentSetting = (Dificulty)aux;
                ChangeManagers(currentSetting);
            }
        }
    }

    private void ChangeManagers(Dificulty current)
    {
        mainCamera.GetComponent<CameraController>().Velocity += (int)current;
        obstacleManager.NumObstacles += (int)current;
        pickUpManager.NumPick += (int)current;
        enemySpawner.NumEnemies += (int)current;
    }
}
