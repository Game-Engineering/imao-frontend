using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventsT : MonoBehaviour {

    private bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }


    public Renderer rend;
    public Canvas katalog;

    private Color startColor;
    
    void OnMouseEnter()
    {
        startColor = rend.material.color;
         if ( (Camera.main.transform.position - transform.position).sqrMagnitude < 3 ) {
            rend.material.color += new Color(0.2f,0.2f,0.2f);
            selected = true;
        }
    }

    private void OnMouseDown()
    {
        if(selected)
        {
            katalog.enabled = !katalog.enabled;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    private void OnMouseExit()
    {
        rend.material.color = startColor;
        selected = false;
    }
    
}
