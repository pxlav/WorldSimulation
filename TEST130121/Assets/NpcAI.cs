using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] float rayLenght;
    [SerializeField] bool isDriveForward;
    [SerializeField] bool isDriveBack;
    [SerializeField] bool isDriveLeft;
    [SerializeField] float backTimer;
    [SerializeField] bool isStop;
    [SerializeField] Vector3 dir;
    private void Start()
    {
        rayLenght = 2.5f;
        backTimer = 0.5f;
        isDriveForward = true;
    }

    private void FixedUpdate()
    {
        RayCastHitting();
        if (isStop == false)
        {
            if (isDriveForward == true)
            {
                DriveForward();
            }
            else
            {
                DriveBack();
            }
        }
    }

    public void DriveForward()
    {
        this.transform.Translate(Vector3.forward / 20);
    }
    public void DriveBack()
    {
        this.transform.Translate(-Vector3.forward / 20);
    }
    public void DriveLeft()
    {
        this.transform.Rotate(Vector3.up * 4);
    }
    public void DriveRight()
    {
        this.transform.Rotate(Vector3.down * 4);
    }
    public void RayCastHitting()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayLenght, Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f), Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, 2.63f), Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f), Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, -2.63f), Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(1.31f, 0, 0.81f), Color.blue);
        //Debug.DrawRay(transform.position, transform.TransformDirection(1.31f, 0, -0.81f), Color.blue);
        //Debug.DrawRay(transform.position, transform.TransformDirection(-1.31f, 0, 0.81f), Color.blue);
        //Debug.DrawRay(transform.position, transform.TransformDirection(-1.31f, 0, -0.81f), Color.blue);

        RaycastHit hitForward;
        Ray rayForward = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(rayForward, out hitForward, rayLenght))
        {
            isDriveForward = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.red);
            Debug.Log("HITTED ON FORWARD");
        }

        RaycastHit hitBack;
        Ray rayBack = new Ray(transform.position, transform.TransformDirection(Vector3.back));
        if (Physics.Raycast(rayBack, out hitBack, rayLenght))
        {
            isDriveForward = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayLenght, Color.red);
            Debug.Log("HITTED ON BACK");
        }

        RaycastHit hitLeft;
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f));
        if (Physics.Raycast(rayLeft, out hitLeft, rayLenght))
        {
            DriveLeft();
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f), Color.red);
            Debug.Log("HITTED ON LEFT");
        }

        RaycastHit hitRight;
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(1.07f, 0, 2.63f));
        if (Physics.Raycast(rayRight, out hitRight, rayLenght))
        {
            DriveRight();
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, 2.63f), Color.red);
            Debug.Log("HITTED ON RIGHT");
        }

        RaycastHit hitBackRight;
        Ray rayBackRight = new Ray(transform.position, transform.TransformDirection(1.07f, 0, -2.63f));
        if (Physics.Raycast(rayBackRight, out hitBackRight, rayLenght))
        {
            DriveLeft();
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, -2.63f), Color.red);
            Debug.Log("HITTED ON BACK RIGHT");
        }
        RaycastHit hitBackleft;
        Ray rayBackleft = new Ray(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f));
        if (Physics.Raycast(rayBackleft, out hitBackleft, rayLenght))
        {
            DriveRight();
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f), Color.red);
            Debug.Log("HITTED ON BACK LEFT");
        }
        RaycastHit hitFrontSide;
        Ray rayFrontSide = new Ray(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f));
        if (Physics.Raycast(rayFrontSide, out hitFrontSide, rayLenght * 2))
        {
            DriveLeft();
            Debug.DrawRay(transform.position, transform.TransformDirection(-2.07f, 0, -3.63f), Color.red);
            Debug.Log("HITTED FRONT SIDE");
        }


    }

}
