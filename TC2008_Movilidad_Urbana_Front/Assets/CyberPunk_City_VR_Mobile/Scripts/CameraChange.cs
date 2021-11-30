using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject RobotCamera;
    public GameObject SecCam1;
    public GameObject SecCam2;
    public GameObject SecCam3;
    public GameObject SecCam4;
    public int CamMode;
    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            if (CamMode == 4)
            {
                CamMode = 0;
            }
            else
            {
                CamMode++;
            }
            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (CamMode == 0)
        {
            SecCam1.SetActive(true);
            SecCam2.SetActive(false);
            SecCam3.SetActive(false);
            SecCam4.SetActive(false);
            RobotCamera.SetActive(false);
        }

        if (CamMode == 1)
        {
            SecCam1.SetActive(false);
            SecCam2.SetActive(true);
            SecCam3.SetActive(false);
            SecCam4.SetActive(false);
            RobotCamera.SetActive(false);
        }
        if (CamMode == 2)
        {
            SecCam1.SetActive(false);
            SecCam2.SetActive(false);
            SecCam3.SetActive(true);
            SecCam4.SetActive(false);
            RobotCamera.SetActive(false);
        }
        if (CamMode == 3)
        {
            SecCam1.SetActive(false);
            SecCam2.SetActive(false);
            SecCam3.SetActive(false);
            SecCam4.SetActive(true);
            RobotCamera.SetActive(false);
        }
        if (CamMode == 4)
        {
            SecCam1.SetActive(false);
            SecCam2.SetActive(false);
            SecCam3.SetActive(false);
            SecCam4.SetActive(false);
            RobotCamera.SetActive(true);
        }
    }
}
