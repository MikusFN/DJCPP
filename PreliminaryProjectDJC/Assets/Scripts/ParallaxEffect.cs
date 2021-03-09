using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    private float lenghtSprite, heightSprite, startPosX, startPosY, bSpriteLenght, bSpriteHeight;

    public GameObject MainCamera;
    public GameObject background;
    public float effectStrenght;


    CameraController camCont;
    private void Awake()
    {
        if (tag == "Obstacle")
        {
            MainCamera = GetComponentInParent<ObstaclesManager>().mainCamera.gameObject;
            background = GetComponentInParent<ObstaclesManager>().gameObject.GetComponentInParent<SpriteBackgroundManager>().gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        lenghtSprite = this.GetComponent<SpriteRenderer>().bounds.size.x;
        heightSprite = this.GetComponent<SpriteRenderer>().bounds.size.y;

        bSpriteLenght = background.GetComponent<SpriteBackgroundManager>().BiggestSpriteLenght;
        bSpriteHeight = background.GetComponent<SpriteBackgroundManager>().BiggestSpriteHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = MainCamera.transform.position.y * (1 - effectStrenght);
        float dist = MainCamera.transform.position.y * effectStrenght;

        transform.position = new Vector3(startPosX, startPosY + dist, transform.position.z);

        if (MainCamera.TryGetComponent<CameraController>(out camCont))
        {
            if (!camCont.IsInsideScreen(GetComponent<SpriteRenderer>().bounds))
            {
                if (temp > startPosY + heightSprite)
                {
                    startPosY += 2 * bSpriteHeight;
                    startPosX = Random.Range(-(bSpriteLenght), (bSpriteLenght));
                    if (tag == "Obstacle")
                    {
                        CapsuleCollider2D col;
                        if (TryGetComponent<CapsuleCollider2D>(out col))
                        {
                            col.isTrigger = true;
                            GetComponent<GameObstacle>().Life = GetComponent<GameObstacle>().InitialLife;
                            GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                }
            }
        }

    }

}
