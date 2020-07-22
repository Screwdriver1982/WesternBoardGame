using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [SerializeField]float cameraSpeed = 100;
    [SerializeField] int cellView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
           

            if (cellView == 0)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 1)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 2)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 3)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }


            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (cellView == 0)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 1)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 2)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 3)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (cellView == 0)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 1)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 2)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 3)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (cellView == 0)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 1)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 2)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cellView == 3)
            {
                UnityEngine.Vector3 direction = new UnityEngine.Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }

    }

    public void FreeCameraMoveTo(Transform newTransform, int view)
    {
        UnityEngine.Vector3 newPosition = newTransform.position;
        transform.position = newPosition;
        UnityEngine.Quaternion newRotation = newTransform.rotation;
        transform.rotation = newRotation;
        cellView = view;
    }
}
