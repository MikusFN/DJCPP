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
            GameObject go = Instantiate(spriteObstaclesPrefab[Random.Range(0, numObstacles)], this.transform);
            go.transform.position = FindNewPosition();
            //go.GetComponent<GameObstacle>().Damage *= (int)(go.GetComponent<SpriteRenderer>().bounds.size.x * go.GetComponent<SpriteRenderer>().bounds.size.y);
            obstacles.Add(go);
        }
    }
    private void Start()
    {
        //startPosX = transform.position.x;
        //startPosY = transform.position.y;
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
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance*2)));
        return newPos;
    }

}
