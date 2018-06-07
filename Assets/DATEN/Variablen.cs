using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





public class Variablen
{
    ////Allgemein
    public static string spieler;
    public static string clickedButton;
    public static Runde runde = new Runde();
    public static string arztOderWirtschaft;

    ////Zelt
    //Anamnese
    public static Dialog anamnese = new Dialog();
    public static bool kameraFest = true;
    public static bool dialogOffen = false;

    //Diagnose
    public static string krankheitDiagnose;
    public static DiagnoseErgebnis diagnoseErgebnis;

    //Alles was den Patienten betrifft
    public static bool patientVorhanden = false;
    public static bool patientGeht = false;
    public static bool patientInZelt = false;
    public static bool patientInReichweite = false;
    public static bool blutbildEinsehbar = false;
    public static Patient momentanerPatient = new Patient();

    public static Blutbild blutbild = new Blutbild();



    ////Wirtschaft
    //Interview
    public static Dialog interview;

    //PK
    public static string iwasPKTEXT;

    //Berichte
    public static Arztbericht arztbericht;
    public static Budgetbericht budgetbericht;

    //Mails
    public static List<Mail> mailliste;
    public static SendeMailliste sendeMailliste;

}

[Serializable]
public class Runde
{
    public int budget;
    // public string nachricht;
    public int ruf;
    public int runde;
    public int wartendePatienten;
}

[Serializable]
public class Patient
{
    public int alter;
    public string vorname;
    public string nachname;
    public string geschlecht;
    public int erscheinungID;
    public int ID;
}

[Serializable]
public class Dialog
{
    public string name;
    public string antwort;
    public List<string> fragen;
    public string option;
}

[Serializable]
public class Blutbild
{
    public string budget;
    public string name;
    public double Erythrozyten;
    public double Leukozyten;
    public double Thrombozyten;
    public double Haemoglobinkonzentration;
    public double Haematokrit;
    public double MCH;
    public double MCHC;
    public double MCV;
}

[Serializable]
public class DiagnoseErgebnis
{
    public int budget;
    public string nachricht;
    public int ruf;
}

[Serializable]
public class Arztbericht
{
    public string ausgaben; //mehrere
    public string rufzuwachs; //mehrere
    public string rufverlust; //mehrere
    public int erfolgreichePatienten; //eine
    public int nichtErfolgreichePatienten; //eine
    public int rufbilanz; //eine
    public int gesamtausgaben; //eine
}

[Serializable]
public class Budgetbericht
{
    public string einnahmen; //mehrere
    public string ausgaben; //mehrere
    public string rufzuwachs; //mehrere
    public string rufverlust; //mehrere
    public int rufbilanz; //eine
    public int gesamtausgaben; //eine
}

[Serializable]
public class Mail
{
    public string absender;
    public string mailinhalt;
}

[Serializable]
public class SendeMailliste
{
    public string LOB;
    public string ABMAHNUNG;
    public string GERAET_GEKAUFT;
    public string DEFAULT_MAIL;
}