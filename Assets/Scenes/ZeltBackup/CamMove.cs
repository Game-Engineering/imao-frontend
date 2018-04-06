using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    public float speedV = 2.0f;
    public float speedH = 2.0f;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    public static CursorLockMode lockMode;

    private bool locked;

	// Use this for initialization
	void Start () {
        locked = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
        // transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane)), Vector3.up);

        if (locked) {
            pitch -= speedV * Input.GetAxis("Mouse Y"); //Vertikale Mausbewegung in Kamaerabewegung
            yaw += speedH * Input.GetAxis("Mouse X"); //Horizontale Mausbewegung in Kamerabewegung
        }
        if(pitch > 90)
        {
            pitch = 90;
        }
        if(pitch < -90)
        {
            pitch = -90;
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        /*  if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
          {
              //Screen.lockCursor = false;
              Cursor.lockState = CursorLockMode.None;
          //    Cursor.visible = true;
          }
          else
          {
              //Screen.lockCursor = true;
              Cursor.lockState = CursorLockMode.Confined;
              Cursor.lockState = CursorLockMode.Locked;
             // Cursor.visible = false;
          }
          */
        

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.lockState = CursorLockMode.Locked;
                locked = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                locked = false;
            }
            Debug.Log(Cursor.visible);
            Cursor.visible = !Cursor.visible;
        }
    }

}
