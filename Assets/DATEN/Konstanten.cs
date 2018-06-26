using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Konstanten
{
    public static string URL = "localhost:8080/imao/api/spiel/";
    public static string URLfest = "localhost:8080/imao/api/spiel/";
    //public static string URL = "http://8e03c25c.ngrok.io/imao/api/spiel/";
    //public static string URLfest = "http://8e03c25c.ngrok.io/imao/api/spiel/";

    public static int POLLING = 100; // nach dieser Anzahl Frames erfolgt ein Update-Poll
}
