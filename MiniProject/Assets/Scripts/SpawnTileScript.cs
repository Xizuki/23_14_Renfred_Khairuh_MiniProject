using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileScript : MonoBehaviour
{
    public GameObject[] shapes;
    public float spawnTimer;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {

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

            int shapeElement = Random.Range(0, shapes.Length);
            Instantiate(shapes[shapeElement], transform.position, Quaternion.identity);
            //Instantiate(shapes[shapeElement], spawnLocations[locationElement].position, Quaternion.identity);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "Shape") { return; }
        spawnTimer = 0;
    }
}
