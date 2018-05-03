using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patientenbewegung : MonoBehaviour
{

    public Transform[] wegpunkteKommen;
    public Transform[] wegpunkteGehen;

    private int momentanerWegpunkt = 0;
    private int laufZaehler = 0;
    private bool rotationAn = false;

    /**
     * Versuch per Start die rotation zu fixen; Problem: anscheinend will es das Quaternion in ein void konvertieren; kp warum
     */
    //void Start()
    //{
    //    Quaternion fixRotation = new Quaternion();
    //    GetComponent<Rigidbody>().rotation = fixRotation.Set(0, 0, 0, 1);
    //}

    void FixedUpdate()
    {
        
        if (Variablen.patientVorhanden)
        {
            if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
            {
                laufZaehler++;
                Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, 5 * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(position);
                //NEU: beim Laufen/MovePosition wird die Walking variable des Animator auf true gesetzt, damit der state switch in die Running animation erfolgt
                GetComponent<Animator>().SetBool("Walking", true);
                Debug.Log("I'm moving 1");
            }
            else
            {
                if (!rotationAn)
                {
                    if (wegpunkteKommen.Length - 2 < momentanerWegpunkt)
                    {
                        Debug.Log("Ziel erreicht");
                        Variablen.patientInZelt = true;
                        Variablen.patientVorhanden = false;
                        momentanerWegpunkt = 0;
                    }
                    else
                    {
                        rotationAn = true;
                        momentanerWegpunkt = (momentanerWegpunkt + 1);
                        laufZaehler = 0;
                    }
                }
            }

            if (transform.localRotation != wegpunkteKommen[momentanerWegpunkt - 1].localRotation && rotationAn)
            {
                Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteKommen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
                GetComponent<Rigidbody>().MoveRotation(rotation);
                //NEU: beim Rotieren/MoveRotation wird die Walking variable des Animator auf false gesetzt, damit der state switch zurück in die Idle animation erfolgt
                GetComponent<Animator>().SetBool("Walking", false);
                Debug.Log("I'm rotating 1");
            }
            else
            {
                rotationAn = false;
            }
        }

        if (Variablen.patientGeht)
        {
            if (transform.position != wegpunkteGehen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
            {
                laufZaehler++;
                Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteGehen[momentanerWegpunkt].position, 5 * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(position);
                //NEU: beim Laufen/MovePosition wird die Walking variable des Animator auf true gesetzt, damit der state switch in die Running animation erfolgt
                GetComponent<Animator>().SetBool("Walking", true);
                Debug.Log("I'm moving 2");
            }
            else
            {
                if (!rotationAn)
                {
                    if (wegpunkteGehen.Length - 2 < momentanerWegpunkt)
                    {
                        Debug.Log("Ziel erreicht");
                        Variablen.patientInZelt = false;
                        Variablen.patientGeht = false;
                        momentanerWegpunkt = 0;
                    }
                    else
                    {
                        rotationAn = true;
                        momentanerWegpunkt = (momentanerWegpunkt + 1);
                        laufZaehler = 0;
                    }
                }
            }

            if (transform.localRotation != wegpunkteGehen[momentanerWegpunkt - 1].localRotation && rotationAn)
            {
                Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteGehen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
                GetComponent<Rigidbody>().MoveRotation(rotation);
                //NEU: beim Rotieren/MoveRotation wird die Walking variable des Animator auf false gesetzt, damit der state switch zurück in die Idle animation erfolgt
                GetComponent<Animator>().SetBool("Walking", false);
                Debug.Log("I'm rotating 2");
            }
            else
            {
                rotationAn = false;
            }
        }

    }
}
