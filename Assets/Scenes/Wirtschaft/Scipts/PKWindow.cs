using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PKWindow : MonoBehaviour
{

    public GameObject background;
    public Text text;

    private string antwort;

    public void haltePK()
    {
        StartCoroutine(getPK());
    }

    IEnumerator getPK()
    {
        UnityWebRequest aufruf = new UnityWebRequest(Konstanten.URL + "haltePressekonferenz");
        aufruf.downloadHandler = new DownloadHandlerBuffer();
        yield return aufruf.SendWebRequest();

        if (aufruf.isNetworkError || aufruf.isHttpError)
        {
            Debug.Log(aufruf.error);
        }
        else
        {
            antwort = aufruf.downloadHandler.text;
            text.text = antwort; //muss noch JSONObjekt erhalten
            background.SetActive(true);
            yield return new WaitForSeconds(4);
            background.SetActive(false);
        }

    }




}
