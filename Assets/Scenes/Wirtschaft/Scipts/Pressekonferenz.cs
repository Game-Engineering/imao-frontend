using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Pressekonferenz : MonoBehaviour
{
    public Canvas can;
    public GameObject pkPanel;
    public Text text;
    public Text dbg;

    private int counter = 0;
    private string antwort;

    public void haltePK()
    {
        dbg.text = Variablen.rundeWirtschaft.aufgabe + "";
        if (Variablen.rundeWirtschaft.aufgabe.Contains("PRESSE"))
        {
            StartCoroutine("getPK");
        }
        //StartCoroutine(getPK());
    }

    IEnumerator getPK()
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "startePressekonferenz");
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Variablen.pressekonferenz = JsonUtility.FromJson<Pressekonferenzobjekt>(aufruf.downloadHandler.text);

            if (Variablen.pressekonferenz.status != "fehler")
            {
                if (Variablen.pressekonferenz.status != "ENDE")
                {
                    pkPanel.GetComponentInChildren<Text>().text = Variablen.pressekonferenz.frage;
                    foreach (Button but in pkPanel.GetComponentsInChildren<Button>(true))
                    {
                        if (counter < Variablen.pressekonferenz.antworten.Count)
                        {
                            but.GetComponentInChildren<Text>().text = Variablen.pressekonferenz.antworten[counter];
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
                    can.gameObject.SetActive(true);
                }
                else
                {
                    can.gameObject.SetActive(false);
                }
                counter = 0;
            }
        }

    }

    public void clickB1()
    {
        StartCoroutine(fuehrePKfort(0));
    }
    public void clickB2()
    {
        StartCoroutine(fuehrePKfort(1));
    }
    public void clickB3()
    {
        StartCoroutine(fuehrePKfort(2));
    }
    IEnumerator fuehrePKfort(int ID)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "pressekonferenz/" + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Variablen.pressekonferenz = JsonUtility.FromJson<Pressekonferenzobjekt>(aufruf.downloadHandler.text);

            if (Variablen.pressekonferenz.status != "fehler")
            {
                if (Variablen.pressekonferenz.status != "ENDE")
                {
                    pkPanel.GetComponentInChildren<Text>().text = Variablen.pressekonferenz.frage;
                    foreach (Button but in pkPanel.GetComponentsInChildren<Button>(true))
                    {
                        if (counter < Variablen.pressekonferenz.antworten.Count)
                        {
                            but.GetComponentInChildren<Text>().text = Variablen.pressekonferenz.antworten[counter];
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
                    can.gameObject.SetActive(true);
                }
                else
                {
                    can.gameObject.SetActive(false);
                }
                counter = 0;
            }
        }
    }


}
