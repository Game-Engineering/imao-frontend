using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

/// <summary>
/// Erlaubt Zugriff auf die Szenenlogik durch Scripts
/// </summary>
public class SzenenManager : MonoBehaviour
{
    public UnityEngine.UI.Text input;

    public void setzeURL()
    {
        Konstanten.URL = input.text + "/imao/api/spiel/";
        Debug.Log(Konstanten.URL);
    }

    public void Beende()
    {
        IMAO.Instanz.Beende();
    }

    public void LadeHauptmenue()
    {
        IMAO.Instanz.LadeHauptmenue();
    }

    public void LadeZelt()
    {
        Variablen.arztOderWirtschaft = "arzt";
        StartCoroutine(sendeRequestErzeuge("erzeuge/arzt/Max/Mustermann/weiblich"));
        StartCoroutine(sendeRequestStart("start/" + Variablen.arztOderWirtschaft));
        Konstanten.URL += "medizin/";
        Debug.Log(Konstanten.URL);
        //   StartCoroutine(sendeRequestRunde("neueRunde"));
        IMAO.Instanz.LadeZelt();
    }

    public void LadeWirtschaft()
    {
        Variablen.arztOderWirtschaft = "manager";
        StartCoroutine(sendeRequestErzeuge("erzeuge/manager/Max/Mustermann/weiblich"));
        StartCoroutine(sendeRequestStart("start/" + Variablen.arztOderWirtschaft));

        Konstanten.URL += "wirtschaft/";
        Debug.Log(Konstanten.URL);
        //  StartCoroutine(sendeRequestRunde("neueRunde"));
        IMAO.Instanz.LadeWirtschaft();
    }

    IEnumerator sendeRequestErzeuge(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET

        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            Debug.Log("Error");
        }
        else
        {
            Variablen.spieler = aufruf.downloadHandler.text;
        }
    }

    IEnumerator sendeRequestStart(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET

        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            Debug.Log("gestartet");

        }
    }
    /*
    IEnumerator sendeRequestRunde(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            Variablen.runde = JsonUtility.FromJson<Runde>(aufruf.downloadHandler.text);
            Debug.Log(Variablen.runde);

            GameObject.Find("GeldWert").GetComponent<UnityEngine.UI.Text>().text = Variablen.runde.budget + "";
            GameObject.Find("RundeWert").GetComponent<UnityEngine.UI.Text>().text = Variablen.runde.runde + "";
            GameObject.Find("PatientenWert").GetComponent<UnityEngine.UI.Text>().text = Variablen.runde.wartendePatienten + "";
            GameObject.Find("RufWert").GetComponent<UnityEngine.UI.Text>().text = Variablen.runde.ruf + "";
        }
    }
    */

}
