using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disabler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject.Find("StelleDiagnose").GetComponent<Button>().interactable = false;
        GameObject.Find("RufePatienten").GetComponent<Button>().interactable = false;
        GameObject.Find("ZeigeBlutbild").GetComponent<Button>().interactable = false;
        GameObject.Find("StarteDialog").GetComponent<Button>().interactable = false;
    }

}
