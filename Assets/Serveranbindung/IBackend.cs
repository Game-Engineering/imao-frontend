using UnityEngine;
using System.Collections;

public interface IBackend{
    string neuesSpiel();
    string getAktuelleBelegung();
    string getBelegung(int nummer);
    string getSpielDaten();
    string getAlleErlaubtenZuege();
    string getErlaubteZuege(string feld);
    string getFigur(string feld);
    string ziehe(string von,string nach);
    string getZugHistorie();
}
