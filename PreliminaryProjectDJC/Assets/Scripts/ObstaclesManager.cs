using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject[] spriteObstaclesPrefab;
    public int numObstacles = 4;
    public GameObject mainCamera;

    private List<GameObject> obstacles;
    private float startPosX, startPosY;
    CameraController camCont;


    // Start is called before the first frame update
    void Awake()
    {
        obstacles = new List<GameObject>();

        for (int i = 0; i < numObstacles; i++)
        {
            GameObject go = Instantiate(spriteObstaclesPrefab[i % spriteObstaclesPrefab.Length], this.transform);
            go.transform.position = FindNewPosition();
            obstacles.Add(go);
        }
    }
    private void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < numObstacles; i++)
        //{
        //    if (mainCamera.TryGetComponent<CameraController>(out camCont))
        //    {
        //        if (obstacles[i].transform.position.y < mainCamera.transform.position.y
        //            &&
        //            !camCont.IsInsideScreen(obstacles[i].GetComponent<SpriteRenderer>().bounds))
        //        {
        //            obstacles[i].transform.position = FindNewPosition();
        //        }
        //    }
        //}
    }

    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());
        //startPosY += planes[3].distance;
        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance * 3.0f)));
        return newPos;
    }

}
