using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] bool showRayCasts;
    [SerializeField] bool isStop;
    [SerializeField] bool isDriveForward;
    [SerializeField] bool canMakeNewEntity;
    [SerializeField] float rayLenght;
    [SerializeField] float newEntityTimer;
    [SerializeField] float waitTimer;
    [SerializeField] float whenWaitTimer;
    [SerializeField] Vector3 dir;
    [SerializeField] GameObject selection;
    [SerializeField] int entityCharacter;
    [SerializeField] int newEntityCharacter;
    [SerializeField] GameObject newEntityPrefab0;
    [SerializeField] GameObject newEntityPrefab1;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject scriptsGameObj;
    [SerializeField] SimulationControl m_simControl;
    [SerializeField] bool isWaiting;
    [SerializeField] bool isRotating;
    [SerializeField] float rotatingTimer;
    [SerializeField] float whenRotatingTimer;
    [SerializeField] int wichSideRotating;

    private void Start()
    {
        rayLenght = 2.5f;
        isDriveForward = true;
        newEntityTimer = 12.0f;
        newEntityPrefab0 = Resources.Load("newEntity0") as GameObject;
        newEntityPrefab1 = Resources.Load("newEntity1") as GameObject;
        lifeTime = Random.Range(1200, 1400);
        scriptsGameObj = GameObject.FindGameObjectWithTag("Control");
        m_simControl = scriptsGameObj.GetComponent<SimulationControl>();
        m_simControl.birthCounter++;
        m_simControl.actualEntityCounter++;
        waitTimer = Random.Range(1, 6);
        whenWaitTimer = Random.Range(4, 16);
        rotatingTimer = Random.Range(0.5f, 1);
        whenRotatingTimer = Random.Range(8, 16);
        if(entityCharacter == 0)
        {
            m_simControl.pinkCounter++;
            m_simControl.actualPinkCounter++;
        }
        if (entityCharacter == 1)
        {
            m_simControl.blueCounter++;
            m_simControl.actualBlueCounter++;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            lifeTime += Random.Range(3, 6);
            Destroy(other.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (isRotating == true)
        {   
            rotatingTimer -= Time.deltaTime;
            if(wichSideRotating == 0)
            {
                DriveLeft();
            }
            if(wichSideRotating == 1)
            {
                DriveRight();
            }
            if (rotatingTimer < 0)
            {
                whenRotatingTimer = Random.Range(8, 16);
                isRotating = false;
            }
        }
        else
        {
            whenRotatingTimer -= Time.deltaTime;
            if (whenRotatingTimer < 0)
            {
                wichSideRotating = Random.Range(0, 2);
                rotatingTimer = Random.Range(0.5f, 1);
                isRotating = true;
            }
        }
        if (isWaiting == true)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0)
            {
                whenWaitTimer = Random.Range(4, 16);
                isWaiting = false;
            }
        }
        else
        {
            whenWaitTimer -= Time.deltaTime;
            if (whenWaitTimer < 0)
            {
                waitTimer = Random.Range(1, 6);
                isWaiting = true;
            }
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            m_simControl.destroyCounter++;
            m_simControl.actualEntityCounter--;
            if(entityCharacter == 0)
            {
                m_simControl.actualBlueCounter--;
            }
            if(entityCharacter == 1)
            {
                m_simControl.actualPinkCounter--;
            }
            Destroy(this.gameObject);
        }
        newEntityPrefab0 = Resources.Load("newEntity0") as GameObject;
        newEntityPrefab1 = Resources.Load("newEntity1") as GameObject;
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
        if (newEntityTimer <= 0)
        {
            canMakeNewEntity = true;
        }
        else
        {
            canMakeNewEntity = false;
            newEntityTimer -= Time.deltaTime;
        }
    }
    public void MakeNewentity()
    {
        if (canMakeNewEntity == true)
        {
            newEntityCharacter = Random.Range(0, 2);
            if (newEntityCharacter == 1)
            {
                Instantiate(newEntityPrefab1, transform.position, transform.rotation);
            }
            if (newEntityCharacter == 0)
            {
                Instantiate(newEntityPrefab0, transform.position, transform.rotation);
            }
            newEntityTimer = 10.0f;
        }
    }
    public void DriveForward()
    {
        if (isWaiting == false)
        {
            this.transform.Translate(Vector3.forward / 20);
        }
    }
    public void DriveBack()
    {
        if (isWaiting == false)
            this.transform.Translate(-Vector3.forward / 20);
    }
    public void DriveLeft()
    {
        if (isWaiting == false)
            this.transform.Rotate(Vector3.up * 4);
    }
    public void DriveRight()
    {
        if (isWaiting == false)
            this.transform.Rotate(Vector3.down * 4);
    }
    public void RayCastHitting()
    {
        if (showRayCasts == true)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayLenght, Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f), Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, 2.63f), Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f), Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, -2.63f), Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayLenght, Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * rayLenght, Color.blue);
            Debug.DrawRay(transform.position, transform.TransformDirection(0.0f, 0.98f, 1.04f), Color.blue);
        }
        RaycastHit hitForward;
        Ray rayForward = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(rayForward, out hitForward, rayLenght))
        {
            isDriveForward = false;
            selection = hitForward.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.red);
        }

        RaycastHit hitBack;
        Ray rayBack = new Ray(transform.position, transform.TransformDirection(Vector3.back));
        if (Physics.Raycast(rayBack, out hitBack, rayLenght))
        {
            isDriveForward = true;
            selection = hitBack.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayLenght, Color.red);
        }

        RaycastHit hitLeft;
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f));
        if (Physics.Raycast(rayLeft, out hitLeft, rayLenght))
        {
            DriveLeft();
            selection = hitLeft.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, 2.63f), Color.red);
        }

        RaycastHit hitRight;
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(1.07f, 0, 2.63f));
        if (Physics.Raycast(rayRight, out hitRight, rayLenght))
        {
            DriveRight();
            selection = hitRight.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, 2.63f), Color.red);
        }

        RaycastHit hitBackRight;
        Ray rayBackRight = new Ray(transform.position, transform.TransformDirection(1.07f, 0, -2.63f));
        if (Physics.Raycast(rayBackRight, out hitBackRight, rayLenght))
        {
            DriveLeft();
            selection = hitBackRight.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(1.07f, 0, -2.63f), Color.red);
        }
        RaycastHit hitBackleft;
        Ray rayBackleft = new Ray(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f));
        if (Physics.Raycast(rayBackleft, out hitBackleft, rayLenght))
        {
            DriveRight();
            selection = hitBackleft.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.07f, 0, -2.63f), Color.red);
        }
        RaycastHit hitFrontSide;
        Ray rayFrontSide = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        if (Physics.Raycast(rayFrontSide, out hitFrontSide, rayLenght))
        {
            DriveLeft();
            selection = hitFrontSide.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.red);
        }
        RaycastHit hitSecondFrontSide;
        Ray raySecondFrontSide = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        if (Physics.Raycast(raySecondFrontSide, out hitSecondFrontSide, rayLenght))
        {
            DriveRight();
            selection = hitSecondFrontSide.collider.gameObject;
            if (selection.gameObject.tag == "Entity0" && entityCharacter == 1)
            {
                MakeNewentity();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left), Color.red);
        }

    }

}
