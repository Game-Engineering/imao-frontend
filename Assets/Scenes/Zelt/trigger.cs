using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

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
    private string antwort;
    private bool patientDa = false;
    private string url = "localhost:8080/imao/api/spiel/";


    

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

    public void rufePatient()
    {
        StartCoroutine(getPatient());
        Debug.Log(antwort);
    }

    IEnumerator getPatient()
    {
        UnityWebRequest aufruf = new UnityWebRequest(url + "getPatient");
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
        }
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!patientDa)
            {
                rufePatient();
            }
            else
            {
                Debug.Log("Patient bereits vorhanden");
                Debug.Log(antwort);
            }
        }

  /*      if (hovered)
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
        */
    }
}
