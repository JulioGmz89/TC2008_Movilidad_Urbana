using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : MonoBehaviour
{
    //float[20] Car;
    //public List<List<float>> Car = new List<List<float>>();

    public float[,] Cars = new float[20, 2];
    public Vector3 Carro1, Carro2, Carro3, Carro4, Carro5, Carro6, Carro7, Carro8, Carro9, Carro10, Carro11, Carro12, Carro13, Carro14, Carro15, Carro16, Carro17, Carro18, Carro19, Carro20;
    public GameObject Carro1Prfab, Carro2Prfab, Carro3Prfab, Carro4Prfab, Carro5Prfab, Carro6Prfab, Carro7Prfab, Carro8Prfab, Carro9Prfab, Carro10Prfab, Carro11Prfab, Carro12Prfab, Carro13Prfab, Carro14Prfab, Carro15Prfab, Carro16Prfab, Carro17Prfab, Carro18Prfab, Carro19Prfab, Carro20Prfab;
    public bool[] isSpawned = new bool[20];

    bool isMoving1, isMoving2;
    Vector3 originPos;

    public bool semaforo1;
    public bool semaforo2;
    public bool semaforo3;
    public bool semaforo4;

    float timeRemaining = .3f;
    void Start()
    {
        originPos = new Vector3(-1.3f, 0f, -81.7f);
        // Instantiate(Carro1Prfab, new Vector3(0, 0, -40) + originPos, Quaternion.identity);
        // Instantiate(Carro2Prfab, new Vector3(40, 0, 4) + originPos, Quaternion.identity);
        // Instantiate(Carro3Prfab, new Vector3(40, 0, 0) + originPos, Quaternion.identity);
        // Instantiate(Carro4Prfab, new Vector3(-4, 0, 40) + originPos, Quaternion.identity);
        // Instantiate(Carro5Prfab, new Vector3(-40, 0, 0) + originPos, Quaternion.identity);

    }

    IEnumerator GetText()
    {
        int k = 0;
        UnityWebRequest www = UnityWebRequest.Get("http://10.25.75.224:8000/");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            Debug.Log(results);

            #region Control de semaforos
            if (results[results.Length - 1] == '0')
            {
                semaforo1 = false;
            }
            else { semaforo1 = true; }

            if (results[results.Length - 3] == '0')
            {
                semaforo2 = false;
            }
            else { semaforo2 = true; }
            if (results[results.Length - 5] == '0')
            {
                semaforo3 = false;
            }
            else { semaforo3 = true; }
            if (results[results.Length - 7] == '0')
            {
                semaforo4 = false;
            }
            else { semaforo4 = true; }
            #endregion


            #region Split de string a puntos x y

            string temp = "";
            for (int i = 0; i < results.Length - 9; i++)
            {
                if (char.IsDigit(results[i]) || results[i] == '-')
                {
                    while (results[i] != ' ')
                    {
                        temp += results[i];
                        i++;
                    }
                    temp += '0';
                    Cars[k, 0] = float.Parse(temp);
                    temp = "";

                    while (results[i] != ']')
                    {
                        temp += results[i];
                        i++;
                    }
                    temp += '0';
                    Cars[k, 1] = float.Parse(temp);
                    temp = "";
                    k++;
                }
                #endregion

                #region Vectores Carros
                Carro1 = new Vector3(Cars[0, 0], 0, Cars[0, 1]) + originPos;
                Carro2 = new Vector3(Cars[1, 0], 0, Cars[1, 1]) + originPos;
                Carro3 = new Vector3(Cars[2, 0], 0, Cars[2, 1]) + originPos;
                Carro4 = new Vector3(Cars[3, 0], 0, Cars[3, 1]) + originPos;
                Carro5 = new Vector3(Cars[4, 0], 0, Cars[4, 1]) + originPos;
                Carro6 = new Vector3(Cars[5, 0], 0, Cars[5, 1]);
                Carro7 = new Vector3(Cars[6, 0], 0, Cars[6, 1]);
                Carro8 = new Vector3(Cars[7, 0], 0, Cars[7, 1]);
                Carro9 = new Vector3(Cars[8, 0], 0, Cars[8, 1]);
                Carro10 = new Vector3(Cars[9, 0], 0, Cars[9, 1]);
                Carro11 = new Vector3(Cars[10, 0], 0, Cars[10, 1]);
                Carro12 = new Vector3(Cars[11, 0], 0, Cars[11, 1]);
                Carro13 = new Vector3(Cars[12, 0], 0, Cars[12, 1]);
                Carro14 = new Vector3(Cars[13, 0], 0, Cars[13, 1]);
                Carro15 = new Vector3(Cars[14, 0], 0, Cars[14, 1]);
                Carro16 = new Vector3(Cars[15, 0], 0, Cars[15, 1]);
                Carro17 = new Vector3(Cars[16, 0], 0, Cars[16, 1]);
                Carro18 = new Vector3(Cars[17, 0], 0, Cars[17, 1]);
                Carro19 = new Vector3(Cars[18, 0], 0, Cars[18, 1]);
                Carro20 = new Vector3(Cars[19, 0], 0, Cars[19, 1]);
                #endregion

            }
        }
    }

    void Update()
    {
        StartCoroutine(GetText());
        InstantiateCars();
        if (Mathf.Round(timeRemaining) > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (Mathf.Round(timeRemaining) == 0)
        {
            CarMovement();
            timeRemaining = .3f;
        }




        // if (isSpawned[0])
        // {
        //     CarMovement(Carro1Prfab, Carro1);
        // }
        // if (isSpawned[1])
        // {
        //     CarMovement(Carro2Prfab, Carro2);
        // }
        // if (isSpawned[2])
        // {
        //     CarMovement(Carro3Prfab, Carro3);
        // }
        // if (isSpawned[3])
        // {
        //     CarMovement(Carro4Prfab, Carro4);
        // }
        // if (isSpawned[4])
        // {
        //     CarMovement(Carro5Prfab, Carro5);
        // }
        // if (isSpawned[5])
        // {
        //     CarMovement(Carro6Prfab, Carro6);
        // }
        // if (isSpawned[6])
        // {
        //     CarMovement(Carro7Prfab, Carro7);
        // }
        // if (isSpawned[7])
        // {
        //     CarMovement(Carro8Prfab, Carro8);
        // }
        // if (isSpawned[8])
        // {
        //     CarMovement(Carro9Prfab, Carro9);
        // }
        // if (isSpawned[9])
        // {
        //     CarMovement(Carro10Prfab, Carro10);
        // }
        // if (isSpawned[10])
        // {
        //     CarMovement(Carro11Prfab, Carro11);
        // }
        // if (isSpawned[11])
        // {
        //     CarMovement(Carro12Prfab, Carro12);
        // }
        // if (isSpawned[12])
        // {
        //     CarMovement(Carro13Prfab, Carro13);
        // }
        // if (isSpawned[13])
        // {
        //     CarMovement(Carro14Prfab, Carro14);
        // }
        // if (isSpawned[14])
        // {
        //     CarMovement(Carro15Prfab, Carro15);
        // }
        // if (isSpawned[15])
        // {
        //     CarMovement(Carro16Prfab, Carro16);
        // }
        // if (isSpawned[16])
        // {
        //     CarMovement(Carro17Prfab, Carro17);
        // }
        // if (isSpawned[17])
        // {
        //     CarMovement(Carro18Prfab, Carro18);
        // }
        // if (isSpawned[18])
        // {
        //     CarMovement(Carro19Prfab, Carro19);
        // }
        // if (isSpawned[19])
        // {
        //     CarMovement(Carro20Prfab, Carro20);
        // }



    }

    void InstantiateCars()
    {
        if (Carro1 != new Vector3(0, 0, 0) && !isSpawned[0])
        {
            Carro1Prfab.transform.position = Carro1;
            isSpawned[0] = true;
        }

        if (Carro2 != new Vector3(0, 0, 0) && !isSpawned[1])
        {
            Carro2Prfab.transform.position = Carro2;
            isSpawned[1] = true;
        }

        if (Carro3 != new Vector3(0, 0, 0) && !isSpawned[2])
        {
            Carro3Prfab.transform.position = Carro3;
            isSpawned[2] = true;
        }

        if (Carro4 != new Vector3(0, 0, 0) && !isSpawned[3])
        {
            Carro4Prfab.transform.position = Carro4;
            isSpawned[3] = true;
        }

        if (Carro5 != new Vector3(0, 0, 0) && !isSpawned[4])
        {
            Carro5Prfab.transform.position = Carro5;
            isSpawned[4] = true;
        }

        if (Carro6 != new Vector3(0, 0, 0) && !isSpawned[5])
        {
            Carro6Prfab.transform.position = Carro6 + originPos;
            isSpawned[5] = true;
        }

        if (Carro7 != new Vector3(0, 0, 0) && !isSpawned[6])
        {
            Carro7Prfab.transform.position = Carro7 + originPos;
            isSpawned[6] = true;
        }

        if (Carro8 != new Vector3(0, 0, 0) && !isSpawned[7])
        {
            Carro8Prfab.transform.position = Carro8 + originPos;
            isSpawned[7] = true;
        }
        if (Carro9 != new Vector3(0, 0, 0) && !isSpawned[8])
        {
            Carro9Prfab.transform.position = Carro9 + originPos;
            isSpawned[8] = true;
        }
        if (Carro10 != new Vector3(0, 0, 0) && !isSpawned[9])
        {
            Carro10Prfab.transform.position = Carro10 + originPos;
            isSpawned[9] = true;
        }
        if (Carro11 != new Vector3(0, 0, 0) && !isSpawned[10])
        {
            Carro11Prfab.transform.position = Carro11 + originPos;
            isSpawned[10] = true;
        }

        if (Carro12 != new Vector3(0, 0, 0) && !isSpawned[11])
        {
            Carro12Prfab.transform.position = Carro12 + originPos;
            isSpawned[11] = true;
        }

        if (Carro13 != new Vector3(0, 0, 0) && !isSpawned[12])
        {
            Carro13Prfab.transform.position = Carro13 + originPos;
            isSpawned[12] = true;
        }

        if (Carro14 != new Vector3(0, 0, 0) && !isSpawned[13])
        {
            Carro14Prfab.transform.position = Carro14 + originPos;
            isSpawned[13] = true;
        }

        if (Carro15 != new Vector3(0, 0, 0) && !isSpawned[14])
        {
            Carro15Prfab.transform.position = Carro15 + originPos;
            isSpawned[14] = true;
        }

        if (Carro16 != new Vector3(0, 0, 0) && !isSpawned[15])
        {
            Carro16Prfab.transform.position = Carro16 + originPos;
            isSpawned[15] = true;
        }

        if (Carro17 != new Vector3(0, 0, 0) && !isSpawned[16])
        {
            Carro17Prfab.transform.position = Carro17 + originPos;
            isSpawned[16] = true;
        }

        if (Carro18 != new Vector3(0, 0, 0) && !isSpawned[17])
        {
            Carro18Prfab.transform.position = Carro18 + originPos;
            isSpawned[17] = true;
        }
        if (Carro19 != new Vector3(0, 0, 0) && !isSpawned[18])
        {
            Carro19Prfab.transform.position = Carro19 + originPos;
            isSpawned[18] = true;
        }
        if (Carro20 != new Vector3(0, 0, 0) && !isSpawned[19])
        {
            Carro20Prfab.transform.position = Carro20 + originPos;
            isSpawned[19] = true;
        }
    }

    // void CarMovement(GameObject Car, Vector3 CarVec)
    // {

    //     Vector3 CarDir = (Car.transform.position - (CarVec + originPos));
    //     Quaternion CarRot = Quaternion.LookRotation(CarDir);
    //     Car.transform.rotation = Quaternion.Slerp(Car.transform.rotation, CarRot, 5f * Time.deltaTime);
    //     Car.transform.position = Vector3.MoveTowards(Car.transform.position, CarVec + originPos, Time.deltaTime * 10f);
    // }

    void CarMovement()
    {

        Vector3 Carro1Dir = (Carro1Prfab.transform.position - (Carro1));
        Quaternion Carro1Rot = Quaternion.LookRotation(Carro1Dir);
        Carro1Prfab.transform.rotation = Quaternion.Slerp(Carro1Prfab.transform.rotation, Carro1Rot, 5f * Time.deltaTime);
        Carro1Prfab.transform.position = Vector3.MoveTowards(Carro1Prfab.transform.position, Carro1, Time.deltaTime * 10f);

        Vector3 Carro2Dir = (Carro2Prfab.transform.position - (Carro2 + originPos));
        Quaternion Carro2Rot = Quaternion.LookRotation(Carro2Dir);
        Carro2Prfab.transform.rotation = Quaternion.Slerp(Carro2Prfab.transform.rotation, Carro2Rot, 5f * Time.deltaTime);
        Carro2Prfab.transform.position = Vector3.MoveTowards(Carro2Prfab.transform.position, Carro2, Time.deltaTime * 10f);

        Vector3 Carro3Dir = (Carro3Prfab.transform.position - (Carro3 + originPos));
        Quaternion Carro3Rot = Quaternion.LookRotation(Carro3Dir);
        Carro3Prfab.transform.rotation = Quaternion.Slerp(Carro3Prfab.transform.rotation, Carro3Rot, 5f * Time.deltaTime);
        Carro3Prfab.transform.position = Vector3.MoveTowards(Carro3Prfab.transform.position, Carro3, Time.deltaTime * 10f);

        Vector3 Carro4Dir = (Carro4Prfab.transform.position - (Carro4 + originPos));
        Quaternion Carro4Rot = Quaternion.LookRotation(Carro4Dir);
        Carro4Prfab.transform.rotation = Quaternion.Slerp(Carro4Prfab.transform.rotation, Carro4Rot, 5f * Time.deltaTime);
        Carro4Prfab.transform.position = Vector3.MoveTowards(Carro4Prfab.transform.position, Carro4, Time.deltaTime * 10f);

        Vector3 Carro5Dir = (Carro5Prfab.transform.position - (Carro5 + originPos));
        Quaternion Carro5Rot = Quaternion.LookRotation(Carro5Dir);
        Carro5Prfab.transform.rotation = Quaternion.Slerp(Carro5Prfab.transform.rotation, Carro5Rot, 5f * Time.deltaTime);
        Carro5Prfab.transform.position = Vector3.MoveTowards(Carro5Prfab.transform.position, Carro5, Time.deltaTime * 10f);

    }
}
