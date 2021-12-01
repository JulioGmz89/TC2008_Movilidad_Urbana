using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : MonoBehaviour
{
    //float[20] Car;
    //public List<List<float>> Car = new List<List<float>>();

    public float[,] Car = new float[20, 2];
    void Start()
    {
        StartCoroutine(GetText());
        //Debug.Log(results);
        print(Car);
    }

    IEnumerator GetText()
    {
        int k = 0, j = 0;
        UnityWebRequest www = UnityWebRequest.Get("https://movilidadurbana.us-south.cf.appdomain.cloud/");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;

            string temp = "";

            for (int i = 0; i < results.Length - 9; i++)
            {
                if (char.IsDigit(results[i]) || results[i] == '-')
                {
                    int cont = i;
                    while (results[i] != ' ' || results[i] != ']')
                    {
                        temp += results[i];
                        i++;
                    }
                    //Debug.Log(results[i]);
                }
            }



            // string temp = "";
            // for (int i = 0; i < results.Length - 9; i++)
            // {
            //     if (results[i] == ' ' || results[i] == ']')
            //     {
            //         Car[k, j] = float.Parse(temp);
            //         if (j == 1)
            //         {
            //             Debug.Log("Webos");
            //             k++;
            //             j = 0;
            //             continue;
            //         }
            //         j++;
            //         Debug.Log(float.Parse(temp));
            //     }
            //     else if (results[i] == '[' || results[i] == '"')
            //     {
            //         continue;
            //     }
            //     else
            //     {
            //         temp = temp + results[i];
            //     }
            // }
            Debug.Log(results);
            // Or retrieve results as binary data
            // byte[] results = www.downloadHandler.data;
            // Debug.Log(results);
        }

    }


    void Update()
    {


    }
}
