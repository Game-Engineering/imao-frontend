using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patientenbewegung2_copy : MonoBehaviour
{

    public Transform[] wegpunkteKommen;
    public Transform[] wegpunkteGehen;

    private int momentanerWegpunkt = 0;
    private int momentanerWegpunktGehen = 0;
    private bool sollLaufen;
    private Rigidbody rb;
    private Animator anim;
    private float geschwindigkeit = 3f;
    private Vector3 momentanePosition;
    //private Vector3 nächsterWegpunkt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        /**
         * Falls wir irgendwann Physics benutzen wollen
         */
        //Physics.gravity = new Vector3(0, -5, 0);
        //rb.centerOfMass = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Variablen.patientVorhanden)
        {
            Quaternion neueRotation = Quaternion.LookRotation(wegpunkteKommen[momentanerWegpunkt].position);
            transform.rotation = neueRotation;
            sollLaufen = true;
            //Debug.Log("#KOMMEN# Initiiere Lauf-Sequenz . . ." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
        }

        if (Variablen.patientGeht)
        {
            Quaternion neueRotationGehen = Quaternion.LookRotation(wegpunkteGehen[momentanerWegpunktGehen].position);
            transform.rotation = neueRotationGehen;
            sollLaufen = true;
            Debug.Log(sollLaufen);
            //Debug.Log("#GEHEN# Initiiere Lauf-Sequenz . . ." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
        }
    }

    void FixedUpdate()
    {
        if (Variablen.patientVorhanden)
        {
            if (sollLaufen)
            {
                //Debug.Log("#KOMMEN# LAUF, WALD, LAUF!" + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                GetComponent<Animator>().SetBool("Walking", true);
                momentanePosition = transform.position;
                rb.MovePosition(Vector3.MoveTowards(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position, Time.fixedDeltaTime * geschwindigkeit));

                if (momentanerWegpunkt < 2 && Vector3.Distance(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position) < 0.1)
                {
                    momentanerWegpunkt++;
                }
                else if (Vector3.Distance(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position) < 0.1)
                {
                    //Debug.Log("#KOMMEN# Boah, lass mal aufhören zu laufen." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                    sollLaufen = false;
                    Variablen.patientInZelt = true;
                    anim.SetBool("Walking", false);
                }
            }
        }

        if (Variablen.patientGeht)
        {
            if (sollLaufen)
            {
                //Debug.Log("#GEHEN# LAUF, WALD, LAUF!" + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                GetComponent<Animator>().SetBool("Walking", true);
                momentanePosition = transform.position;
                rb.MovePosition(Vector3.MoveTowards(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position, Time.fixedDeltaTime * geschwindigkeit));

                if (momentanerWegpunktGehen < 2 && Vector3.Distance(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position) < 0.1)
                {
                    momentanerWegpunktGehen++;
                }
                else if (Vector3.Distance(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position) < 0.1)
                {
                    //Debug.Log("#GEHEN# Boah, lass mal aufhören zu laufen." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                    sollLaufen = false;
                    anim.SetBool("Walking", false);
                    Variablen.patientGeht = false;
                }
            }
        }

        ////testen fürs sterben (lul)
        ///**
        // * kp wie die Abfrage ist für's sterben; gibt's was in Variablen was gesettet wird, wenn die falsche Diagnose gesetzt wird?
        // * am besten: patientVorhanden UND patientGeht werden auf false gesetzt, eine neue variable patientStirbt auf true? idk ob das der fall ist
        // */

        //if ()
        //{
        //    GetComponent<Animator>().SetBool("isAlive", false);
        //}

    }
}

/**
 * Ideen warum es nicht geht:
 * - alles unter ein FixedUpdate/Update nicht auftrennen?
 * - patientGeht wird nicht auf true gesetzt?
 * - honestly kp was sonst da es legit copy paste von dem davor ist was läuft; sie haben separate wegpunkte, separate wegpunkt counter, momentanePosition wird immer neu genommen und
 *   obwohl sollLaufen im FixedUpdate auf falsch gesetzt wird, wird es im Udpate wieder auf true gesetzt, sobal patientGeht true ist
 */ 