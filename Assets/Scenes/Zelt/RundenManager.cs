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
        if (!Variablen.patientVorhanden && !Variablen.patientInZelt && !Variablen.patientGeht)
            StartCoroutine(sendeRequestRunde("neueRunde"));
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
            Variablen.runde = JsonUtility.FromJson<Runde>(aufruf.downloadHandler.text);

            GameObject.Find("GeldWert").GetComponent<Text>().text = Variablen.runde.budget + "";
            GameObject.Find("RundeWert").GetComponent<Text>().text = Variablen.runde.runde + "";
            GameObject.Find("PatientenWert").GetComponent<Text>().text = Variablen.runde.wartendePatienten + "";
            GameObject.Find("RufWert").GetComponent<Text>().text = Variablen.runde.ruf + "";
            GameObject.Find("RufePatienten").GetComponent<Button>().interactable = true;

        }
    }

    private void rundeNeuladen()
    {
        GameObject.Find("GeldWert").GetComponent<Text>().text = Variablen.runde.budget + "";
        GameObject.Find("RundeWert").GetComponent<Text>().text = Variablen.runde.runde + "";
        GameObject.Find("PatientenWert").GetComponent<Text>().text = Variablen.runde.wartendePatienten + "";
        GameObject.Find("RufWert").GetComponent<Text>().text = Variablen.runde.ruf + "";
    }

    private void Start()
    {
        InvokeRepeating("rundeNeuladen", 5, 5);
    }
}
