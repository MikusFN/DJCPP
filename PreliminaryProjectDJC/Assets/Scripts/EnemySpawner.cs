using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] EnemiesPrefab;
    public GameObject mainCamera;

    private int numEnemies;
    private int startNumEnemies;
    private List<GameObject> enemies;

    public int NumEnemies { get => numEnemies; set => numEnemies = value; }

    private void Awake()
    {
        NumEnemies = 2;

        enemies = new List<GameObject>();

        for (int i = 0; i < NumEnemies; i++)
        {
            GameObject go = Instantiate(EnemiesPrefab[i % EnemiesPrefab.Length], this.transform);
            go.transform.position = FindNewPosition();
            enemies.Add(go);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startNumEnemies = NumEnemies;
    }

    private void Update()
    {
        if (NumEnemies > startNumEnemies)
        {
            for (int i = 0; i < (NumEnemies - startNumEnemies); i++)
            {
                GameObject go = Instantiate(EnemiesPrefab[i % EnemiesPrefab.Length], this.transform);
                go.transform.position = FindNewPosition();
                enemies.Add(go);
            }
        }
        startNumEnemies = numEnemies;
    }

    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());
        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance)));
        return newPos;
    }
}
