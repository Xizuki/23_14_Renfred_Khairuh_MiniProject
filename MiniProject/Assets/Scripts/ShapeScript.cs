using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    public GameObject[] adjacentShapes;
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
        foreach(GameObject shape in adjacentShapes)
        {
            if(shape == other) { check = true; }
        }

        if(check)
        {

        }
    }

    public void GetAdjacent()
    {
        
    }

    void OnCollisionStay(Collision col)
    //void OnCollisionEnter(Collision col)
    {
        bool sameObject = false;
        foreach (GameObject shape in adjacentShapes)
        {
            if(col.gameObject == shape) { sameObject = true; break; }
        }

        if (!sameObject)
        {
            for(int i =0; i<adjacentShapes.Length;i++)
            {
                if(adjacentShapes[i] != null) { break; }
                adjacentShapes[i] = col.gameObject;
            }
        }
    }
}
