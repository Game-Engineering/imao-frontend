using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton zur globalen Verwaltung der Szenenlogik
/// </summary>
public class IMAO {
    #region Singleton
    private static IMAO instanz;

    public static IMAO Instanz
    {
        get
        {
            if (instanz == null)
                return instanz = new IMAO();
            return instanz;
        }
    }

    private IMAO() { }
    #endregion

    public void Beende()
    {
        Application.Quit();
    }

    public void LadeHauptmenue()
    {
        SceneManager.LoadScene("Hauptmenü");
    }

    public void LadeZelt()
    {
        SceneManager.LoadScene("Zelt");
    }

    public void LadeWirtschaft()
    {
        SceneManager.LoadScene("Wirtschaft");
    }
}
