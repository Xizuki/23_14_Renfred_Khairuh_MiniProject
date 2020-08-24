using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool areShapesFalling;
    public float CheckShapesNotFallingTime;
    public float CheckShapesNotFallingTimer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckShapesVelocity();
        CheckShapesNotFallingTimer += Time.deltaTime;
    }

    public bool CheckMatch(ShapeScript[] shapes) // Can Tidy up but dont need to rush
    {
        bool Checkings = false;

        foreach (ShapeScript shape in shapes)
        {
            bool[] Check = new bool[4];
            //print("shape name = " + shape.name);
            for (int i = 0; i < shape.adjacentShapeColliders.Length; i++)
            {
                //print("sshape.adjacentShapeColliders[i].shape.name = " + shape.adjacentShapeColliders[i].shape.name);
                if (shape.adjacentShapeColliders[i].shape == null) { continue; }
                if (shape.adjacentShapeColliders[i].shape.name != shape.name) { continue; }   // can change to check for ID instead but i not sure if needed rn

                Check[i] = true;

            }


            if (Check[0] && Check[1]) { Destroy(shape.gameObject); Destroy(shape.adjacentShapeColliders[0].shape); Destroy(shape.adjacentShapeColliders[1].shape); Checkings = true; }
            if (Check[2] && Check[3]) { Destroy(shape.gameObject); Destroy(shape.adjacentShapeColliders[2].shape); Destroy(shape.adjacentShapeColliders[3].shape); Checkings = true; }

            for(int i =0; i< Check.Length; i++)
            {
                if (shape.adjacentShapeColliders[i].shape == null) { break; }
                if (!Check[i]) { continue; }
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape == null) { break; }
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.name == shape.name)
                {
                    CheckSameShape(shape.gameObject.GetComponent<ShapeScript>());
                    CheckSameShape(shape.adjacentShapeColliders[i].shape.gameObject.GetComponent<ShapeScript>());
                    CheckSameShape(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.gameObject.GetComponent<ShapeScript>());

                    Destroy(shape.gameObject);
                    Destroy(shape.adjacentShapeColliders[i].shape);
                    Destroy(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape);
                    Checkings = true;

                    areShapesFalling = true;
                }
            }

        }
        return Checkings;
    }

    public void CheckSameShape(ShapeScript shape)
    {
        for (int i = 0; i < 4; i++)
        {
            if (shape.adjacentShapeColliders[i].shape == null) { continue; }
            if (shape.adjacentShapeColliders[i].shape.name != shape.gameObject.name) { continue; }
            if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().ChainReactionCheck) { continue; }

            shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().ChainReactionCheck = true;
            CheckSameShape(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>());
            Destroy(shape.adjacentShapeColliders[i].shape);

        } 
    }

    public void CheckShapesVelocity()
    {
        if(CheckShapesNotFallingTimer < CheckShapesNotFallingTime) { return; }
        CheckShapesNotFallingTimer = 0;
        if (!areShapesFalling) { return; }

        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject shape in shapes)
        {
            if(Mathf.RoundToInt(shape.GetComponent<Rigidbody>().velocity.y) !=0) { return; }
        }
        areShapesFalling = false;
    }
}
