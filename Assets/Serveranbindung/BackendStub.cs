using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class BackendStub : IBackend{
    public BackendStub(){
    }

    public string neuesSpiel(){
        return getXmlvonRest("admin/neuesSpiel");
    }

    public string getAktuelleBelegung(){
        return getXmlvonRest("getAktuelleBelegung");
    }

    public string getBelegung(int nummer){
        return getXmlvonRest("getBelegung/" + nummer);
    }

    public string getSpielDaten(){
        return getXmlvonRest("getSpielDaten");
    }

    public string getAlleErlaubtenZuege(){
        return getXmlvonRest("getAlleErlaubtenZuege");
    }

    public string getErlaubteZuege(string feld){
        return getXmlvonRest("getErlaubteZuege/" + feld);
    }

    public string getFigur(string feld){
        return getXmlvonRest("getFigur/" + feld);
    }

    public string ziehe(string von, string nach){
        return getXmlvonRest("ziehe/" + von + "/" + nach);
    }

    public string getZugHistorie(){
        return getXmlvonRest("getZugHistorie");
    }

    private string getXmlvonRest(string s)
    {
        string anfrage = Konstante.URL + s;
        string antwort = "";
        ServicePointManager.ServerCertificateValidationCallback = CallbackSSL;
        HttpWebRequest requestHTTP = (HttpWebRequest)WebRequest.Create(anfrage);
        HttpWebResponse responseHTTP = (HttpWebResponse)requestHTTP.GetResponse();
        StreamReader reader = new StreamReader(responseHTTP.GetResponseStream());
        try{
            if (requestHTTP.HaveResponse == false){
                throw new Exception("Keine Antwort vorhanden!");
            }
            antwort = reader.ReadToEnd();
        }
        catch (Exception e){
            throw e;
        }
        finally{
            if (responseHTTP != null)
                responseHTTP.Close();
            requestHTTP = null;
            responseHTTP = null;
        }
        return antwort;
    }

    // für SSL
    public bool CallbackSSL(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors){
        bool OK = true;
        if (sslPolicyErrors != SslPolicyErrors.None){
            for (int i = 0; i < chain.ChainStatus.Length; i++){
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown){
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
}