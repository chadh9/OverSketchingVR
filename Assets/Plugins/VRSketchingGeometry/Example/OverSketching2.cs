using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.Commands.Ribbon;
using VRSketchingGeometry.Commands.Patch;
using VRSketchingGeometry.Commands.Group;
using VRSketchingGeometry.Commands.Selection;
using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry.Export;

public class OverSketching2 : MonoBehaviour
{
    private CommandInvoker Invoker;
    public DefaultReferences Defaults;

    public SketchWorld SketchWorld;
    public SketchWorld DeserializedSketchWorld;
    public LineSketchObject LineSketchObject;
    public RibbonSketchObject RibbonSketchObject;
    public PatchSketchObject PatchSketchObject;
    public SketchObjectGroup SketchObjectGroup;
    public SketchObjectSelection SketchObjectSelection;
    private string SavePath;

    LineSketchObject test0;
    LineSketchObject test2;
    // Start is called before the first frame update
    void Start()
    {
        //Create a SketchWorld, many commands require a SketchWorld to be present
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();



        test0 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();

        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -2, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 0, 1)));


        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 2, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 4, 1)));

        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 5, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 6, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 7, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 8, 1)));





        test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();

        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 1, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 3, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 4, 2)));



        calculateScaledCurve2(test2,test0);


    }



    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            print(test0.getNumberOfControlPoints());

            Invoker = new CommandInvoker();
            //Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 60, 60)));
            
            test0.SetControlPoints(test2.GetControlPoints());
        }
    }
    List<Vector3> calculateScaledCurve(LineSketchObject oCurve, LineSketchObject dCurve) {
        
        test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

        Invoker = new CommandInvoker();

        List<Vector3> scaledPoints = dCurve.GetControlPoints();

        foreach (var x in oCurve.GetControlPoints())
        {
            for (int i = 0; i < dCurve.GetControlPoints().Count; i++) {
                Vector3 scaledPoint1= (dCurve.GetControlPoints()[i] + (x - dCurve.GetControlPoints()[i]) * 0.5f);
                scaledPoints[i] = (dCurve.GetControlPoints()[i] + (x - dCurve.GetControlPoints()[i]) * (1/Mathf.Pow(2 , (x - dCurve.GetControlPoints()[i]).sqrMagnitude)));
                print("test "+ ((x - dCurve.GetControlPoints()[i]).sqrMagnitude));



            }


        }
        foreach (var x in scaledPoints)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(test2, x));
        }
        return new List<Vector3>();            
    }
    List<Vector3> calculateScaledCurve2(LineSketchObject oCurve, LineSketchObject dCurve)
    {

        test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

        Invoker = new CommandInvoker();

        List<Vector3> scaledPoints = dCurve.GetControlPoints();
        
        var list = new List<Vector3>();

        for (int i=0; i < dCurve.getNumberOfControlPoints(); i++) 
        {
            list.Add(new Vector3());

            for (int j = 0; j < oCurve.GetControlPoints().Count; j++)
            {
                //Vector3 scaledPoint1 = (dCurve.GetControlPoints()[i] + (x - dCurve.GetControlPoints()[i]) * 0.5f);
                list[i] += ((oCurve.GetControlPoints()[j] - dCurve.GetControlPoints()[i]) * (1 / Mathf.Pow(1.5f, (oCurve.GetControlPoints()[j] - dCurve.GetControlPoints()[i]).sqrMagnitude)));


            }
            print(i + " i " + list[i]);
            print("count"+list.Count);
        }




        for (int i=0; i<dCurve.getNumberOfControlPoints() ; i++)
        {
            list[i] = dCurve.GetControlPoints()[i] + (list[i] / oCurve.getNumberOfControlPoints());
            Invoker.ExecuteCommand(new AddControlPointCommand(test2, list[i]));
        }
        return new List<Vector3>();
    }
}
