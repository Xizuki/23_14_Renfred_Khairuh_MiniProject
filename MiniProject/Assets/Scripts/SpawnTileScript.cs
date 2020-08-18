using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileScript : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] shapes;
    public float spawnTimer;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnLocationGO = GameObject.FindGameObjectsWithTag("SpawnLocation");
        for (int i=0; i < spawnLocationGO.Length; i++)
        {
            spawnLocations[i] = spawnLocationGO[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnShapes();
        spawnTimer += Time.deltaTime;
    }

    void SpawnShapes()
    {
        if(spawnTimer < spawnTime) { return; }

        spawnTimer = 0;
        // int locationElement = Random.Range(0, spawnLocations.Length);

        foreach (Transform t in spawnLocations)
        {
            int shapeElement = Random.Range(0, shapes.Length);
            Instantiate(shapes[shapeElement], t.position, Quaternion.identity);
            //Instantiate(shapes[shapeElement], spawnLocations[locationElement].position, Quaternion.identity);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag != "Shape") { return; }
        spawnTimer = 0;
    }
}
