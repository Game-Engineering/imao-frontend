using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InterviewManager : MonoBehaviour
{

    public Canvas can;

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
            Variablen.interview = JsonUtility.FromJson<Dialog>(test);
            can.GetComponentInChildren<Text>().text = Variablen.interview.antwort;
            foreach (Button but in can.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.interview.fragen.Count)
                {
                    but.GetComponentInChildren<Text>().text = Variablen.interview.fragen[counter];
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

            Debug.Log(Variablen.interview.option);

            if (Variablen.interview.option.Equals("ende"))
            {
                wartend = true;
                Variablen.dialogOffen = false;
                can.gameObject.SetActive(false);

                Variablen.dialogOffen = false;
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


    public GameObject a;


    
}
