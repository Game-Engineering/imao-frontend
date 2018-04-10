using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PatientenVariablen {

    public Name name;
    public Symptome symptome;
    public int alter;
    public int id;

}

[Serializable]
public class Name
{
    public string vorname;
    public string nachname;
}
[Serializable]
public class Symptome
{
    public string symptom1;
    public string symptom2;
    public string symptom3;
    public string symptom4;
    public string symptom5;
}
