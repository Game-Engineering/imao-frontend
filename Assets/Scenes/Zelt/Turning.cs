using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{

    private Vector3 movementVector;

    public Rigidbody rb;

    public float speedH = 2.0f;
    public float movementSpeed = 2.0f;

    private float yaw = 0.0f;



    // Use this for initialization
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Variablen.kameraFest = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Variablen.kameraFest)
        {
            yaw += speedH * Input.GetAxis("Mouse X"); //Horizontale Mausbewegung in Kamerabewegung

            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movementVector = transform.forward * (movementSpeed + 4) * Time.deltaTime;
                    movementVector.y -= movementVector.y;
                    rb.transform.position += movementVector;
                }
                else
                {
                    movementVector = transform.forward * movementSpeed * Time.deltaTime;
                    movementVector.y -= movementVector.y;
                    rb.transform.position += movementVector;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector = transform.forward * movementSpeed * Time.deltaTime;
                movementVector.y -= movementVector.y;
                rb.transform.position -= movementVector;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.transform.position -= transform.right * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.transform.position += transform.right * Time.deltaTime;
            }
        }


        /*
         * Mauszeiger zentrieren bzw dezentrieren um auswahl zu erschaffen
        */
        if ((Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKeyDown(KeyCode.Escape)) && !Variablen.dialogOffen)
        {
            mouseLock();
        }


    }

    void mouseLock()
    {
        if (Variablen.kameraFest)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Variablen.kameraFest = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
            Variablen.kameraFest = true;
        }
    }
}
