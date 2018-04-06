using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody rb;
    private Vector3 movementVector;

    public float speedH = 2.0f;
    private float yaw = 0.0f;
    private bool locked;

    // Use this for initialization
    void Start()
    {
        locked = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (locked)
        {
            //Flat
            if (Input.GetKey(KeyCode.W))
            {
                movementVector = transform.forward * Time.deltaTime;
                movementVector.y -= movementVector.y;
                rb.transform.position += movementVector;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector = transform.forward * Time.deltaTime;
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


            //Rotation
            
                yaw += speedH * Input.GetAxis("Mouse X"); //Horizontale Mausbewegung in Kamerabewegung  

                transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            locked = !locked;
           // Debug.Log(locked);
        }
    }
}
