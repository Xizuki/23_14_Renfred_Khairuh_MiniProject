using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    public GameObject selectedObject1;
    public GameObject selectedObject2;

    public Vector3 swappingObj1Pos;
    public Vector3 swappingObj2Pos;

    Vector3 Obj1ToObj2;
    Vector3 Obj2ToObj1;
    public float swappingSpeed;
    public bool isSwapping;
    public float swappingTimer;
    public float swappingDuration;

    [Header("References")]
    public GameObject prefabSwappingCollider;
    public GameObject runtimeSwappingCollider;

    [SerializeField]
    private bool triggerOnce = false;
    [SerializeField]private bool checkOnce = false;

    //public GameObject swappingCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectObject();
        Swapping();
    }

    void SelectObject()
    {
        if (isSwapping || GameManager.instance.areShapesFalling) { return; } // If in Swapping - Dont Run the Code
        if (!Input.GetKeyDown(KeyCode.Mouse0)) { return; } // If not Left Clicking - Dont Run the Code

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Shape") { return; }

            if (!selectedObject1)
            {
                selectedObject1 = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<ShapeScript>().selectedEffect.SetActive(true);
            }
            else
            {
                if(hit.collider.gameObject == selectedObject1)
                {
                    selectedObject1 = null;
                    hit.collider.gameObject.GetComponent<ShapeScript>().selectedEffect.SetActive(false);
                    return;
                }

                bool check = false;
                foreach(ShapeAdjacentColliderScript adjacent in selectedObject1.GetComponent<ShapeScript>().adjacentShapeColliders)
                {
                    if (hit.collider.gameObject == adjacent.shape) { check = true; }
                }

                if (!check) { return; }

                selectedObject2 = hit.collider.gameObject;
                selectedObject2.GetComponent<ShapeScript>().selectedEffect.SetActive(true);
                Swap();
            }
        }
    }

    void Swap()
    {

        selectedObject1.layer = LayerMask.NameToLayer("Swapping");
        selectedObject2.layer = LayerMask.NameToLayer("Swapping");

        selectedObject1.GetComponent<Rigidbody>().useGravity = false;
        selectedObject2.GetComponent<Rigidbody>().useGravity = false;
        //selectedObject1.GetComponent<BoxCollider>().enabled = false;
        //selectedObject2.GetComponent<BoxCollider>().enabled = false;

        

        swappingObj1Pos = selectedObject1.transform.position;
        swappingObj2Pos = selectedObject2.transform.position;

        swappingSpeed = (((swappingObj1Pos - swappingObj2Pos).magnitude)/1.5f) / swappingDuration;
        Obj1ToObj2 = selectedObject2.transform.position - swappingObj1Pos;
        Obj2ToObj1 = selectedObject1.transform.position - swappingObj2Pos;

        if (Mathf.Round(Obj1ToObj2.x) != 0)
        {
            runtimeSwappingCollider = Instantiate(prefabSwappingCollider, selectedObject1.transform.position + (Obj1ToObj2 / 2), Quaternion.identity);
            runtimeSwappingCollider.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Mathf.Round(Obj1ToObj2.y) != 0)  
        {
            runtimeSwappingCollider = Instantiate(prefabSwappingCollider, selectedObject1.transform.position + (Obj1ToObj2 / 2), Quaternion.identity);
            runtimeSwappingCollider.transform.eulerAngles = new Vector3(0, 0, 90);
        }

        triggerOnce = false;
        isSwapping = true;
        
    }

    void Swapping()
    {

        if (triggerOnce)
        {
            checkOnce = false;
        }
        else
            checkOnce = true;



        if (!isSwapping) { return; }

        selectedObject1.transform.position += Obj1ToObj2 * swappingSpeed * Time.deltaTime;
        selectedObject2.transform.position += Obj2ToObj1 * swappingSpeed * Time.deltaTime;

        swappingTimer += Time.deltaTime;

        if(swappingTimer < swappingDuration) { return; }


        print(GameManager.instance.CheckMatch(new ShapeScript[] { selectedObject1.GetComponent<ShapeScript>(), selectedObject2.GetComponent<ShapeScript>() }, checkOnce));

        if (GameManager.instance.CheckMatch(new ShapeScript[] { selectedObject1.GetComponent<ShapeScript>(), selectedObject2.GetComponent<ShapeScript>() }, checkOnce) == true)
        {
            Destroy(runtimeSwappingCollider);
            isSwapping = false;
            swappingTimer = 0;

            selectedObject1.layer = LayerMask.NameToLayer("Default");
            selectedObject2.layer = LayerMask.NameToLayer("Default");
            selectedObject1.GetComponent<Rigidbody>().useGravity = true;
            selectedObject2.GetComponent<Rigidbody>().useGravity = true;
            //selectedObject1.GetComponent<BoxCollider>().enabled = true;
            //selectedObject2.GetComponent<BoxCollider>().enabled = true;
            selectedObject1.GetComponent<ShapeScript>().selectedEffect.SetActive(false);
            selectedObject2.GetComponent<ShapeScript>().selectedEffect.SetActive(false);

            selectedObject1 = null;
            selectedObject2 = null;

        }
        else 
        {
            if (!triggerOnce)
            {
                swappingSpeed = -swappingSpeed;
                triggerOnce = true;
            }

            if (swappingTimer < (swappingDuration * 2)) { return; }
            Destroy(runtimeSwappingCollider);

            isSwapping = false;
            swappingTimer = 0;

            selectedObject1.layer = LayerMask.NameToLayer("Default");
            selectedObject2.layer = LayerMask.NameToLayer("Default");
            selectedObject1.GetComponent<Rigidbody>().useGravity = true;
            selectedObject2.GetComponent<Rigidbody>().useGravity = true;
            //selectedObject1.GetComponent<BoxCollider>().enabled = true;
            //selectedObject2.GetComponent<BoxCollider>().enabled = true;
            selectedObject1.GetComponent<ShapeScript>().selectedEffect.SetActive(false);
            selectedObject2.GetComponent<ShapeScript>().selectedEffect.SetActive(false);

            selectedObject1 = null;
            selectedObject2 = null;

            

        }

        
    }
}
