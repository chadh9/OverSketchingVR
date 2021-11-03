using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SketchRunner;
    PenFunctionality penFunctionality;
    Text weightText;

    void Start()
    {
        penFunctionality = SketchRunner.GetComponent<PenFunctionality>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = penFunctionality.vPosition;
        gameObject.transform.rotation = penFunctionality.qRotation;
        
    }
}
