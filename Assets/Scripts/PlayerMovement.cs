using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float oneCellJumpTime;
    [SerializeField] float jumpPower =3f;
    [SerializeField] float rotateTime =1f;
    [SerializeField] float cellSize = 3f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            
            Vector3 rotatedVector = (transform.rotation * Quaternion.AngleAxis(-90f, Vector3.up)).eulerAngles;

            transform.DORotate(rotatedVector, rotateTime).SetEase(Ease.InOutSine).OnComplete(JumpForward);

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            Vector3 rotatedVector = (transform.rotation * Quaternion.AngleAxis(90f, Vector3.up)).eulerAngles;

            transform.DORotate(rotatedVector, rotateTime).SetEase(Ease.InOutSine).OnComplete(JumpForward); ;
            
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 newPosition = transform.position - transform.right * cellSize;
            transform.DOJump(newPosition, jumpPower, 1, oneCellJumpTime);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            JumpForward();
        }
    }

    private void JumpForward()
    {
        Vector3 newPosition = transform.position + transform.right * cellSize;
        transform.DOJump(newPosition, jumpPower, 1, oneCellJumpTime);
    }
}
