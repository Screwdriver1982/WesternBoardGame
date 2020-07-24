using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeCamera : MonoBehaviour
{
    public Camera cameraMain;
    [SerializeField]float cameraSpeed = 100;
    [SerializeField] int cameraView;
    [SerializeField] float rotateTime = 0.1f;
    bool allowRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
           

            if (cameraView == 0)
            {
                Vector3 direction = new Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 1)
            {
                Vector3 direction = new Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 2)
            {
                Vector3 direction = new Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 3)
            {
                Vector3 direction = new Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }


            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (cameraView == 0)
            {
                Vector3 direction = new Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 1)
            {
                Vector3 direction = new Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 2)
            {
                Vector3 direction = new Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 3)
            {
                Vector3 direction = new Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (cameraView == 0)
            {
                Vector3 direction = new Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 1)
            {
                Vector3 direction = new Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 2)
            {
                Vector3 direction = new Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 3)
            {
                Vector3 direction = new Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (cameraView == 0)
            {
                Vector3 direction = new Vector3(-1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 1)
            {
                Vector3 direction = new Vector3(0, 0, -1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 2)
            {
                Vector3 direction = new Vector3(1, 0, 0);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
            else if (cameraView == 3)
            {
                Vector3 direction = new Vector3(0, 0, 1);
                transform.position += cameraSpeed * Time.deltaTime * direction;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) && allowRotate)
        {
            RotateCamera();
        }



    }

    public void FreeCameraMoveTo(Transform newTransform, int view)
    {
        Vector3 newPosition = new Vector3(newTransform.position.x, 18f, newTransform.position.z);
        transform.position = newPosition;
        Quaternion newRotation = newTransform.rotation;
        transform.rotation = newRotation;
        cameraView = view;
    }

    void RotateCamera()
    {
        Vector3 currentEulerAngles = transform.eulerAngles;
        Vector3 newEulerAngles = currentEulerAngles + new Vector3(0, -90f, 0);
        //transform.eulerAngles = currentEulerAngles;
        allowRotate = false;
        cameraView = (cameraView + 1) % 4;
        transform.DORotate(newEulerAngles, rotateTime).SetEase(Ease.InOutSine).OnComplete(AllowRotate);

    }

    void AllowRotate()
    {
        allowRotate = true;
    }

}
