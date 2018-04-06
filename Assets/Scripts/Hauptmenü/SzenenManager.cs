using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Erlaubt Zugriff auf die Szenenlogik durch Scripts
/// </summary>
public class SzenenManager : MonoBehaviour {
    public void Beende()
    {
        IMAO.Instanz.Beende();
    }

    public void LadeHauptmenue()
    {
        IMAO.Instanz.LadeHauptmenue();
    }

	public void LadeZelt()
    {
        IMAO.Instanz.LadeZelt();
    }

    public void LadeWirtschaft()
    {
        IMAO.Instanz.LadeWirtschaft();
    }
}
