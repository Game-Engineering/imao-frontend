using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patientenbewegung2 : MonoBehaviour
{

    public Transform[] wegpunkteKommen;
    public Transform[] wegpunkteGehen;

    public float speed;

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
        /**
         * Alt; Dient als Aufhänger für automatisiertes Verhalten
         */
        //if (Variablen.patientVorhanden)
        //{
        //    if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
        //    {
        //        laufZaehler++;
        //        Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, 5 * Time.deltaTime);
        //        GetComponent<Rigidbody>().MovePosition(position);
        //        //NEU: beim Laufen/MovePosition wird die Walking variable des Animator auf true gesetzt, damit der state switch in die Running animation erfolgt
        //        GetComponent<Animator>().SetBool("Walking", true);
        //        Debug.Log("I'm moving 1");
        //    }
        //    else
        //    {
        //        if (!rotationAn)
        //        {
        //            if (wegpunkteKommen.Length - 2 < momentanerWegpunkt)
        //            {
        //                Debug.Log("Ziel erreicht");
        //                Variablen.patientInZelt = true;
        //                Variablen.patientVorhanden = false;
        //                momentanerWegpunkt = 0;
        //            }
        //            else
        //            {
        //                rotationAn = true;
        //                momentanerWegpunkt = (momentanerWegpunkt + 1);
        //                laufZaehler = 0;
        //            }
        //        }
        //    }

        //    if (transform.localRotation != wegpunkteKommen[momentanerWegpunkt - 1].localRotation && rotationAn)
        //    {
        //        Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteKommen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
        //        GetComponent<Rigidbody>().MoveRotation(rotation);
        //        //NEU: beim Rotieren/MoveRotation wird die Walking variable des Animator auf false gesetzt, damit der state switch zurück in die Idle animation erfolgt
        //        GetComponent<Animator>().SetBool("Walking", false);
        //        Debug.Log("I'm rotating 1");
        //    }
        //    else
        //    {
        //        rotationAn = false;
        //    }
        //}

        if (Variablen.patientVorhanden)
        {
            /**
             * Kp warum es nicht will
             */
            ////Bewegungsablauf
            //if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && !rotationAn)
            //{
            //    Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, 5 * Time.deltaTime);
            //    Debug.Log("Ich bewege mich!");
            //    GetComponent<Animator>().SetBool("Walking", true);
            //    GetComponent<Rigidbody>().MovePosition(position);
            //}
            //else
            //{
            //    if (!rotationAn)
            //    {
            //        if (wegpunkteKommen.Length == momentanerWegpunkt)
            //        {
            //            Debug.Log("Ziel erreicht");
            //            Variablen.patientInZelt = true;
            //            Variablen.patientVorhanden = false;
            //            momentanerWegpunkt = 0;
            //        }
            //        else
            //        {
            //            Debug.Log("Ich bin noch nicht am Ziel, zeit mich zu rotieren!");
            //            rotationAn = true;
            //            momentanerWegpunkt = (momentanerWegpunkt + 1);
            //        }
            //    }
            //}

            ////Rotation auf momentanen Wegpunkt abstimmen
            //if (transform.localRotation != wegpunkteKommen[momentanerWegpunkt].localRotation && rotationAn)
            //{
            //    Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteKommen[momentanerWegpunkt].localRotation, 50 * Time.deltaTime);
            //    Debug.Log("Ich rotiere! Sie hegen Groll!");
            //    GetComponent<Animator>().SetBool("Walking", false);
            //    GetComponent<Rigidbody>().MoveRotation(rotation);
            //}
            //else
            //{
            //    Debug.Log("Ich muss nicht mehr rotieren!");
            //    rotationAn = false;
            //}

            /**
             * Alt; Geht nicht aufgrund der Änderung durch die Animationen
             */
            //float step = speed * Time.deltaTime;

            //if(transform.position != wegpunkteKommen[momentanerWegpunkt].position)
            //{
            //    Debug.Log("Ich muss mich weiter rotieren.");
            //    Vector3 targetDir = wegpunkteKommen[momentanerWegpunkt].position - transform.position;

            //    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            //    Debug.DrawRay(transform.position, newDir, Color.red);

            //    // Move our position a step closer to the target.
            //    transform.rotation = Quaternion.LookRotation(newDir);
            //}
            //else
            //{
            //    Debug.Log("Auf zur nächsten Stelle!");
            //    momentanerWegpunkt++;
            //}

            float step = speed * Time.deltaTime;

            //if(transform.position != wegpunkteKommen[momentanerWegpunkt].position)
            //{
            //    Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, step);
            //    GetComponent<Rigidbody>().MovePosition(position);
            //}
            //else
            //{
            //    momentanerWegpunkt
            //}

            //    if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)

            if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
            {
                Debug.Log("Ich muss mich noch bewegen!");
                //Vector3 pos = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, step);
                //GetComponent<Rigidbody>().MovePosition(pos);

                Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, 5 * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(position);
            }
            else
            {
                Debug.Log("Momentaner Wegpunkt vor Erhöhen: " + momentanerWegpunkt);
                momentanerWegpunkt++;
                Debug.Log("Momentaner Wegpunkt nach Erhöhen: " + momentanerWegpunkt);
            }
        }

        if (Variablen.patientGeht)
        {
            Debug.Log("WIP");
        }

        /**
         * Alt; Dient als Aufhänger für automatisiertes Verhalten
         */


        //if (Variablen.patientGeht)
        //{
        //    if (transform.position != wegpunkteGehen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
        //    {
        //        laufZaehler++;
        //        Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteGehen[momentanerWegpunkt].position, 5 * Time.deltaTime);
        //        GetComponent<Rigidbody>().MovePosition(position);
        //        //NEU: beim Laufen/MovePosition wird die Walking variable des Animator auf true gesetzt, damit der state switch in die Running animation erfolgt
        //        GetComponent<Animator>().SetBool("Walking", true);
        //        Debug.Log("I'm moving 2");
        //    }
        //    else
        //    {
        //        if (!rotationAn)
        //        {
        //            if (wegpunkteGehen.Length - 2 < momentanerWegpunkt)
        //            {
        //                Debug.Log("Ziel erreicht");
        //                Variablen.patientInZelt = false;
        //                Variablen.patientGeht = false;
        //                momentanerWegpunkt = 0;
        //            }
        //            else
        //            {
        //                rotationAn = true;
        //                momentanerWegpunkt = (momentanerWegpunkt + 1);
        //                laufZaehler = 0;
        //            }
        //        }
        //    }

        //    if (transform.localRotation != wegpunkteGehen[momentanerWegpunkt - 1].localRotation && rotationAn)
        //    {
        //        Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteGehen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
        //        GetComponent<Rigidbody>().MoveRotation(rotation);
        //        //NEU: beim Rotieren/MoveRotation wird die Walking variable des Animator auf false gesetzt, damit der state switch zurück in die Idle animation erfolgt
        //        GetComponent<Animator>().SetBool("Walking", false);
        //        Debug.Log("I'm rotating 2");
        //    }
        //    else
        //    {
        //        rotationAn = false;
        //    }
        //}

    }
}
