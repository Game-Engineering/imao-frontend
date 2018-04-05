using System.Collections;
using System.Collections.
Generic;
using UnityEngine;
public class trigger : MonoBehaviour
{

    public Renderer rend;
    public Canvas can;

    private Color startColor;
    private bool hovered = false;
    private UnityEngine.UI.Text clicker;


    private void Start()
    {
        startColor = rend.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        rend.material.color += new Color(0.2f, 0.2f, 0.2f);
        can.gameObject.SetActive(true);
        hovered = true;
        clicker = can.GetComponentInChildren<UnityEngine.UI.Text>();
    }

    private void OnTriggerExit(Collider other)
    {
      rend.material.color = startColor;
      can.gameObject.SetActive(false);
      hovered = false;
      clicker.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (hovered)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicker.gameObject.SetActive(false);
            }
        }
    }

}
