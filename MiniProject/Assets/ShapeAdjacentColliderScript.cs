using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAdjacentColliderScript : MonoBehaviour
{
    public GameObject shape;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "Shape") { return; }
        shape = col.gameObject;
    }

    void OnTriggerExit()
    {
        shape = null;
    }
}
