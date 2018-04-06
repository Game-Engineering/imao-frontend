using System.Collections;
using System.Collections.
Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{

    public Renderer rend;
    public Canvas can;
    public Canvas dialogBox;
    public BackendAnbindung other;

    private Color startColor;
    private bool hovered = false;
    private UnityEngine.UI.Text clicker;
    private bool dialogGestartet;


    public Dialog dialog;

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
        rend.material.color = startColor; //Farbe ändern
        can.gameObject.SetActive(false); //Canvas mit klicke deaktivieren
        hovered = false;
        clicker.gameObject.SetActive(true);
        dialogGestartet = false;                // dialog ausblenden
        dialogBox.gameObject.SetActive(false);  // und zurücksetzen
    }

    private void Update()
    {

        if (hovered)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicker.gameObject.SetActive(false);

                //randomuser test für Backendanbidung
                other.randomUser();

                //Dialog fortsetzen falls bereits gestartet
                if (dialogGestartet)
                {
                    //ZeigeNächstenSatz ist bool, falls kein Satz mehr in Queue wird false zurückgegeben
                    if (!FindObjectOfType<DialogManager>().ZeigeNächstenSatz())
                    {
                        dialogGestartet = false;
                    }
                }
                //Dialog startet falls noch nicht gestartet
                if (!dialogGestartet)
                {
                    
                    FindObjectOfType<DialogManager>().StarteDialog(dialog);
                    dialogGestartet = true;
                }
                

            }
        }
    }



}
