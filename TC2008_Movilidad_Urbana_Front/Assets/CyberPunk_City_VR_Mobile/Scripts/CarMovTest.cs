using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovTest : MonoBehaviour
{
    public ConnectionManager ConMan;

    // Update is called once per frame
    void Update()
    {
        Vector3 CarDir = (transform.position - ConMan.Carro1).normalized;
        Quaternion CarRot = Quaternion.LookRotation(CarDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, CarRot, 20f * Time.deltaTime);
        //transform.Translate(ConMan.Carro1 * 10f);
        transform.position = Vector3.MoveTowards(transform.position, ConMan.Carro1, Time.deltaTime * 10f);
    }
}
