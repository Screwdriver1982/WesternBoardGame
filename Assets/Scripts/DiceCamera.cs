using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCamera : MonoBehaviour
{
    [SerializeField] Transform diceTransform;
    [SerializeField] float additionalAltitude = 3f;
    void Start()
    {
        Spot();
    }

    // Update is called once per frame
    void Update()
    {
        Spot();
    }

    void Spot()
    {
        transform.position = new Vector3(diceTransform.position.x, additionalAltitude, diceTransform.position.z);

    }
}
