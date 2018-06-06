using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManagerScript : MonoBehaviour {

    public GameObject pc;

	// Use this for initialization
	void Start () {
		
	}
	
    public void startePC()
    {
        pc.SetActive(true);
    }
    public void beendePC()
    {
        pc.SetActive(false);
    }


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T))
        {
            startePC();
        }
	}
}
