using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ColorHexUtility;

public class PickableIcon : MonoBehaviour
{

    Image currentImage;

    public Sprite life; // Drag your first sprite here
    public Sprite shield;
    public Sprite rateOfFire;
    public Sprite shotsNum;
    public Sprite teleport;
    public Sprite destroyer;
    public Sprite none;

    // Start is called before the first frame update
    void Start()
    {
        currentImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeIcon(string icon) {

        if (icon == "life") {
            currentImage.sprite = life;
        }

        else if (icon == "shield") {
            currentImage.sprite = shield;
        }

        else if (icon == "rateOfFire") {
            currentImage.sprite = rateOfFire;
        }

        else if (icon == "shotsNum") {
            currentImage.sprite = shotsNum;
            currentImage.color = new ColorHex("#504FC0");
        }

        else if (icon == "teleport") {
            currentImage.sprite = teleport;
            currentImage.color = new ColorHex("#29D6C3");
        }

        else if (icon == "destroyer") {
            currentImage.sprite = destroyer;
        }
    }

        public void emptyIcon() {

            currentImage.sprite = none;

    }
}
