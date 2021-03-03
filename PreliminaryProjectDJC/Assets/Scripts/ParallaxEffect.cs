using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    private float lenghtSprite, heightSprite, startPosX, startPosY, bSpriteLenght, bSpriteHeight;

    public GameObject MainCamera;
    public GameObject background;
    public float effectStrenght;


    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        lenghtSprite = GetComponent<SpriteRenderer>().bounds.size.x;
        heightSprite = GetComponent<SpriteRenderer>().bounds.size.y;

        bSpriteLenght = background.GetComponent<SpriteBackgroundManager>().BiggestSpriteLenght;
        bSpriteHeight = background.GetComponent<SpriteBackgroundManager>().BiggestSpriteHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = MainCamera.transform.position.y * (1 - effectStrenght);
        float dist = MainCamera.transform.position.y * effectStrenght;

        transform.position = new Vector3(startPosX, startPosY + dist, transform.position.z);

        if (!IsInsideScreen(heightSprite))
        {
            if (temp > startPosY + heightSprite)
            {
                startPosY += 2 * bSpriteHeight;
                startPosX = Random.Range(-(bSpriteLenght), (bSpriteLenght));
            }
        }
    }

    private bool IsInsideScreen(float spriteHeight)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(MainCamera.GetComponent<Camera>());

        return  GeometryUtility.TestPlanesAABB(planes, GetComponent<SpriteRenderer>().bounds);
        //return GetComponent<SpriteRenderer>().isVisible;   
    }
}
