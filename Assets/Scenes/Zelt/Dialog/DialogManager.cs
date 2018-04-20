using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DialogManager : MonoBehaviour
{

    public Canvas can;
    public Canvas blutbild;

    private string antwort;
    private int counter = 0;
    private bool wartend = false;

    public void starteDialog()
    {
        Debug.Log(Variablen.momentanerPatient.ID);
        StartCoroutine(getDialog("getAnamnese/" + Variablen.momentanerPatient.ID));
    }

    public void fuehreDialogFort()
    {
        StartCoroutine(getDialog("getAnamnese/" + Variablen.momentanerPatient.ID + "/" + Variablen.clickedButton));
    }

    public void blutbildErstellen()
    {
        StartCoroutine(getBlutbild("getBlutbild/" + Variablen.momentanerPatient.ID));
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
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Variablen.kameraFest = true;
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
        if (Input.GetKeyDown(KeyCode.F) && Variablen.patientVorhanden && Variablen.patientInReichweite)
        {
            starteDialog();
            can.gameObject.SetActive(true);

            Variablen.dialogOffen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Variablen.kameraFest = false;
        }

        if (Input.GetKeyDown(KeyCode.B) && Variablen.anamnese.option.Equals("blutbild"))
        {
            blutbildErstellen();
        }
    }
}
