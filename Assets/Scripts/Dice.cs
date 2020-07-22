using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public int diceLastThrow;
    public Action onDiceStopped = delegate { };

    [SerializeField] Rigidbody rb;
    [SerializeField] Transform[] brinks;
    [SerializeField] Vector3 startTransform;
    [SerializeField] float baseMagnitude = 1f;
    [SerializeField] Quaternion startRotation;
    [SerializeField] float baseAngelMagnitude;
    [SerializeField] Vector3 startVelocity;
    [SerializeField] float stopFactor = 0.1f;
    [SerializeField] float showResultTime = 2f;


    int throwResult = 0;




    // Start is called before the first frame update

    public void ThrowDice()
    {
        rb.isKinematic = false;
        transform.position = startTransform;
        startVelocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-1f, 1f)).normalized * baseMagnitude;
        startRotation = Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
        transform.rotation = startRotation;
        rb.velocity = startVelocity;
        rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * baseAngelMagnitude;
    }

    private bool StopCheck()
    {
        if (rb.velocity.magnitude < stopFactor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int DiceStopBrink()
    {
        int mayBeResult = 0;
        for (int i = 1; i < brinks.Length; i++)
        {
            if (brinks[i].position.y > brinks[mayBeResult].position.y)
            {
                mayBeResult = i;
            }
        }
        return mayBeResult + 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (!rb.isKinematic)
        {
            if (StopCheck())
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                diceLastThrow = DiceStopBrink();
                StartCoroutine(DiceStopped(showResultTime));
            }
        }

    }

    IEnumerator DiceStopped(float showResultTime)
    {
        yield return new WaitForSeconds(showResultTime);
        onDiceStopped();
    }


}
