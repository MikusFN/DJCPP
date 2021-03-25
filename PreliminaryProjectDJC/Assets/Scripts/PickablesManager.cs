using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject[] spritePicksPrefab;
    public GameObject mainCamera;

    private int numPick;
    private int startNumPick;
    private int maxPicks;
    private List<GameObject> pickables;
    private float startPosX, startPosY;
    CameraController camCont;

    public int NumPick { get => numPick; set => numPick = value; }

    private void Awake()
    {
        maxPicks = 10;
        numPick = 2;

        pickables = new List<GameObject>();

        for (int i = 0; i < NumPick; i++)
        {
            if (spritePicksPrefab.Length > 0)
            {
                GameObject go = Instantiate(spritePicksPrefab[i % spritePicksPrefab.Length], this.transform);
                go.transform.position = FindNewPosition();
                go.GetComponent<Pickable>().PpType = (PPUpType)(i % spritePicksPrefab.Length);
                pickables.Add(go);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startNumPick = numPick;
    }

    // Update is called once per frame
    void Update()
    {
        if (numPick > startNumPick && numPick < maxPicks)
        {
            for (int i = 0; i < (NumPick - startNumPick); i++)
            {
                if (spritePicksPrefab.Length > 0)
                {
                    GameObject go = Instantiate(spritePicksPrefab[(i + 2) % spritePicksPrefab.Length], this.transform);
                    go.transform.position = FindNewPosition();
                    go.GetComponent<Pickable>().PpType = (PPUpType)((i + 2) % spritePicksPrefab.Length);
                    pickables.Add(go);
                }
            }
        }
        else if (numPick > startNumPick &&
            (!pickables.Exists(x => x.GetComponent<Pickable>().PpType == PPUpType.destroyer)
            || !pickables.Exists(x => x.GetComponent<Pickable>().PpType == PPUpType.Teleport)))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(spritePicksPrefab[(i + 5) % spritePicksPrefab.Length], this.transform);
                go.transform.position = FindNewPosition();
                go.GetComponent<Pickable>().PpType = (PPUpType)((i + 5) % spritePicksPrefab.Length);
                pickables.Add(go);
            }
        }
        startNumPick = numPick;
    }

    private Vector3 FindNewPosition()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera.GetComponent<Camera>());
        //startPosY += planes[3].distance;
        Vector3 newPos = new Vector3((Random.Range(-planes[0].distance, planes[0].distance)),
                transform.position.y + (Random.Range(planes[3].distance, planes[3].distance)));
        return newPos;
    }

}
