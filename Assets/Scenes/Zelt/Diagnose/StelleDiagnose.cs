using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StelleDiagnose : MonoBehaviour
{

    public Canvas can;

    private string antwort;


    public void oeffneDiagnose()
    {
        GameObject.Find("BackgroundDiagnose").SetActive(true);
        GameObject.Find("DiagnoseErgebnis").SetActive(false);
        can.gameObject.SetActive(true);
    }

    public void stelleDiagnose()
    {
        StartCoroutine(getDiagnose("getDiagnose/" + Variablen.momentanerPatient.ID + "/" + Variablen.krankheitDiagnose));
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
            zeigeErgebnis();
            yield return new WaitForSeconds(5);
            Variablen.patientGeht = true;
            Variablen.patientInZelt = false;
        }
    }

    public void zeigeErgebnis()
    {
        can.GetComponentInChildren<Text>().text = Variablen.diagnoseErgebnis.nachricht;
        GameObject.Find("BackgroundDiagnose").SetActive(false);
        GameObject.Find("DiagnoseErgebnis").SetActive(true);

        GameObject.Find("StelleDiagnose").GetComponent<Button>().interactable = false;
        GameObject.Find("ZeigeBlutbild").GetComponent<Button>().interactable = false;
        GameObject.Find("StarteDialog").GetComponent<Button>().interactable = false;

        GameObject.Find("NeueRunde").GetComponent<Button>().interactable = true;

        if (Variablen.runde.wartendePatienten > 0)
        {
            GameObject.Find("RufePatienten").GetComponent<Button>().interactable = false;
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
