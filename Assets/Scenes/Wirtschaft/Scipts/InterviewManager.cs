using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class InterviewManager : MonoBehaviour
{

    public Canvas can;
    public GameObject panelPartner;
    public Image list;
    public GameObject panelPC;
    public Canvas blendekomm;
    public Canvas blendegeht;

    public Transform spawnpoint;
    public GameObject[] interviewpartner;


    public Text debug;

    private string antwort;
    private int counter = 0;
    private bool wartend = false;
    private GameObject clone = null;

    public void starteDialog()
    {
        int ID = -1;

        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        foreach (IPartner partner in Variablen.interviewPartner.interviewPartner)
        {
            if (clickedButton.GetComponentInChildren<Text>().text == partner.name)
            {
                Variablen.momentanerInterviewpartnerID = partner.id;
                break;
            }
        }



        StartCoroutine(getDialog("interview/" + Variablen.momentanerInterviewpartnerID));

        blende();

        //diagnose.gameObject.SetActive(false); //alles was aus gehn muss (Berichte etc) hier

        Variablen.dialogOffen = true;
    }

    public void fuehreInterviewFort()
    {
        StartCoroutine(getDialog("interview/" + Variablen.momentanerInterviewpartnerID + "/" + Variablen.clickedButton));
    }

    IEnumerator getDialog(string schnittstelle)
    {

        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        debug.text += "unter yield \n";

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            debug.text += "error\n";

        }
        else
        {
            antwort = aufruf.downloadHandler.text;


            Variablen.interview = JsonUtility.FromJson<Interview>(antwort);
            if (Variablen.interview.frage != "ENDE")
            {
                can.GetComponentInChildren<Text>().text = Variablen.interview.frage;
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
            }
            else
            {
                can.GetComponentInChildren<Text>().text = "Danke, dass Sie sich die Zeit genommen haben";
                StartCoroutine("beendeInterview");
                blende();
            }



            counter = 0;
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
            debug.text += Variablen.interviewPartner.interviewPartner[0].name;

            foreach (Button but in list.GetComponentsInChildren<Button>(true))
            {
                debug.text += Variablen.interviewPartner.interviewPartner.Count;

                if (counter < Variablen.interviewPartner.interviewPartner.Count)
                {
                    foreach (Text txt in but.GetComponentsInChildren<Text>())
                    {
                        if (txt.name == "Name")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].name;
                            debug.text += Variablen.interviewPartner.interviewPartner[counter].name;

                        }
                        if (txt.name == "AnsehenWert")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].maxAnsehen + "";
                            debug.text += Variablen.interviewPartner.interviewPartner[counter].maxAnsehen + "";

                        }
                        if (txt.name == "SchwierigkeitWert")
                        {
                            txt.text = Variablen.interviewPartner.interviewPartner[counter].schwierigkeit + "";
                            debug.text += Variablen.interviewPartner.interviewPartner[counter].schwierigkeit + "";

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

            panelPartner.SetActive(true);
        }
    }


    public void schliesseInterviewpartner()
    {
        panelPartner.SetActive(false);
    }

    public void schliesseDialog()
    {
        can.gameObject.SetActive(false);
    }

    public void blende()
    {

        Image img;
        Text txt;

        if (Variablen.partnerAnwesend)
        {
            img = blendegeht.GetComponentInChildren<Image>();
            txt = blendegeht.GetComponentInChildren<Text>();
            blendegeht.gameObject.SetActive(true);
            StartCoroutine("beendeInterviewBlende");
            Variablen.partnerAnwesend = false;
        }
        else
        {
            img = blendekomm.GetComponentInChildren<Image>();
            txt = blendekomm.GetComponentInChildren<Text>();
            blendekomm.gameObject.SetActive(true);
            StartCoroutine("startInterviewBlende");
            Variablen.partnerAnwesend = true;
        }

    }

    IEnumerator beendeInterview()
    {
        //hier irgendwie aufstehen lassen
        yield return new WaitForSeconds(3);
        blende();
    }

    IEnumerator startInterviewBlende()
    {
        yield return new WaitForSeconds(1);
        panelPC.SetActive(false);
        can.gameObject.SetActive(true);
        clone = Instantiate(interviewpartner[Random.Range(0,interviewpartner.Length-1)], spawnpoint.position, spawnpoint.rotation) as GameObject;
        yield return new WaitForSeconds(2);
        blendekomm.gameObject.SetActive(false);
    }
    IEnumerator beendeInterviewBlende()
    {
        yield return new WaitForSeconds(1);
        panelPC.SetActive(true);
        panelPartner.SetActive(false);
        can.gameObject.SetActive(false);
        Destroy(clone);
        yield return new WaitForSeconds(2);
        blendegeht.gameObject.SetActive(false);
    }

}
