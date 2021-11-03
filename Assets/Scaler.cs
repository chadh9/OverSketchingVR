using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;

    // Start is called before the first frame update
    void Start()
    {
        
        float currentSize = gameObject.GetComponent<Renderer>().bounds.size.z; // I'm not sure if this compiles (I guess you need to specify the component type)
        print(currentSize);

        Vector3 scale = gameObject.transform.localScale;
        print(scale);
        scale.x = (gameObject1.transform.position - gameObject2.transform.position).magnitude * scale.x / currentSize;
        scale.y = (gameObject1.transform.position - gameObject2.transform.position).magnitude * scale.y / currentSize;
        scale.z = (gameObject1.transform.position - gameObject2.transform.position).magnitude * scale.z / currentSize;
        print((gameObject1.transform.position - gameObject2.transform.position).sqrMagnitude);

        gameObject.transform.localScale = scale;
        
    }

    // Update is called once per frame
    void Update()
    {


        float currentSize = gameObject.GetComponent<Renderer>().bounds.size.z; // I'm not sure if this compiles (I guess you need to specify the component type)
        print(currentSize);

        gameObject.transform.position = new Vector3(200, 0, 0);
        Vector3 scale = gameObject.transform.localScale;
        print(scale);
        scale.x = (2) * scale.x / currentSize;
        scale.y = (2) * scale.y / currentSize;
        scale.z = 2 * scale.z / currentSize;
        print((gameObject1.transform.position - gameObject2.transform.position).sqrMagnitude);

        gameObject.transform.localScale = scale;
    }
}
