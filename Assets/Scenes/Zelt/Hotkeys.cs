using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotkeys : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Application.LoadLevel("Zelt");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Zelt");   
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("ASDF");
            Application.Quit();
        }
    }
}
