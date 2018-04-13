using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BackendAnbindungHauptmenu : MonoBehaviour
{

    public InputField name;
    public string url = "loclahost:8080/imao/api/spiel";

    // Update is called once per frame
    void Update()
    {


    }


    public void fuerArzt()
    {

        string test = url + "arzt" + name.text + "/mustermann";
        Debug.Log(test);

        UnityWebRequest aufruf = new UnityWebRequest();  //Quasi ein GET
    }
}
