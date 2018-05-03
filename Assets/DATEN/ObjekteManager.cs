using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjekteManager : MonoBehaviour {

    public Text fTipp;
    void setfTipp()
    {
        Objekte.fTipp = fTipp;
    }

    private void Start()
    {
        setfTipp();
    }


}

