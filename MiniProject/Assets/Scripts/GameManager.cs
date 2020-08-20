using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckMatch(ShapeScript[] shapes) //shapes is the 2 shapes you are swapping with each other
    {
        foreach(ShapeScript shape in shapes)
        {
            bool[] Check = new bool[4];         // Check for Adjacent Shapes   [0] = x+1, [1] = x-1, [2] = y+1, [3] = y-1
            for (int i = 0; i < shape.adjacentShapeColliders.Length; i++)
            {
                if (shape.adjacentShapeColliders[i].shape == null) { continue; }    
                if (shape.adjacentShapeColliders[i].shape.name != shape.name) { continue; }   // can change to check for ID instead but i not sure if needed rn

                Check[i] = true;                // Adjacent Shape [i] = same as "shape"
            }


            // if Both Side on the X axis is the Same shape
            if (Check[0] && Check[1]) { Destroy(shape.gameObject); Destroy(shape.adjacentShapeColliders[0].shape); Destroy(shape.adjacentShapeColliders[1].shape); return true; }
            // if Both Side on the Y axis is the Same shape
            if (Check[2] && Check[3]) { Destroy(shape.gameObject); Destroy(shape.adjacentShapeColliders[2].shape); Destroy(shape.adjacentShapeColliders[3].shape); return true; }



            for(int i =0; i< Check.Length; i++)
            {
                if (shape.adjacentShapeColliders[i].shape == null) { continue; }
                // Should Check if [i] Side is same shape
                if (!Check[i]) { continue; }
                // Check if Adjacent Shape [i]'s Adjacent Shape[i] = null
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape == null) { continue; }
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.name == shape.name)
                {
                    Destroy(shape.gameObject);
                    Destroy(shape.adjacentShapeColliders[i].shape);
                    Destroy(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape);
                    return true;
                }
            }
      
        }
        return false;
    }
}
