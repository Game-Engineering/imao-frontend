using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BildschirmScript : MonoBehaviour {

    Color hovercolor;
    Color originalC;
    MeshRenderer mrenderer;
    GameObject hovered = null;

    public WindowManagerScript wms;


    // Use this for initialization
    void Start () {
        mrenderer = GetComponent<MeshRenderer>();
        originalC = mrenderer.material.color;
        hovercolor = originalC + new Color(0.2f, 0.2f, 0.2f);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) &&  hovered != null)
        {
            wms.startePC();
        }
	}

    private void OnMouseOver()
    {
        mrenderer.material.color = hovercolor;
        hovered = gameObject;
    }

    private void OnMouseExit()
    {
        mrenderer.material.color = originalC;
        hovered = null;
    }

}
