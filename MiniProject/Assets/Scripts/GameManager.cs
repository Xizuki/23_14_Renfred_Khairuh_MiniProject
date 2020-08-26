using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool areShapesFalling;
    public float CheckShapesNotFallingTime;
    public float CheckShapesNotFallingTimer;
    public ParticleSystem destroyedPart;

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


            if (Check[0] && Check[1]) { DestroyShapes(shape.gameObject.GetComponent<ShapeScript>(), shape.adjacentShapeColliders[0].shape.GetComponent<ShapeScript>(), shape.adjacentShapeColliders[1].shape.GetComponent<ShapeScript>()); Checkings = true; }
            if (Check[2] && Check[3]) { DestroyShapes(shape.gameObject.GetComponent<ShapeScript>(), shape.adjacentShapeColliders[2].shape.GetComponent<ShapeScript>(), shape.adjacentShapeColliders[3].shape.GetComponent<ShapeScript>()); Checkings = true; }

            for (int i =0; i< Check.Length; i++)
            {
                if (shape.adjacentShapeColliders[i].shape == null) { continue; }
                if (!Check[i]) { continue; }
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape == null) { continue; }
                if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.name == shape.name)
                {
                    DestroyShapes(shape, shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>(), shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>());
                    Checkings = true;

                    areShapesFalling = true;
                }
            }

        }
        return Checkings;
    }

    public void DestroyShapes(ShapeScript shape1, ShapeScript shape2, ShapeScript shape3)
    {
        ParticleSystem part1 = Instantiate(destroyedPart, shape1.transform.position, Quaternion.identity);
        ParticleSystem part2 = Instantiate(destroyedPart, shape2.transform.position, Quaternion.identity);
        ParticleSystem part3 = Instantiate(destroyedPart, shape3.transform.position, Quaternion.identity);

        print(shape1.GetComponentInChildren<MeshRenderer>().material.color);

        part1.startColor = shape1.GetComponentInChildren<MeshRenderer>().material.color;
        part2.startColor = part1.startColor;
        part3.startColor = part1.startColor;

        CheckSameShape(shape1, part1.startColor);
        CheckSameShape(shape2, part1.startColor);
        CheckSameShape(shape3, part1.startColor);

        Destroy(shape1.gameObject);
        Destroy(shape2.gameObject);
        Destroy(shape3.gameObject);
    }

    public void CheckSameShape(ShapeScript shape, Color color)
    {
        for (int i = 0; i < 4; i++)
        {
            if (shape.adjacentShapeColliders[i].shape == null) { continue; }
            if (shape.adjacentShapeColliders[i].shape.name != shape.gameObject.name) { continue; }
            if (shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().ChainReactionCheck) { continue; }

            shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().ChainReactionCheck = true;

            CheckSameShape(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>(),color);

            ParticleSystem part1 = Instantiate(destroyedPart, shape.transform.position, Quaternion.identity);
            part1.startColor = color;

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
