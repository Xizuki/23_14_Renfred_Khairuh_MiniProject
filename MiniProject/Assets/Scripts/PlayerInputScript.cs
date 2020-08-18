using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    public GameObject selectedObject1;
    public GameObject selectedObject2;
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectObject();
    }

    void SelectObject()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) { return; }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Shape") { return; }

            if (!selectedObject1)
            {
                selectedObject1 = hit.collider.gameObject;
            }
            else
            {
                if(hit.collider.gameObject == selectedObject1) { selectedObject1 = null; return; }
                //if()

                selectedObject2 = hit.collider.gameObject;
                Swap();
            }
        }
    }

    void Swap()
    {
        //selectedObject1 = null;
        //selectedObject2 = null;
    }

    
}
