using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Überprüft, ob eine Eingabe gültig ist und aktiviert oder deaktiviert ggf. Kontrollelemente
/// </summary>
public class EingabeUeberpruefung : MonoBehaviour {
    public InputField eingabe; //Eingabefeld, dessen Inhalt geprüft wird
    public Selectable[] elemente; //Elemente, die bei gültigem Namen aktiviert werden

	void Start () {
        if (eingabe.onValueChanged == null)
            eingabe.onValueChanged = new InputField.OnChangeEvent();

        eingabe.onValueChanged.AddListener(UeberpruefeEingabe);
	}
	
    private void UeberpruefeEingabe(string name)
    {
        foreach (var e in elemente)
            e.interactable = name.Length > 0;
    }
}
