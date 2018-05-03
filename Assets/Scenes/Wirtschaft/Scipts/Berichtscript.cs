using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Berichtscript : MonoBehaviour
{

    public GameObject arztbericht;
    public GameObject budgetbericht;

    private string antwort;

    public void zeigeArztbericht()
    {
        StartCoroutine(getArztbericht());
    }

    IEnumerator getArztbericht()
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "getArztbreicht");
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.arztbericht = JsonUtility.FromJson<Arztbericht>(antwort);
            arztbericht.SetActive(true);

            GameObject.Find("AusgabenArztWert").GetComponent<Text>().text = Variablen.arztbericht.ausgaben;
            GameObject.Find("RufzuwachsArztWert").GetComponent<Text>().text = Variablen.arztbericht.rufzuwachs;
            GameObject.Find("RuverlustArztWert").GetComponent<Text>().text = Variablen.arztbericht.rufverlust;
            GameObject.Find("RufbilanzArztWert").GetComponent<Text>().text = Variablen.arztbericht.rufbilanz + "";
            GameObject.Find("GesamtausgabenArztWert").GetComponent<Text>().text = Variablen.arztbericht.gesamtausgaben + "";
            GameObject.Find("ErfolgreichePatientenWert").GetComponent<Text>().text = Variablen.arztbericht.erfolgreichePatienten + "";
            GameObject.Find("nichtErfolgreichePatientenWert").GetComponent<Text>().text = Variablen.arztbericht.nichtErfolgreichePatienten + "";

        }
    }

    public void zeigeBudgetbericht()
    {
        StartCoroutine(getBudgetbericht());
    }

    IEnumerator getBudgetbericht()
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "getBudgetbreicht");
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Variablen.budgetbericht = JsonUtility.FromJson<Budgetbericht>(antwort);
            budgetbericht.SetActive(true);

            GameObject.Find("EinnahmenBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.einnahmen;
            GameObject.Find("AusgabenBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.ausgaben;
            GameObject.Find("RufzuwachsBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.rufzuwachs;
            GameObject.Find("RuverlustBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.rufverlust;
            GameObject.Find("RufbilanzBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.rufbilanz + "";
            GameObject.Find("GesamtausgabenBudgetWert").GetComponent<Text>().text = Variablen.budgetbericht.gesamtausgaben + "";


        }
    }

    public void schliesseArztbericht()
    {
        arztbericht.SetActive(false);
    }

    public void schliesseBudgetbericht()
    {
        budgetbericht.SetActive(false);
    }
}
