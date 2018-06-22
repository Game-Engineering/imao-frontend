using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexaScript : MonoBehaviour
{

    Color hovercolor;
    Color originalC;
    MeshRenderer mrenderer;
    GameObject hovered = null;

    public WikiNavigationScript wns;

    void Start()
    {
        mrenderer = GetComponent<MeshRenderer>();
        originalC = mrenderer.material.color;
        hovercolor = originalC + new Color(0.2f, 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && hovered != null)
        {
            wns.zeigeWikiWindow();
        }
    }

    private void OnMouseOver()
    {
        if (!Variablen.bildschirmactive)
        {
            mrenderer.material.color = hovercolor;
            hovered = gameObject;
        }
    }

    private void OnMouseExit()
    {
        mrenderer.material.color = originalC;
        hovered = null;
    }
}
