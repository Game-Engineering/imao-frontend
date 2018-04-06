using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    private Queue<string> saetze;
    public Text patientenName;
    public Text dialogText;
    public Canvas can;

	// Use this for initialization
	void Start () {
        saetze = new Queue<string>();
	}

    public void StarteDialog (Dialog dialog)
    {
        patientenName.text = dialog.patientname;

        saetze.Clear();

        foreach(string satz in dialog.satz) {
            saetze.Enqueue(satz);
        }
        can.gameObject.SetActive(true);
        ZeigeNächstenSatz();
    }

    public bool ZeigeNächstenSatz ()
    {
        if (saetze.Count == 0)
        {
            BeendeDialog();
            can.gameObject.SetActive(false);
            return false;
        }

        string satz = saetze.Dequeue();
        dialogText.text = satz;
            return true;
    }

    void BeendeDialog()
    {
        Debug.Log("Konversation beendet");
    }
}
