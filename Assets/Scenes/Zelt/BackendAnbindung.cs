using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System;
// using System.Web.Helpers;  googlen wie es geht da es damit einfacher geht



//Komplettes JSON "Objekt" erstellt (Aufbau sollte bekannt sein)
[Serializable]
public class TestClass
{
    public List<Person> results;    //Personendaten in einer Liste || in JSON durch "foo":[{...}] erkennbar
    public InfList info;            //Info zu Auruf als eigenes Objekt || in JSOn durch "bar":{...} erkennbar
}

//Personendatenliste mit eigenen Variablen (Bsp.: gender) und wieder Objekten (Bsp.: name)
[Serializable]
public class Person
{
    public string gender;
    public name name;
    public location location;
    public string email;
    public login login;
    public string phone;
    public string dob;
}

//Die restlichen Listen, s.o.
[Serializable]
public class InfList
{
    public string seed;
    public int results;
    public int page;
    public string version;
}
[Serializable]
public class name
{
    public string title;
    public string first;
    public string last;
}
[Serializable]
public class location
{
    public string street;
    public string city;
    public string state;
    public int postcode;
}
[Serializable]
public class login
{
    public string username;
    public string password;
}



//Benötigt für Tests und einfaches Beispiele unten
[Serializable]
public class MyClass
{
    public int level;
    public float timeElapsed;
    public string playerName;
}

// HIER ZWISCHEN SICHERER CODE
// vvvvvvvvvvvvvvvvvvvvvvvvvvv
public class BackendAnbindung : MonoBehaviour {

    private string antwort = "";

    private TestClass testObjekt = new TestClass();

	public BackendAnbindung()
    {
    }

    public void randomUser()
    {
        StartCoroutine(getRandomUser());

    }

    /**
     * Einfacher als gedacht
     */
    IEnumerator getRandomUser()
    {
        UnityWebRequest aufruf = new UnityWebRequest("http://randomuser.me/api");  //Quasi ein GET
        aufruf.downloadHandler = new DownloadHandlerBuffer();  //Downloadhandler liest Antwort von GET
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        } else
        {
            antwort = aufruf.downloadHandler.text;
            Debug.Log(antwort);     //Test um die Antwort zu sehen

            testObjekt = JsonUtility.FromJson<TestClass>(antwort);  //Erklärung siehe Beispiel unten

            Debug.Log(JsonUtility.ToJson(testObjekt));  //Test ob es auch wirklich übernommen wurde
// ^^^^^^^^^^^^^^^^^^^^^^^^^^^
// HIER ZWISCHEN SICHERER CODE


        // Hier wird anhand eines einfachen Beispiels gezeit wie JsonUtility funktioniert
        /*
        //Erstellen eines neuen Objektes aus MyClass und setzen der Werte
        MyClass myObject = new MyClass();
        myObject.level = 1;
        myObject.timeElapsed = 4.5f;
        myObject.playerName = "Mister Magic Weasel";

        //Das Objekt wird zu einem JSON-String umgewandelt und ausgegeben
        Debug.Log("_____OBJECTS TEST_____");
        string json = JsonUtility.ToJson(myObject);
        Debug.Log(json);
            
        //Ein zweites Objekt wird erstellt und zu einem String umgewandelt und ausgegeben, es ist "leer"
        MyClass myObject2 = new MyClass();
        string json2 = JsonUtility.ToJson(myObject2);
        Debug.Log(json2);

        //JSON-String wird in 2. Objekt geparsed, in einen zweiten JSON-String gewandelt welcher dann ausgegeben wird. Ergebnis json = json2
        myObject2 = JsonUtility.FromJson<MyClass>(json);
        Debug.Log(myObject);
        json2 = JsonUtility.ToJson(myObject2);
        Debug.Log(json2); //This works fine
        */

        }
    }






    /**
     * Unser kläglicher versuch Schach auf unser Projekt zu beziehen 
     */
/*
    private string getRandomUser()
    {

        string url = "https://randomuser.me/";
        string antwort = "";

        Debug.Log("Bin in RandomUser1");
        HttpWebRequest requestHTTP = (HttpWebRequest)WebRequest.Create(url);
        Debug.Log("Bin in RandomUser2");
        HttpWebResponse responseHTTP = (HttpWebResponse)requestHTTP.GetResponse(); //Hier wird der Fehler geworfen
        Debug.Log("Bin in RandomUser3");
        StreamReader reader = new StreamReader(responseHTTP.GetResponseStream());


        try
        {
            if (requestHTTP.HaveResponse == false)
            {
                throw new System.Exception("Keine Antowrt");
            }
            Debug.Log("This" + responseHTTP);
     //       antwort = reader.ReadToEnd();
        } catch (Exception e)
        {
            throw e;
        } finally
        {
            if (responseHTTP != null)
            {
                responseHTTP.Close();
            }
            responseHTTP = null;
            requestHTTP = null;
        } 

        return antwort;
    }

    public bool CallbackSSL(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool OK = true;
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid) OK = false;
                }
            }
        }
        return OK;
    }

    */

}
