using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patientenbewegung2 : MonoBehaviour
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
            //Debug.Log("Initiiere Lauf-Sequenz . . ." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
        }

        if (Variablen.patientGeht)
        {
            Quaternion neueRotationGehen = Quaternion.LookRotation(wegpunkteGehen[momentanerWegpunktGehen].position);
            transform.rotation = neueRotationGehen;
            sollLaufen = true;
            //Debug.Log("Initiiere Lauf-Sequenz . . ." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
        }
    }

    void FixedUpdate()
    {
        if (Variablen.patientVorhanden)
        {
            if (sollLaufen)
            {
                //Debug.Log("LAUF, WALD, LAUF!" + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                GetComponent<Animator>().SetBool("Walking", true);
                momentanePosition = transform.position;
                rb.MovePosition(Vector3.MoveTowards(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position, Time.fixedDeltaTime * geschwindigkeit));

                if (momentanerWegpunkt < 2 && Vector3.Distance(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position) < 0.1)
                {
                    momentanerWegpunkt++;
                }
                else if (Vector3.Distance(momentanePosition, wegpunkteKommen[momentanerWegpunkt].position) < 0.1)
                {
                    //Debug.Log("Boah, lass mal aufhören zu laufen." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                    sollLaufen = false;
                    anim.SetBool("Walking", false);
                }
            }
        }

        if (Variablen.patientGeht)
        {
            if (sollLaufen)
            {
                //Debug.Log("LAUF, WALD, LAUF!" + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                GetComponent<Animator>().SetBool("Walking", true);
                momentanePosition = transform.position;
                rb.MovePosition(Vector3.MoveTowards(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position, Time.fixedDeltaTime * geschwindigkeit));

                if (momentanerWegpunktGehen < 2 && Vector3.Distance(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position) < 0.1)
                {
                    momentanerWegpunktGehen++;
                }
                else if (Vector3.Distance(momentanePosition, wegpunkteGehen[momentanerWegpunktGehen].position) < 0.1)
                {
                    //Debug.Log("Boah, lass mal aufhören zu laufen." + "Momentaner Wegpunkt: " + momentanerWegpunkt);
                    sollLaufen = false;
                    anim.SetBool("Walking", false);
                }
            }
        }
    }
}
