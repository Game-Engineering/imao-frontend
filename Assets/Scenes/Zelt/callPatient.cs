using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class callPatient : MonoBehaviour
{

    public GameObject[] patienten;
    public Transform spawnpoint;

    private GameObject clone = null;
    Material m1_Material, m2_Material, m3_Material;
    private string antwort;




    /**
     * Einfacher als gedacht
     * LEGACY CODE als Aufhänger
     */
    /*
   IEnumerator getPatientendaten()
   {
       UnityWebRequest aufruf = new UnityWebRequest("http://randomuser.me/api");  //Quasi ein GET
       //UnityWebRequest aufruf = new UnityWebRequest("http://games.informatik.hs-mannheim.de:8080/rest/schach/spiel/getBelegung/0");  //Quasi ein GET

       aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
       yield return aufruf.SendWebRequest();

       if (aufruf.isNetworkError || aufruf.isHttpError)
       {
           Debug.Log(aufruf.error);
       }
       else
       {
           antwort = aufruf.downloadHandler.text;

           deleteMeObjekt = JsonUtility.FromJson<TestClass>(antwort);

           System.Random rng = new System.Random();

           patient.alter = rng.Next(4, 75);
           patient.id = rng.Next(1, 25);

           foreach (Person person in deleteMeObjekt.results)
           {

               Name name = new Name();
               Symptome symptome = new Symptome();

               name.vorname = person.name.first;
               name.nachname = person.name.last;

               symptome.symptom1 = person.location.state;
               symptome.symptom2 = person.location.postcode + "";
               symptome.symptom3 = person.location.city;
               symptome.symptom4 = person.location.street;
               patient.symptome = symptome;
               patient.name = name;
           }

           Debug.Log("Patientendaten: " + JsonUtility.ToJson(patient));


       }
   }
   */

    IEnumerator getPatient()
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "getPatient");
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            Debug.Log(antwort);
            Variablen.momentanerPatient = JsonUtility.FromJson<Patient>(antwort);

            Debug.Log(JsonUtility.ToJson(Variablen.momentanerPatient));

            erstellePatient();
            Debug.Log("Patient wird erstellt ...");
            GameObject.Find("StarteDialog").GetComponent<UnityEngine.UI.Button>().interactable = true;
            GameObject.Find("StelleDiagnose").GetComponent<UnityEngine.UI.Button>().interactable = true;
            GameObject.Find("RufePatient").GetComponent<UnityEngine.UI.Button>().interactable = false;
            GameObject.Find("NeueRunde").GetComponent<UnityEngine.UI.Button>().interactable = false;

        }
    }


    public void rufePatient()
    {
        if (!Variablen.patientVorhanden && !Variablen.patientInZelt && !Variablen.patientGeht && Variablen.runde.wartendePatienten > 0)
        {
            Debug.Log("Neuer Patient wird gerufen");

            StartCoroutine(getPatient());
            Variablen.runde.wartendePatienten = (Variablen.runde.wartendePatienten - 1); // <-- C# hat manchmal Probleme mit ++/--
      }
        else
        {
            Debug.Log("Patient bereits vorhanden");
        }

    }

    private void Update()
    {



        //farbwechsel testen
        if (Input.GetKeyDown(KeyCode.U))
        {
            erstellePatient();
            Variablen.patientVorhanden = true;
        }
        //wenn patient gerufen, dann bewegen

    }

    //Patient wird anhand der Patient.erscheinungID
    public void erstellePatient()
    {
        Debug.Log("Neuer Patient wird erstellt");

        int erscheinungID = Variablen.momentanerPatient.erscheinungID;

        Destroy(clone);
        //Debug.Log("Das vorherige Objekt wurde gelöscht.");
        clone = Instantiate(patienten[erscheinungID], spawnpoint.position, spawnpoint.rotation) as GameObject;
        //Debug.Log("Das neue Objekt wurde erstellt.");


        /**
        * Einfacher als gedacht
        * LEGACY CODE als Aufhänger
        */

        ////jedes mesh einzeln ansteuern weil es mehr als 1 gibt
        //m1_Material = clone.transform.Find("body").GetComponent<MeshRenderer>().material;
        //m2_Material = clone.transform.Find("Kopf").GetComponent<MeshRenderer>().material;
        //m3_Material = clone.transform.Find("Ohren").GetComponent<MeshRenderer>().material;

        ////Debug.Log("Erscheinungs ID: " + Variablen.momentanerPatient.erscheinungID);

        //String erscheinung = Variablen.momentanerPatient.erscheinungID + "";

        //if(erscheinung[2] == '1')
        //{
        //    m1_Material.color = new Color32(255, 224, 189, 255);
        //    m2_Material.color = new Color32(255, 224, 189, 255);
        //    m3_Material.color = new Color32(255, 224, 189, 255);

        //    Debug.Log("Farbe: NORMAL");
        //}else if (erscheinung[2] == '2')
        //{
        //    m1_Material.color = new Color32(255, 227, 159, 255);
        //    m2_Material.color = new Color32(255, 227, 159, 255);
        //    m3_Material.color = new Color32(255, 227, 159, 255);

        //    Debug.Log("Farbe: GELB");
        //}
        //else if (erscheinung[2] == '3')
        //{
        //    m1_Material.color = new Color32(255, 102, 102, 255);
        //    m2_Material.color = new Color32(255, 102, 102, 255);
        //    m3_Material.color = new Color32(255, 102, 102, 255);

        //    Debug.Log("Farbe: ROT");
        //}
        //else
        //{
        //    m1_Material.color = new Color32(255, 248, 241, 255);
        //    m2_Material.color = new Color32(255, 248, 241, 255);
        //    m3_Material.color = new Color32(255, 248, 241, 255);

        //    Debug.Log("Farbe: BLASS");
        //}
    }



}
