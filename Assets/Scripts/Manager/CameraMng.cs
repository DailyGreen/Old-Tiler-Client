﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMng : MonoBehaviour
{
    public Camera mycam;
    public float moveSpeed;
    public int cameraLevel = 0;     // 멀리 보이는 정도 0, ~ 6 높을수록 멀리 보임
    [SerializeField]
    bool scrollUp = false;
    [SerializeField]
    bool scrollDown = false;
    [SerializeField]
    bool scrollPos = false;
    Quaternion beforeRot;
    public Animator camAnim;

    // Update is called once per frame
    void Update()
    {
        keyUpdate();
    }

    Vector3[] mainCamPos = { new Vector3(0, 0, 0), new Vector3(0, 9, -16), new Vector3(0, 28, -40) };
    Vector3[] mainCamRot = { new Vector3(25, 0, 0), new Vector3(27, 0, 0), new Vector3(34, 0, 0) };

    void keyUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-2, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(2, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -2);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraLevel < 12)
        {
            cameraLevel++;
            mycam.transform.position += new Vector3(0, 7, 7);
            //transform.position += new Vector3(0, 0, 7);
            //if (cameraLevel < 8)
                mycam.transform.Rotate(new Vector3(6, 0, 0));
            //mycam.transform.Rotate(new Vector3(3, 0, 0));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraLevel > 0)
        {
            cameraLevel--;
            mycam.transform.position += new Vector3(0, -7, -7);
            //transform.position += new Vector3(0, 0, -7);
            //if (cameraLevel < 8)
            mycam.transform.Rotate(new Vector3(-6, 0, 0));
            //mycam.transform.Rotate(new Vector3(-3, 0, 0));
        }
        /*if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraLevel < 10)
        {
            //Debug.Log("DOWN");
            cameraLevel++;
            //camAnim.speed = 1;
            //camAnim.SetInteger("cameraLevel", cameraLevel);
            transform.position += new Vector3(0, 3, 0);
            mycam.transform.position += new Vector3(0, 0, 5);
            mycam.transform.Rotate(new Vector3(10, 0, 0));
            mycam.fieldOfView += 8;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraLevel > 0)
        {
            //Debug.Log("UP");
            cameraLevel--;
            //camAnim.SetInteger("cameraLevel", cameraLevel);
            //mycam.transform.position += new Vector3(0, -3, 0);
            transform.position += new Vector3(0, -3, 0);
            mycam.transform.position += new Vector3(0, 0, -5);
            mycam.transform.Rotate(new Vector3(-10, 0, 0));
            mycam.fieldOfView -= 8;
        }*/
    }
}
