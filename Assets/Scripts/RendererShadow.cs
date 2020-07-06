using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererShadow : MonoBehaviour
{
    [SerializeField] SpriteRenderer board;
    // Start is called before the first frame update
    void Start()
    {
        board.receiveShadows = true;
    }

    
}
