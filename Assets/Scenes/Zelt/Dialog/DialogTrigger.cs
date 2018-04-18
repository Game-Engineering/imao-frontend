using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{

    public Canvas can;

    private void OnTriggerEnter(Collider other)
    {
        Variablen.patientInReichweite = true;
        Objekte.fTipp.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Variablen.patientInReichweite = false;
        Objekte.fTipp.gameObject.SetActive(false);
    }
}
