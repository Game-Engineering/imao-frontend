using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class SponsorenWindow : MonoBehaviour
{

    public GameObject sponsorPanel;
    public Text debug;
    public Image aktuell, moeglich;

    private string antwort;
    private int counter = 0;

    public void zeigeSponsorenWindow()
    {
        zeigeAktuelleSponsoren();
        zeigeMoeglicheSponsoren();
        sponsorPanel.SetActive(true);
    }
    public void schliesseSponsorenWindow()
    {
        sponsorPanel.SetActive(false);
    }

    public void zeigeAktuelleSponsoren()
    {
        if (Variablen.sponsorenNeuLaden)
            StartCoroutine(getAktuelleSponsoren("getAktuelleSponsoren"));
    }

    IEnumerator getAktuelleSponsoren(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();

        Debug.Log("TEST A");

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            DebugConsole.Log("Aktuell: " + antwort);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Debug.Log("Aktuell: " + antwort);
            DebugConsole.Log("Aktuell: " + antwort);

            Variablen.sponsorenlisteAktuell = JsonUtility.FromJson<aktuelleSponsorenliste>(antwort);

            debug.text = aktuell.GetComponentsInChildren<Button>(true).Length + "";

            foreach (Button but in aktuell.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.sponsorenlisteAktuell.aktuelleSponsoren.Count)
                {
                    foreach (Text txt in but.GetComponentsInChildren<Text>())
                    {
                        if (txt.name == "SponsorName")
                        {
                            txt.text = Variablen.sponsorenlisteAktuell.aktuelleSponsoren[counter].sponsorName;
                        }
                        if (txt.name == "SponsorBetrag")
                        {
                            txt.text = Variablen.sponsorenlisteAktuell.aktuelleSponsoren[counter].monatlicherBetrag + "";
                        }
                        if (txt.name == "SponsorZeit")
                        {
                            txt.text = Variablen.sponsorenlisteAktuell.aktuelleSponsoren[counter].zeitraum + "";
                        }
                        if (txt.name == "SponsorMalus")
                        {
                            txt.text = Variablen.sponsorenlisteAktuell.aktuelleSponsoren[counter].absprungansehen + "";
                        }
                    }
                    but.gameObject.SetActive(true);
                    but.interactable = false;
                    counter++;
                }
                else
                {
                    but.GetComponentInChildren<Text>().text = "";
                    but.interactable = false;
                    but.gameObject.SetActive(false);
                }
            }
            counter = 0;
        }
    }

    public void zeigeMoeglicheSponsoren()
    {
        if (Variablen.sponsorenNeuLaden)
            StartCoroutine(getMoeglicheSponsoren("getMoeglicheSponsoren"));
    }

    IEnumerator getMoeglicheSponsoren(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
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

            Debug.Log("Aktuell: " + antwort);
            DebugConsole.Log("Aktuell: " + antwort);

            Variablen.sponsorenlisteMoeglich = JsonUtility.FromJson<moeglicheSponsorenliste>(antwort);


            foreach (Button but in moeglich.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren.Count)
                {
                    foreach (Text txt in but.GetComponentsInChildren<Text>())
                    {
                        if (txt.name == "SponsorName")
                        {
                            txt.text = Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren[counter].sponsorName;
                        }
                        if (txt.name == "SponsorBetrag")
                        {
                            txt.text = Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren[counter].monatlicherBetrag + "€";
                        }
                        if (txt.name == "SponsorZeit")
                        {
                            txt.text = Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren[counter].zeitraum + " Monate";
                        }
                        if (txt.name == "SponsorMalus")
                        {
                            txt.text = Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren[counter].absprungansehen + "";
                        }
                    }
                    but.gameObject.SetActive(true);
                    if (Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren[counter].angeworben)
                    {
                        but.interactable = false;
                    }
                    else
                    {
                        but.interactable = true;
                    }
                    counter++;
                }
                else
                {
                    but.GetComponentInChildren<Text>().text = "";
                    but.gameObject.SetActive(false);
                }
            }
            counter = 0;
        }
    }

    public void werbeSponsor()
    {
        StartCoroutine(werbeSponsorAn("/werbeSponsorAn/"));
    }

    IEnumerator werbeSponsorAn(string schnittstelle)
    {
        int ID = -1;
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        clickedButton.GetComponent<Button>().interactable = false;

        foreach (Sponsor spon in Variablen.sponsorenlisteMoeglich.verfuegbareSponsoren)
        {
            if (clickedButton.GetComponentInChildren<Text>().text == spon.sponsorName)
            {
                ID = spon.ID;
                Variablen.sponsorenNeuLaden = false;
                Variablen.geworbeneSponsoren.Add(spon.sponsorName);
                break;
            }
        }


        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle + ID);
        aufruf.downloadHandler = new DownloadHandlerBuffer();

        yield return aufruf.SendWebRequest();


        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;


            zeigeSponsorenWindow();
        }
    }

}
