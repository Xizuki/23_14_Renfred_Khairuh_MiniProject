using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileSubScript : MonoBehaviour
{
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag != "Shape") { triggered = true; return; }
        else { triggered = false; return; }
    }
}
