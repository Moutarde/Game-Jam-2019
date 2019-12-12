using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SuspectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnKilled()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public bool IsTarget()
    {
        //TODO
        return false;
    }
}
