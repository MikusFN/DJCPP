using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject[] spritePicksPrefab;
    public GameObject mainCamera;

    private int numPick = 6;
    private List<GameObject> pickables;
    private float startPosX, startPosY;
    CameraController camCont;

    private void Awake()
    {
        pickables = new List<GameObject>();

        for (int i = 0; i < numPick; i++)
        {
            if (spritePicksPrefab.Length > 0)
            {
                GameObject go = Instantiate(spritePicksPrefab[i%spritePicksPrefab.Length], this.transform);
                go.transform.position = FindNewPosition();
                go.GetComponent<Pickable>().PpType = (PPUpType)(i%6);
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
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance * 4.0f)));
        return newPos;
    }

}
