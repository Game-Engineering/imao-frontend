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

    void FixedUpdate()
    {

        if (Variablen.patientVorhanden)
        {
            if (transform.position != wegpunkteKommen[momentanerWegpunkt].position && laufZaehler < 90 && !rotationAn)
            {
                laufZaehler++;
                Vector3 position = Vector3.MoveTowards(transform.position, wegpunkteKommen[momentanerWegpunkt].position, 5 * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(position);
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
                Debug.Log(transform.localRotation + "Rotating" + wegpunkteKommen[momentanerWegpunkt - 1].localRotation);
                Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteKommen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
                GetComponent<Rigidbody>().MoveRotation(rotation);

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
                Debug.Log(transform.localRotation + "Rotating" + wegpunkteGehen[momentanerWegpunkt - 1].localRotation);
                Quaternion rotation = Quaternion.RotateTowards(transform.localRotation, wegpunkteGehen[momentanerWegpunkt - 1].localRotation, 50 * Time.deltaTime);
                GetComponent<Rigidbody>().MoveRotation(rotation);

            }
            else
            {
                rotationAn = false;
            }
        }

    }
}
