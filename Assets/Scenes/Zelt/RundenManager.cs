using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RundenManager : MonoBehaviour
{

    public Canvas panel;

    public void starteNeueRunde()
    {
        if (Variablen.arztOderWirtschaft == "arzt")
        {
            if (!Variablen.patientVorhanden && !Variablen.patientInZelt && !Variablen.patientGeht)
                StartCoroutine(sendeRequestRunde("neueRunde"));
        } else
        {
            StartCoroutine(sendeRequestRundeWirtschaft("neueRunde"));
        }
    }

    IEnumerator sendeRequestRundeWirtschaft(string schnittstelle)
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
            Variablen.rundeWirtschaft = JsonUtility.FromJson<RundeWirtschaft>(aufruf.downloadHandler.text);

            GameObject.Find("GeldWert").GetComponent<Text>().text = Variablen.rundeWirtschaft.budget + "";
            GameObject.Find("RundeWert").GetComponent<Text>().text = Variablen.rundeWirtschaft.runde + "";
            
            GameObject.Find("RufWert").GetComponent<Text>().text = Variablen.rundeWirtschaft.ruf + "";

        }
    }

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
            Variablen.rundeArzt = JsonUtility.FromJson<RundeArzt>(aufruf.downloadHandler.text);

            GameObject.Find("GeldWert").GetComponent<Text>().text = Variablen.rundeArzt.budget + "";
            GameObject.Find("RundeWert").GetComponent<Text>().text = Variablen.rundeArzt.runde + "";
            if (Variablen.arztOderWirtschaft == "arzt")
            {
                GameObject.Find("PatientenWert").GetComponent<Text>().text = Variablen.rundeArzt.wartendePatienten + "";
                GameObject.Find("RufePatienten").GetComponent<Button>().interactable = true;
            }
            GameObject.Find("RufWert").GetComponent<Text>().text = Variablen.rundeArzt.ruf + "";

        }
    }

    private void rundeNeuladen()
    {
        GameObject.Find("GeldWert").GetComponent<Text>().text = Variablen.rundeArzt.budget + "";
        GameObject.Find("RundeWert").GetComponent<Text>().text = Variablen.rundeArzt.runde + "";
        if (Variablen.arztOderWirtschaft == "arzt")
        {
            GameObject.Find("PatientenWert").GetComponent<Text>().text = Variablen.rundeArzt.wartendePatienten + "";
        }
        GameObject.Find("RufWert").GetComponent<Text>().text = Variablen.rundeArzt.ruf + "";
    }

    private void Start()
    {
        //InvokeRepeating("rundeNeuladen", 5, 5);
    }
}
