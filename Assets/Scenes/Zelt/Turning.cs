using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {

    private Vector3 movementVector;

    public Rigidbody rb;

    public float speedH = 2.0f;
    public float mms = 2.0f;

    private float yaw = 0.0f;



	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void FixedUpdate () {

        yaw += speedH * Input.GetAxis("Mouse X"); //Horizontale Mausbewegung in Kamerabewegung

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            movementVector = transform.forward * mms * Time.deltaTime;
            movementVector.y -= movementVector.y;
            rb.transform.position += movementVector;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector = transform.forward * mms * Time.deltaTime;
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
}
