using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DialogManager : MonoBehaviour
{

    public Canvas can;
    public Canvas blutbild;
    public Canvas ultraschall;
    public Canvas roentgenbild;
    public Canvas diagnose;

    private string antwort;
    private int counter = 0;
    private bool wartend = false;

    public void starteDialog()
    {
        if (Variablen.patientInZelt)
        {
            Debug.Log(Variablen.momentanerPatient.ID);
            StartCoroutine(getDialog("getAnamnese/" + Variablen.momentanerPatient.ID));

            diagnose.gameObject.SetActive(false);
            can.gameObject.SetActive(true);

            Variablen.dialogOffen = true;
        }
    }

    public void fuehreDialogFort()
    {
        StartCoroutine(getDialog("getAnamnese/" + Variablen.momentanerPatient.ID + "/" + Variablen.clickedButton));
    }

    public void blutbildErstellen()
    {
        StartCoroutine(getBlutbild("getBlutbild/" + Variablen.momentanerPatient.ID));
    }

    public void roentgenbildErstellen()
    {
        StartCoroutine(getRoentgenbild("getRoentgen/" + Variablen.momentanerPatient.ID));
    }

    public void ultraschallErstellen()
    {
        StartCoroutine(getUltraschall("getUltraschall/" + Variablen.momentanerPatient.ID));
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
            Variablen.anamnese = JsonUtility.FromJson<Dialog>(test);
            can.GetComponentInChildren<Text>().text = Variablen.anamnese.antwort;
            foreach (Button but in can.GetComponentsInChildren<Button>(true))
            {
                if (counter < Variablen.anamnese.fragen.Count)
                {
                    but.GetComponentInChildren<Text>().text = Variablen.anamnese.fragen[counter];
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

            Debug.Log(Variablen.anamnese.option);

            if (Variablen.anamnese.option.Equals("ende"))
            {
                wartend = true;
                Variablen.dialogOffen = false;
                can.gameObject.SetActive(false);

                Variablen.dialogOffen = false;
            }
            if (Variablen.anamnese.option.Equals("blutbild"))
            {
                GameObject.Find("ZeigeBlutbild").GetComponent<Button>().interactable = true;
            }
            if (Variablen.anamnese.option.Equals("ultraschall"))
            {
                GameObject.Find("ZeigeUltraschall").GetComponent<Button>().interactable = true;
            }
            counter = 0;
        }
    }

    IEnumerator getBlutbild(string schnittstelle)
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

            Variablen.blutbild = JsonUtility.FromJson<Blutbild>(antwort);

            Debug.Log(JsonUtility.ToJson(Variablen.blutbild));

            blutbild.gameObject.SetActive(true);
            GameObject.Find("LeukozytenWert").GetComponent<Text>().text = Variablen.blutbild.Leukozyten + "/µl";
            GameObject.Find("ErythrozytenWert").GetComponent<Text>().text = Variablen.blutbild.Erythrozyten + "/µl";
            GameObject.Find("ThrombozytenWert").GetComponent<Text>().text = Variablen.blutbild.Thrombozyten + "/µl";
            GameObject.Find("HaemoglobinkonzentrationWert").GetComponent<Text>().text = Variablen.blutbild.Haemoglobinkonzentration + "g/dl";
            GameObject.Find("HaematokritWert").GetComponent<Text>().text = Variablen.blutbild.Haematokrit + "%";
            GameObject.Find("MCHWert").GetComponent<Text>().text = Variablen.blutbild.MCH + "pg";
            GameObject.Find("MCHCWert").GetComponent<Text>().text = Variablen.blutbild.MCHC + "g/dl";
            GameObject.Find("MCVWert").GetComponent<Text>().text = Variablen.blutbild.MCV + "fl";
        }
    }

    IEnumerator getRoentgenbild(string schnittstelle)
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + schnittstelle);
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
            roentgenbild.gameObject.SetActive(false);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;

            Variablen.roentgenbild = JsonUtility.FromJson<Roentgenbild>(antwort);


            roentgenbild.gameObject.SetActive(true);
            roentgenbild.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/Zelt/roentgen" + Variablen.roentgenbild.roentgenID);

        }
    }

    IEnumerator getUltraschall(string schnittstelle)
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

            Variablen.ultraschall = JsonUtility.FromJson<Ultraschall>(antwort);

            ultraschall.gameObject.SetActive(true);
            ultraschall.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/Zelt/ultraschall" + Variablen.ultraschall.ultraschallID);

        }
    }


    public void setB1()
    {
        Variablen.clickedButton = "0";
        fuehreDialogFort();
    }
    public void setB2()
    {
        Variablen.clickedButton = "1";
        fuehreDialogFort();
    }
    public void setB3()
    {
        Variablen.clickedButton = "2";
        fuehreDialogFort();
    }
    public void setB4()
    {
        Variablen.clickedButton = "3";
        fuehreDialogFort();
    }
    public void setB5()
    {
        Variablen.clickedButton = "4";
        fuehreDialogFort();
    }
    public void setB6()
    {
        Variablen.clickedButton = "5";
        fuehreDialogFort();
    }
    public void exitBlutbild()
    {
        blutbild.gameObject.SetActive(false);
    }


    public GameObject a;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Variablen.patientInZelt)
        {
            starteDialog();
        }

    }
}
