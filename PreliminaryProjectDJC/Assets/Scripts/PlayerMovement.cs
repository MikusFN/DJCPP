using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public Camera mainCam;
    public float multSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    Vector2 mousePos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * multSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * multSpeed;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, true, mousePos);
        controller.Move(verticalMove * Time.fixedDeltaTime, false, mousePos);
    }
}
