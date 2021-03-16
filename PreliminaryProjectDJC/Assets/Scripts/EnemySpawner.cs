using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] EnemiesPrefab;
    public GameObject mainCamera;
    public int numEnemies = 2;

    private List<GameObject> enemies;


    public float timer = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", timer);
    }


    void Spawn() {

        enemies = new List<GameObject>();

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject go = Instantiate(EnemiesPrefab[Random.Range(0, numEnemies)], this.transform);
            go.transform.position = FindNewPosition();
            enemies.Add(go);
        }

        Invoke("Spawn", timer);
    }


    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());
        //startPosY += planes[3].distance;
        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance*2)));
        return newPos;
    }
}
