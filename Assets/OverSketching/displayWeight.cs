using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayWeight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SketchRunner;
    PenFunctionality penFunctionality;


    void Start()
    {
        penFunctionality = SketchRunner.GetComponent<PenFunctionality>();
    }

    // Update is called once per frame
    void Update()
    {
       // gameObject.GetComponent<Text>().text = String.Format("{0:0.000}", (penFunctionality.maxWeight - penFunctionality.weight)/100 ); ;
        gameObject.GetComponent<Text>().text = String.Format("{0:0.00}", -(  penFunctionality.weight-(penFunctionality.maxWeight+penFunctionality.minWeight))/penFunctionality.maxWeight) ;
    }
}
