using RESTClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstraktes Hilfsscript, das Zugriff auf Rest APIs bietet.
/// Erbende Klassen können die Methoden GET, POST und PUT verwenden, um mit Rest APIs zu kommunizieren.
/// </summary>
public abstract class RestServer : MonoBehaviour
{
    private static System.Object nullObjekt = null; //Wird für Anfragen genutzt, die keine Nutzdaten tragen
    public bool sicher = false;
    public string server;
    public int port;
    private RestClient client;

    private void Start()
    {
        client = new RestClient(string.Format("{0}:{1}", server, port), sicher);
    }

    /// <summary>
    /// Sendet eine Anfrage an den angegebenen Endpunkt, unter Verwendung des angegeben HTTP-Verbs.
    /// </summary>
    /// <typeparam name="T">Erwarteter Rückgabe-Typ der Rest API</typeparam>
    /// <typeparam name="U">Typ der zu schickenden Daten</typeparam>
    /// <param name="endpunkt">Der Endpunkt der Rest API an den die Anfrage geschickt wird</param>
    /// <param name="verb">Das zu verwendende HTTP-Verb</param>
    /// <param name="daten">Die zu schickenden Werte</param>
    /// <param name="callback">Callback-Funktion die nach Durchführung der Anfrage aufgerufen wird</param>
    /// <returns></returns>
    private IEnumerator SendeAnfrage<T, U>(string endpunkt, Method verb = Method.GET, U daten = null, Action<IRestResponse<T>> callback = null) where U : class
    {
        //Bereite vollständige URL vor, füge evtl fehldenen / zwischen Basis- und Endpunkt-URL ein
        var url = client.Url;
        url += (endpunkt.StartsWith("/") || client.Url.EndsWith("/")) ? endpunkt : "/" + endpunkt;
        RestRequest anfrage = new RestRequest(url, verb);

        //Bei POST- oder PUT-Anfragen werden eventuell Daten geschickt
        if (verb == Method.POST || verb == Method.PUT)
            anfrage.AddBody(daten);

        //Führe den Anfragen aus
        yield return anfrage.Request.SendWebRequest();

        //Rufe den Callback auf
        anfrage.GetText(callback);
    }

    /// <summary>
    /// Führt eine GET-Anfrage aus. (Lesen)
    /// (Einfacher Wrapper um SendeAnfrage.)
    /// </summary>
    /// <typeparam name="T">Erwarteter Rückgabe-Typ der Rest API</typeparam>
    /// <param name="endpunkt">Der Endpunkt der Rest API an den die Anfrage geschickt wird</param>
    /// <param name="callback">Callback-Funktion die nach Durchführung der Anfrage aufgerufen wird</param>
    /// <returns></returns>
    protected IEnumerator GET<T>(string endpunkt, Action<IRestResponse<T>> callback = null)
    {
        yield return SendeAnfrage(endpunkt, Method.GET, nullObjekt, callback);
    }

    /// <summary>
    /// Führt eine POST-Anfrage aus. (Schreiben; nicht idempotent)
    /// (Einfacher Wrapper um SendeAnfrage.)
    /// </summary>
    /// <typeparam name="T">Erwarteter Rückgabe-Typ der Rest API</typeparam>
    /// <typeparam name="U">Typ der zu schickenden Daten</typeparam>
    /// <param name="endpunkt">Der Endpunkt der Rest API an den die Anfrage geschickt wird</param>
    /// <param name="daten">Die zu schickenden Werte</param>
    /// <param name="callback">Callback-Funktion die nach Durchführung der Anfrage aufgerufen wird</param>
    /// <returns></returns>
    protected IEnumerator POST<T, U>(string endpunkt, U daten, Action<IRestResponse<T>> callback = null) where U : class
    {
        yield return SendeAnfrage(endpunkt, Method.POST, daten, callback);
    }

    /// <summary>
    /// Führt eine POST-Anfrage aus. (Schreiben; nicht idempotent)
    /// (Einfacher Wrapper um SendeAnfrage.)
    /// </summary>
    /// <typeparam name="T">Erwarteter Rückgabe-Typ der Rest API</typeparam>
    /// <typeparam name="U">Typ der zu schickenden Daten</typeparam>
    /// <param name="endpunkt">Der Endpunkt der Rest API an den die Anfrage geschickt wird</param>
    /// <param name="daten">Die zu schickenden Werte</param>
    /// <param name="callback">Callback-Funktion die nach Durchführung der Anfrage aufgerufen wird</param>
    /// <returns></returns>
    protected IEnumerator PUT<T, U>(string endpunkt, U daten, Action<IRestResponse<T>> callback = null) where U : class
    {
        yield return SendeAnfrage(endpunkt, Method.PUT, daten, callback);
    }
}

