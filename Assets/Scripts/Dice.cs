using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform[] brinks;
    [SerializeField] Vector3 startTransform;
    [SerializeField] float baseMagnitude = 1f;
    [SerializeField] Quaternion startRotation;
    [SerializeField] float baseAngelMagnitude;
    [SerializeField] Vector3 startVelocity;
    [SerializeField] float stopFactor = 0.1f;


    int throwResult = 0;



    // Start is called before the first frame update
    void Start()
    {
        ThrowDice();
    }

    private void ThrowDice()
    {
        rb.isKinematic = false;
        transform.position = startTransform;
        startVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)).normalized * baseMagnitude;
        startRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        transform.rotation = startRotation;
        rb.velocity = startVelocity;
        rb.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * baseAngelMagnitude;
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ThrowDice();
        }

        if (!rb.isKinematic)
        {
            if (StopCheck())
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                print(DiceStopBrink());
            }
        }

    }
}
