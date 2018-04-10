using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class callPatient : MonoBehaviour
{


    private string antwort;
    private bool patientDa = false;

    private PatientenVariablen patient = new PatientenVariablen();

    private TestClass deleteMeObjekt = new TestClass();



    /**
     * Einfacher als gedacht
     */
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

    private void rufePatient()
    {
        Debug.Log("Neuer Patient wird gerufen");

        StartCoroutine(getPatientendaten());


        patientDa = true;
    }

    private void setzeDialog()
    {

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!patientDa)
            {
                rufePatient();
            }
            else
            {
                Debug.Log("Patient bereits vorhanden");
                Debug.Log("Patientendaten: " + JsonUtility.ToJson(patient));
            }
        }

    }



}
