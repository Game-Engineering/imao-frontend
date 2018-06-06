using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Mails : MonoBehaviour
{

    public GameObject panelSenden;
    public GameObject panelErhalten;
    public GameObject panelMail;
    public Image list;
    public Text debug;

    private string antwort;
    private int counter = 0;

    public void zeigeSendbareMails()
    {
        StartCoroutine(getSendbareMails("getMoeglicheSendeMails"));
    }

    IEnumerator getSendbareMails(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();

        Debug.Log("TEST");

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Debug.Log(antwort);

            Variablen.sendeMailliste = JsonUtility.FromJson<SendeMailliste>(antwort);

            panelSenden.SetActive(true);
            // GameObject.Find("Lob").GetComponent<Text>().text = Variablen.sendeMailliste.LOB;
            GameObject.Find("Lob").GetComponentInChildren<Text>().text = Variablen.sendeMailliste.LOB;
            GameObject.Find("Default").GetComponentInChildren<Text>().text = Variablen.sendeMailliste.DEFAULT_MAIL;
            GameObject.Find("GeraeGekauft").GetComponentInChildren<Text>().text = Variablen.sendeMailliste.GERAET_GEKAUFT;
            GameObject.Find("Abmahnung").GetComponentInChildren<Text>().text = Variablen.sendeMailliste.ABMAHNUNG;

        }
    }

    IEnumerator sendeMail(string ID)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "sendeMail/" + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
    }

    public void sendeLob()
    {
        StartCoroutine(sendeMail("LOB"));
    }
    public void sendeAbmahnung()
    {
        StartCoroutine(sendeMail("ABMAHNUNG"));
    }
    public void sendeGekauft()
    {
        StartCoroutine(sendeMail("GERAET_GEKAUFT"));
    }
    public void sendeDefault()
    {
        StartCoroutine(sendeMail("DEFAULT_MAIL"));
    }

    public void zeigeErhalteneMails()
    {
        debug.text = "i got something";

        StartCoroutine(getErhalteneMails("getMails"));
    }

    IEnumerator getErhalteneMails(string schnittstelle)
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
            Variablen.postfach = JsonUtility.FromJson<Postfach>(antwort); //TODO HERE


            foreach (Button but in list.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.postfach.mailliste.Count)
                {
                    foreach (Text txt in but.GetComponentsInChildren<Text>())
                    {
                        if (txt.name == "Sender")
                        {
                            txt.text = Variablen.postfach.mailliste[counter].absender;
                        }
                        else
                        {
                            if (Variablen.postfach.mailliste[counter].mailInhalt.Length < 21)
                            {
                                txt.text = Variablen.postfach.mailliste[counter].mailInhalt;
                            } else
                            {
                                txt.text = Variablen.postfach.mailliste[counter].mailInhalt.Substring(0, 20) + "...";
                            }
                        }
                    }
                    //but.GetComponentInChildren<Text>().text = Variablen.postfach.mailliste[counter];

                    but.gameObject.SetActive(true);
                    counter++;
                }
                else
                {
                    but.GetComponentInChildren<Text>().text = "";
                    // but.enabled = false;
                    but.gameObject.SetActive(false);
                }
            }
            counter = 0;

            panelErhalten.SetActive(true);
        }
    }

    public void zeigeMailAn(int id)
    {
        foreach (Text txt in panelMail.GetComponentsInChildren<Text>(true))
        {
            if (txt.name == "Absender")
            {
                txt.text = Variablen.postfach.mailliste[id].absender;
            }
            if (txt.name == "Inhalt")
            {
                txt.text = Variablen.postfach.mailliste[id].mailInhalt;
            }
        }
        panelMail.SetActive(true);
    }

    public void schliesseSendenMails()
    {
        panelSenden.SetActive(false);
    }
    public void schliesseErhalteneMails()
    {
        panelErhalten.SetActive(false);
    }
    public void schliesseMail()
    {
        panelMail.SetActive(false);
    }
}
