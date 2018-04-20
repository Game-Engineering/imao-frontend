using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public Transform trans;
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("GO");
            do
            {
                Debug.Log("GO2");

                trans.right += new Vector3(-0.1f,0,0);
            } while (trans.position.x > -10);
        }

	}
}
