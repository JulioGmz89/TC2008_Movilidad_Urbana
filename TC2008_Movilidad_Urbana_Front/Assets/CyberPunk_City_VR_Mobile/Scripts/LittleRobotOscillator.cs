using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobotOscillator : MonoBehaviour
{
    float timeCounter;
    float speed;
    float width;
    float height;

    Vector3 originPos;
    void Start()
    {
        timeCounter = 0;
        speed = .1f;
        width = 10f;
        height = 10f;
        originPos = new Vector3(-1f, 0f, -80f);
    }

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = 15f;
        float z = Mathf.Sin(timeCounter) * height;

        transform.position = new Vector3(x, y, z) + originPos;


        Vector3 relativePos = originPos - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
