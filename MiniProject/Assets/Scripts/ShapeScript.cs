using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    public ShapeAdjacentColliderScript[] adjacentShapeColliders; // [0] = x+1, [1] = x-1, [2] = y+1, [3] = y-1
    public GameObject selectedEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Swap(GameObject other)
    {
        bool check = false;
        
        foreach(ShapeAdjacentColliderScript col in adjacentShapeColliders)
        {
            if(col.shape == other) { check = true; }
        }

        if(check)
        {

        }
    }

    public void CheckMatch()
    {
        
    }

}
