using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InterviewWindow : MonoBehaviour
{

    public Canvas can;
    public GameObject panelPartner;
    public Image list;

    private string antwort;
    private int counter = 0;
    private bool wartend = false;

    public void starteDialog()
    {
        if (Variablen.patientInZelt)
        {
            Debug.Log(Variablen.momentanerPatient.ID);
            StartCoroutine(getDialog("Interview" + Variablen.momentanerPatient.ID));

            //diagnose.gameObject.SetActive(false); //alles was aus gehn muss (Berichte etc) hier
            can.gameObject.SetActive(true);

            Variablen.dialogOffen = true;
        }
    }

    public void fuehreInterviewFort()
    {
        StartCoroutine(getDialog("Interview/" + Variablen.momentanerPatient.ID + "/" + Variablen.clickedButton));
    }

    IEnumerator getDialog(string schnittstelle)
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
            antwort = aufruf.downloadHandler.text;
            string test = aufruf.downloadHandler.text;
            Variablen.interview = JsonUtility.FromJson<Interview>(test);
            can.GetComponentInChildren<Text>().text = Variablen.interview.frage;
            if (Variablen.interview.frage != "ENDE")
            {
                foreach (Button but in can.GetComponentsInChildren<Button>(true))
                {
                    if (counter < Variablen.interview.antworten.Count)
                    {
                        but.GetComponentInChildren<Text>().text = Variablen.interview.antworten[counter];
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
            } else
            {
                yield return new WaitForSeconds(5);
                can.gameObject.SetActive(false);
            }
        }
    }

    public void setB1()
    {
        Variablen.clickedButton = "0";
        fuehreInterviewFort();
    }
    public void setB2()
    {
        Variablen.clickedButton = "1";
        fuehreInterviewFort();
    }
    public void setB3()
    {
        Variablen.clickedButton = "2";
        fuehreInterviewFort();
    }
    public void setB4()
    {
        Variablen.clickedButton = "3";
        fuehreInterviewFort();
    }
    public void setB5()
    {
        Variablen.clickedButton = "4";
        fuehreInterviewFort();
    }
    public void setB6()
    {
        Variablen.clickedButton = "5";
        fuehreInterviewFort();
    }


    public void zeigeDialogpartner()
    {
        StartCoroutine(getInterviewPartner("getInterviewPartner"));
    }

    IEnumerator getInterviewPartner(string schnittstelle)
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
            Variablen.interviewPartner = JsonUtility.FromJson<Interviewpartner>(antwort); //TODO HERE

            foreach (Button but in list.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.interviewPartner.interviewPartner.Count)
                {
                    foreach (Text txt in but.GetComponentsInChildren<Text>())
                    {
                        if (txt.name == "Name")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].name;
                        }
                        if (txt.name == "AnsehenWert")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].maxAnsehen + "";
                        }
                        if (txt.name == "SchwierigkeitWert")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].schwierigkeit + "";
                        }
                    }
                    //but.GetComponentInChildren<Text>().text = Variablen.postfach.mailliste[counter];
                    //but.gameObject.SetActive(true);
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

            panelPartner.SetActive(true);
        }
    }


    public void schliesseInterviewpartner()
    {
        panelPartner.SetActive(false);
    }

}
