using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamerabewegung : MonoBehaviour
{

    public Camera kopf;
    public float speedH = 2.0f, speedV = 2.0f; //Horizontale und Vertikale Mausgeschwindigkeit

    private Vector3 kopfbewegung;
    private float yaw = 0.0f, pitch = 0.0f;





    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch += speedV * (-1) * Input.GetAxis("Mouse Y");

            if (pitch <= -85)
                pitch = -85;
            if (pitch >= 45)
                pitch = 45;
            if (yaw <= -100)
                yaw = -100;
            if (yaw >= 100)
                yaw = 100;

            kopf.transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);

        }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

}
