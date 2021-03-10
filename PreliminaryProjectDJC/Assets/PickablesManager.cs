using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject[] spritePicksPrefab;
    public GameObject mainCamera;

    private int numPick = 2;
    private List<GameObject> pickables;
    private float startPosX, startPosY;
    CameraController camCont;

    private void Awake()
    {
        pickables = new List<GameObject>();

        for (int i = 0; i < numPick; i++)
        {
            int rn = Random.Range(0, 2);
            if (spritePicksPrefab.Length > 0)
            {
                GameObject go = Instantiate(spritePicksPrefab[rn], this.transform);
                go.transform.position = FindNewPosition();
                go.GetComponent<Pickable>().PpType = (PPUpType)rn;
                //go.GetComponent<GameObstacle>().Damage *= (int)(go.GetComponent<SpriteRenderer>().bounds.size.x * go.GetComponent<SpriteRenderer>().bounds.size.y);
                pickables.Add(go);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());
        //startPosY += planes[3].distance;
        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance * 2)));
        return newPos;
    }

}
