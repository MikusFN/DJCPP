using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject[] spriteObstaclesPrefab;
    public GameObject mainCamera;

    private int numObstacles;
    private List<GameObject> obstacles;
    private float startPosX, startPosY;
    CameraController camCont;
    private int startObstacles;

    public int NumObstacles { get => numObstacles; set => numObstacles = value; }

    // Start is called before the first frame update
    void Awake()
    {
        NumObstacles = 4;

        obstacles = new List<GameObject>();

        for (int i = 0; i < NumObstacles; i++)
        {
            GameObject go = Instantiate(spriteObstaclesPrefab[i % spriteObstaclesPrefab.Length], this.transform);
            go.transform.position = FindNewPosition();
            obstacles.Add(go);
        }
    }
    private void Start()
    {
        startObstacles = NumObstacles;

    }

    // Update is called once per frame
    void Update()
    {
        if (numObstacles > startObstacles)
        {
            for (int i = 0; i < (NumObstacles - startObstacles); i++)
            {
                GameObject go = Instantiate(spriteObstaclesPrefab[i % spriteObstaclesPrefab.Length], this.transform);
                go.transform.position = FindNewPosition();
                obstacles.Add(go);
            }
        }
        startObstacles = numObstacles;
    }

    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());

        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance)));
        return newPos;
    }

}
