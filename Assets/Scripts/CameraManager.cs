using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
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

    void keyUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1);
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraLevel < 10)
        {
            //Debug.Log("DOWN");
            cameraLevel++;
            //camAnim.speed = 1;
            //camAnim.SetInteger("cameraLevel", cameraLevel);
            mycam.transform.position += new Vector3(0, 0, 1);
            mycam.transform.Rotate(new Vector3(2, 0, 0));
            mycam.fieldOfView += 4;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraLevel > 0)
        {
            //Debug.Log("UP");
            cameraLevel--;
            //camAnim.SetInteger("cameraLevel", cameraLevel);
            //mycam.transform.position += new Vector3(0, -3, 0);
            mycam.transform.position += new Vector3(0, 0, -1);
            mycam.transform.Rotate(new Vector3(-2, 0, 0));
            mycam.fieldOfView -= 4;
        }
    }
}
