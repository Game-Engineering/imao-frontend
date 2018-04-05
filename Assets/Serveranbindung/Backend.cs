using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend : MonoBehaviour {
    private BackendStub backend = null;
    private int pollCounter = 0;

	// Use this for initialization
	void Start () {
        backend = new BackendStub();
        Debug.Log(backend.neuesSpiel());
        Debug.Log(backend.getAktuelleBelegung());
    }

    // Update is called once per frame
    void Update () {
        if (pollCounter >= Konstante.POLLING){
            pollCounter = 0;
            Debug.Log(backend.getAktuelleBelegung());
        }
        pollCounter++;
    }
}
