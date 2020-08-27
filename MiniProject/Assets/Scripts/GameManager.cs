using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool JustStarted;
    public float justStartTime;
    private float justStartTimer;

    public static GameManager instance;
    public AudioSource audioSource;

    public int score;
    public int scoreRequirement;
    //public int moves;

    public Text scoreText;
    public bool areShapesFalling;
    public float CheckShapesNotFallingTime;
    public float CheckShapesNotFallingTimer;
    public ParticleSystem destroyedPart;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        CheckShapesVelocity();
        CheckShapesNotFallingTimer += Time.deltaTime;
        if (JustStarted) { if (justStartTimer >= justStartTime) { JustStarted = false; } }
        justStartTimer += Time.deltaTime;

        
    }

    public bool CheckMatch(ShapeScript[] shapes, bool canCheck) // Can Tidy up but dont need to rush
    {
        if (!canCheck)
            return false;

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

            for (int i = 0; i < Check.Length; i++)
            {
<<<<<<< HEAD

=======
>>>>>>> branch_Renfred_v3
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

    public void GainScore(int plusScore)
    {
        audioSource.Play();
        score += plusScore;
        scoreText.text = "Score : " + score;

        if (score >= scoreRequirement) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); }
    }
    public void DestroyShapes(ShapeScript shape1, ShapeScript shape2, ShapeScript shape3)
    {
        ParticleSystem part1 = Instantiate(destroyedPart, shape1.transform.position, Quaternion.identity);
        ParticleSystem part2 = Instantiate(destroyedPart, shape2.transform.position, Quaternion.identity);
        ParticleSystem part3 = Instantiate(destroyedPart, shape3.transform.position, Quaternion.identity);

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

            CheckSameShape(shape.adjacentShapeColliders[i].shape.GetComponent<ShapeScript>(), color);

            ParticleSystem part1 = Instantiate(destroyedPart, shape.transform.position, Quaternion.identity);
            part1.startColor = color;
            Destroy(shape.adjacentShapeColliders[i].shape);
            GainScore(1);
        }
    }

    public void CheckShapesVelocity()
    {
        if (CheckShapesNotFallingTimer < CheckShapesNotFallingTime) { return; }
        CheckShapesNotFallingTimer = 0;
        if (!areShapesFalling) { return; }

        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject shape in shapes)
        {
            if (Mathf.RoundToInt(shape.GetComponent<Rigidbody>().velocity.y) != 0) { return; }
        }
        areShapesFalling = false;
        CheckMoves();
    }

    public void CheckMoves()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject shape in shapes)
        {
            for (int i = 0; i < 4; i++)
            {
                bool[] Check = new bool[2];
                try
                {
                    if (shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>() == null) { }
                }
                catch { }
                try
                {
                    if (shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>() == null) { }
                }
                catch { }


                ShapeScript shape1SpacesAway = shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>();
                ShapeScript shape2SpacesAway = shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>().adjacentShapeColliders[i].shape.GetComponent<ShapeScript>();

                if (shape1SpacesAway.name == name) { Check[0] = true; }
                if (shape2SpacesAway.name == name) { Check[1] = true; }

                if (Check[0] && Check[1]) { return; }

                if (Check[0])
                {
                    for (int ii = 0; ii < 2; ii++)
                    {
                        if (i == 0 || i == 1)
                        {
                            if (shape2SpacesAway.adjacentShapeColliders[2].shape.name == shape.name
                            || shape2SpacesAway.adjacentShapeColliders[3].shape.name == shape.name)
                            {
                                return;
                            }
                        }
                        else if (i == 2 || i == 3)
                        {
                            if (shape2SpacesAway.adjacentShapeColliders[0].shape.name == shape.name
                            || shape2SpacesAway.adjacentShapeColliders[1].shape.name == shape.name)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        SceneManager.LoadScene("LoseScene");
    }
}
