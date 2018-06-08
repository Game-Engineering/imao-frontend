using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StelleDiagnose : MonoBehaviour
{

    public Canvas can;
    public Canvas dialog;

    private string antwort;
    private GameObject auswahl;
    private GameObject ergebnis;
    private bool ersteDiagnose = true;

    public void oeffneDiagnose()
    {
        dialog.gameObject.SetActive(false);
        can.gameObject.SetActive(true);
        if (ersteDiagnose)
        {
            auswahl = GameObject.Find("BackgroundDiagnose");
            ergebnis = GameObject.Find("DiagnoseErgebnis");
            Debug.Log(auswahl);
            Debug.Log(ergebnis);
            ersteDiagnose = false;
        }

        auswahl.SetActive(true);
        ergebnis.SetActive(false);
    }

    public void stelleDiagnose()
    {
        StartCoroutine(getDiagnose("diagnose/" + Variablen.momentanerPatient.ID + "/" + Variablen.krankheitDiagnose));
    }

    IEnumerator getDiagnose(string schnittstelle)
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
            Variablen.diagnoseErgebnis = JsonUtility.FromJson<DiagnoseErgebnis>(antwort);
            Variablen.patientGeht = true;
            zeigeErgebnis();
            yield return new WaitForSeconds(3);
            Variablen.patientGeht = true;
            Variablen.patientInZelt = false;
            Variablen.patientVorhanden = false;
            can.gameObject.SetActive(false);
        }
    }

    public void zeigeErgebnis()
    {
        ergebnis.GetComponentInChildren<Text>().text = Variablen.diagnoseErgebnis.nachricht;
        auswahl.SetActive(false);
        ergebnis.SetActive(true);

        GameObject.Find("StelleDiagnose").GetComponent<Button>().interactable = false;
        GameObject.Find("ZeigeBlutbild").GetComponent<Button>().interactable = false;
        GameObject.Find("StarteDialog").GetComponent<Button>().interactable = false;
        Variablen.patientInZelt = false;

        GameObject.Find("NeueRunde").GetComponent<Button>().interactable = true;

        if (Variablen.rundeArzt.wartendePatienten > 0)
        {
            GameObject.Find("RufePatienten").GetComponent<Button>().interactable = true;
        }
    }

    public void getButtonGesund()
    {
        Variablen.krankheitDiagnose = "0";
        stelleDiagnose();
    }
    public void getButtonMasern()
    {
        Variablen.krankheitDiagnose = "1";
        stelleDiagnose();
    }
    public void getButtonCholera()
    {
        Variablen.krankheitDiagnose = "2";
        stelleDiagnose();
    }
    public void getButtonBilharziose()
    {
        Variablen.krankheitDiagnose = "3";
        stelleDiagnose();
    }
    public void getButtonHIV()
    {
        Variablen.krankheitDiagnose = "4";
        stelleDiagnose();
    }
    public void getButtonHepA()
    {
        Variablen.krankheitDiagnose = "5";
        stelleDiagnose();
    }
    public void getButtonHepB()
    {
        Variablen.krankheitDiagnose = "6";
        stelleDiagnose();
    }
    public void getButtonTetanus()
    {
        Variablen.krankheitDiagnose = "7";
        stelleDiagnose();
    }
    public void getButtonGelbfieber()
    {
        Variablen.krankheitDiagnose = "8";
        stelleDiagnose();

    }
    public void getButtonDengueFieber()
    {
        Variablen.krankheitDiagnose = "9";
        stelleDiagnose();
    }
    public void getButtonHautleishmaniasis()
    {
        Variablen.krankheitDiagnose = "10";
        stelleDiagnose();
    }



}
