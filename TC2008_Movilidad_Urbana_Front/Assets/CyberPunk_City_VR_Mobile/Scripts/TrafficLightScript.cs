using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightScript : MonoBehaviour
{
    //public ConnectionManager ConMan;
    public GameObject LuzVerdeNorte, LuzRojaNorte, LuzVerdeSur, LuzRojaSur, LuzVerdeEste, LuzRojaEste, LuzVerdeOeste, LuzRojaOeste;
    // Update is called once per frame
    float timeRemaining = 8f;
    void Update()
    {
        if (Mathf.Round(timeRemaining) > 0)
        {
            LuzVerdeOeste.SetActive(true);
            LuzVerdeSur.SetActive(false);
            LuzVerdeEste.SetActive(false);
            LuzVerdeNorte.SetActive(false);

            LuzRojaOeste.SetActive(false);
            LuzRojaEste.SetActive(true);
            LuzRojaNorte.SetActive(true);
            LuzRojaSur.SetActive(true);
            timeRemaining -= Time.deltaTime;
        }
        if (Mathf.Round(timeRemaining) == 0)
        {
            float timeRemaining1 = 9f;
            if (Mathf.Round(timeRemaining1) > 0)
            {
                LuzVerdeOeste.SetActive(false);
                LuzVerdeSur.SetActive(false);
                LuzVerdeEste.SetActive(false);
                LuzVerdeNorte.SetActive(true);

                LuzRojaOeste.SetActive(true);
                LuzRojaEste.SetActive(true);
                LuzRojaNorte.SetActive(false);
                LuzRojaSur.SetActive(true);
                timeRemaining1 -= Time.deltaTime;

            }
            else
            {
                LuzVerdeOeste.SetActive(false);
                LuzVerdeSur.SetActive(true);
                LuzVerdeEste.SetActive(false);
                LuzVerdeNorte.SetActive(false);

                LuzRojaOeste.SetActive(true);
                LuzRojaEste.SetActive(true);
                LuzRojaNorte.SetActive(true);
                LuzRojaSur.SetActive(false);
            }
        }
        // if (ConMan.semaforo1)
        // {
        //     LuzVerdeNorte.SetActive(true);
        //     LuzRojaNorte.SetActive(false);
        // }
        // else
        // {
        //     LuzVerdeNorte.SetActive(false);
        //     LuzRojaNorte.SetActive(true);
        // }

        // if (ConMan.semaforo2)
        // {
        //     LuzVerdeSur.SetActive(true);
        //     LuzRojaSur.SetActive(false);
        // }
        // else
        // {
        //     LuzVerdeSur.SetActive(false);
        //     LuzRojaSur.SetActive(true);
        // }

        // if (ConMan.semaforo3)
        // {
        //     LuzVerdeEste.SetActive(true);
        //     LuzRojaEste.SetActive(false);
        // }
        // else
        // {
        //     LuzVerdeEste.SetActive(false);
        //     LuzRojaEste.SetActive(true);
        // }


        // if (ConMan.semaforo4)
        // {
        //     LuzVerdeOeste.SetActive(true);
        //     LuzRojaOeste.SetActive(false);
        // }
        // else
        // {
        //     LuzVerdeOeste.SetActive(false);
        //     LuzRojaOeste.SetActive(true);
        // }
    }
}

