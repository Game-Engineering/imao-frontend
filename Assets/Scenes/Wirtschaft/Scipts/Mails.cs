using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Mails : MonoBehaviour
{

    public GameObject panelSenden;
    public GameObject panelErhalten;

    private string antwort;

    public void zeigeSendbareMails()
    {
        StartCoroutine(getSendbareMails("getMoeglicheSendeMails"));
    }

    IEnumerator getSendbareMails(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Variablen.sendeMailliste = JsonUtility.FromJson<SendeMailliste>(antwort);

            panelSenden.SetActive(true);
            GameObject.Find("Lob").GetComponent<Text>().text = Variablen.sendeMailliste.LOB;
            GameObject.Find("Default").GetComponent<Text>().text = Variablen.sendeMailliste.DEFAULT_MAIL;
            GameObject.Find("GeraeGekauft").GetComponent<Text>().text = Variablen.sendeMailliste.GERAET_GEKAUFT;
            GameObject.Find("Abmahnung").GetComponent<Text>().text = Variablen.sendeMailliste.ABMAHNUNG;

        }
    }

    public void schliesseSendenMails()
    {
        panelSenden.SetActive(false);
    }

    IEnumerator sendeMail(string ID)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
    }

    public void sendeLob()
    {
        StartCoroutine(sendeMail("0"));
    }
    public void sendeAbmahnung()
    {
        StartCoroutine(sendeMail("1"));
    }
    public void sendeGekauft()
    {
        StartCoroutine(sendeMail("2"));
    }
    public void sendeDefault()
    {
        StartCoroutine(sendeMail("3"));
    }


    public void zeigeErhalteneMails()
    {
        StartCoroutine(getErhalteneMails("getMails"));
    }

    IEnumerator getErhalteneMails(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "getArztbreicht");
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        } else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.mailliste.Add(JsonUtility.FromJson<Mail>(antwort)); //TODO HERE
        }
    }
}
