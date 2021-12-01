using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMov : MonoBehaviour
{
    Vector3[] positionArray = new Vector3[20];
    float speed;
    float rotationSpeed;
    int current;

    Vector3 originPos;
    void Start()
    {
        originPos = new Vector3(2.5f, 0f, -77f);
        #region Car Position
        positionArray[0] = new Vector3(-10f * 4, 0f, -2f * 4) + originPos;
        positionArray[1] = new Vector3(-10f * 4, 0f, -2f * 4) + originPos;
        positionArray[2] = new Vector3(-8f * 4, 0f, -2f * 4) + originPos;
        positionArray[3] = new Vector3(-7f * 4, 0f, -2f * 4) + originPos;
        positionArray[4] = new Vector3(-6f * 4, 0f, -2f * 4) + originPos;
        positionArray[5] = new Vector3(-5f * 4, 0f, -2f * 4) + originPos;
        positionArray[6] = new Vector3(-4f * 4, 0f, -2f * 4) + originPos;
        positionArray[7] = new Vector3(-3f * 4, 0f, -2f * 4) + originPos;
        positionArray[8] = new Vector3(-2.7f * 4, 0f, -2.08f * 4) + originPos;
        positionArray[9] = new Vector3(-2.46f * 4, 0f, -2.26f * 4) + originPos;
        positionArray[10] = new Vector3(-2.26f * 4, 0f, -2.47f * 4) + originPos;
        positionArray[11] = new Vector3(-2.11f * 4, 0f, -2.73f * 4) + originPos;
        positionArray[12] = new Vector3(-2f * 4, 0f, -3f * 4) + originPos;
        positionArray[13] = new Vector3(-2f * 4, 0f, -4f * 4) + originPos;
        positionArray[14] = new Vector3(-2f * 4, 0f, -5f * 4) + originPos;
        positionArray[15] = new Vector3(-2f * 4, 0f, -6f * 4) + originPos;
        positionArray[16] = new Vector3(-2f * 4, 0f, -7f * 4) + originPos;
        positionArray[17] = new Vector3(-2f * 4, 0f, -8f * 4) + originPos;
        positionArray[18] = new Vector3(-2f * 4, 0f, -9f * 4) + originPos;
        positionArray[19] = new Vector3(-2f * 4, 0f, -10f * 4) + originPos;
        #endregion

        speed = 10f;
        rotationSpeed = 20f;
        current = 0;
        // print(new Vector3(-10f, 0f, 0f) + originPos);
        // print(new Vector3(-10f, 0f, 0f) + new Vector3(-4f, 0f, 4f));
    }
    void Update()
    {
        Movement();
        if (transform.position == positionArray[current])
        {
            current++;
        }
    }

    void Movement()
    {
        Vector3 dir = (transform.position - positionArray[current]).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, positionArray[current], Time.deltaTime * speed);
    }

}
