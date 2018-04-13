using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogvorlage
{
    public int ID = -1;
    public string text = "";
    public Auswahl[] auswahl = new Auswahl[5];
}

public class Auswahl
{
    public string text = "";    //welcher Text wird bei diese Auswahl angezeigt?
    public int verweiseAufID = -1; //auf welche neue Dialogvorlage.ID wird verwiesen?
}

public class AnamneseManager : MonoBehaviour {

    private Dialogvorlage[] dialogvorlagen = new Dialogvorlage[25];
    private Dialogvorlage begruessung = new Dialogvorlage();
    private Dialogvorlage beenden = new Dialogvorlage();



    // Use this for initialization
    void Start () {

        
	}

    public void StarteDialog(Dialog dialog)
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
