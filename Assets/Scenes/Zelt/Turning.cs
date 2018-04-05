using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {

    public Rigidbody rb;

    public float speedH = 2.0f;

    private float yaw = 0.0f;



	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update () {

        yaw += speedH * Input.GetAxis("Mouse X"); //Horizontale Mausbewegung in Kamerabewegung

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);


    }
}
