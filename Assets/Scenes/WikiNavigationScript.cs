using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WikiNavigationScript : MonoBehaviour
{

    public Canvas can;
    public Image scrollerKat;
    public Image scrollerUKat;

    private string antwort;
    private string kategorien = Konstanten.URLfest + "getWikiKategorien/";
    private string elemente = Konstanten.URLfest + "getWikiElement/";

    private int selectedKat = -1;
    private int counter = 0;

    public void zeigeWikiWindow()
    {
        StartCoroutine(getWikiKategorien());
        // can.gameObject.SetActive(true);
        Variablen.bildschirmactive = true;
    }

    IEnumerator getWikiKategorien()
    {
        Debug.Log(elemente);
        Debug.Log(kategorien);
        UnityWebRequest aufruf = new UnityWebRequest(kategorien);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();


        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            DebugConsole.Log("Aktuell: " + antwort);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.wikiKategorien = JsonUtility.FromJson<WikiKategorien>(antwort);

            foreach (Button but in scrollerKat.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.wikiKategorien.Kategorien.Count)
                {
                    but.GetComponentInChildren<Text>(true).text = Variablen.wikiKategorien.Kategorien[counter];
                    but.gameObject.SetActive(true);
                    counter++;
                }
                else
                {
                    but.GetComponentInChildren<Text>().text = "";
                    but.gameObject.SetActive(false);
                }
            }

            scrollerKat.gameObject.SetActive(true);

            counter = 0;
            can.gameObject.SetActive(true);
        }
    }


    public void getElemente(int ID)
    {
        StartCoroutine(getWikiElement(ID));
        selectedKat = ID;
    }

    IEnumerator getWikiElement(int ID)
    {
        Debug.Log(elemente);
        Debug.Log(kategorien);
        UnityWebRequest aufruf = new UnityWebRequest(elemente + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();


        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            DebugConsole.Log("Aktuell: " + antwort);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.unterkategorien = JsonUtility.FromJson<Unterkategorien>(antwort);

            Debug.Log(antwort);

            foreach (Button but in scrollerUKat.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.unterkategorien.response.Count)
                {
                    but.GetComponentInChildren<Text>(true).text = Variablen.unterkategorien.response[counter].question;
                    but.gameObject.SetActive(true);
                    counter++;
                }
                else
                {
                    but.GetComponentInChildren<Text>().text = "";
                    but.gameObject.SetActive(false);
                }
            }

            scrollerUKat.gameObject.SetActive(true);

            counter = 0;
        }
    }


    public void getAntwort(int ID)
    {
        StartCoroutine(getWikiAntwort(ID));
    }

    IEnumerator getWikiAntwort(int ID)
    {
        UnityWebRequest aufruf = new UnityWebRequest(elemente + "/" + selectedKat + "/" + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();


        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            DebugConsole.Log("Aktuell: " + antwort);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.wikiTexte = JsonUtility.FromJson<WikiTexte>(antwort);

            GameObject.Find("AntwortFrage").GetComponent<Text>().text = Variablen.wikiTexte.response.question;
            GameObject.Find("AntwortAntwort").GetComponent<Text>().text = Variablen.wikiTexte.response.content;
        }
    }


    public void schliesseWiki()
    {
        can.gameObject.SetActive(false);
        Variablen.bildschirmactive = false;
    }
}



// Beispielaufruf: 
// localhost:8080/imao/api/spiel/getWikiKategorien

