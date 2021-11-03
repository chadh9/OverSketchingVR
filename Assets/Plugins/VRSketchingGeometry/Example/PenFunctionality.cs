using System.Linq;
using System;
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
using UnityEngine.XR;

using Valve.VR;

public class PenFunctionality : MonoBehaviour
{

    public SteamVR_Action_Boolean drawCurve;
    public SteamVR_Action_Boolean drawOverSketchingCurve;
    public SteamVR_Action_Boolean undo;
    public SteamVR_Action_Boolean redo;
    public SteamVR_Action_Vector2 weightSlider;
    public SteamVR_Action_Vector2 rangeSlider;

    public OverSketching3 overSketch;

    private CommandInvoker Invoker;
    private CommandInvoker InvokerOCurve;
    public DefaultReferences Defaults;

    public SketchWorld SketchWorld;
    public SketchWorld DeserializedSketchWorld;
    public LineSketchObject LineSketchObject;
    public RibbonSketchObject RibbonSketchObject;
    public PatchSketchObject PatchSketchObject;
    public SketchObjectGroup SketchObjectGroup;
    public SketchObjectSelection SketchObjectSelection;


    public Vector3 vPosition = new Vector3();
    public Quaternion qRotation = new Quaternion();

    public GameObject PenPoint;
    public GameObject rangeIndicator;

    private string SavePath;

    public GameObject RangeIndicator;
    LineSketchObject dCurve;
    LineSketchObject oCurve;
    GameObject CurveParent;

    [Range(0f, 1000f)]
    public float weight;

    [Range(0f, 1000f)]
    public float range;


    void Start()
    {
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();

        SketchObjectGroup = Instantiate(Defaults.SketchObjectGroupPrefab).GetComponent<SketchObjectGroup>();



        Invoker = new CommandInvoker();
        InvokerOCurve = new CommandInvoker();

        dCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        oCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();


        // Invoker.ExecuteCommand(new AddToGroupCommand(SketchObjectGroup, dCurve));
        // Invoker.ExecuteCommand(new AddToSelectionAndHighlightCommand(SketchObjectSelection, SketchObjectGroup));


        dCurve.minimumControlPointDistance = 0.005f;
        dCurve.SetLineDiameter(0.01f);
        dCurve.SetInterpolationSteps(10);

        oCurve.minimumControlPointDistance = 0.005f;
        oCurve.SetInterpolationSteps(100);
        oCurve.SetLineDiameter(0.01f);

        oCurve.GetComponentInChildren<Renderer>().material.color = new Color(0, 0.1f, 1f, 0.7f);

       

        overSketch = GetComponent<OverSketching3>();

        weight = 1000;
        range = 0.1f;

        /*
        LineSketchObject lo = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        lo.name = "lo";
        lo.minimumControlPointDistance = 0.005f;
        lo.SetLineDiameter(0.01f);
        lo.SetInterpolationSteps(10);
        
        Invoker.ExecuteCommand(new AddControlPointCommand(lo, new Vector3(200, 0, -1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(lo, new Vector3(200, 0, 1)));
        */


       // Invoker.ExecuteCommand(new AddControlPointContinuousCommand(dCurve, new Vector3(0,1,0)));


    }

    void createOCurve() {
        oCurve.minimumControlPointDistance = 0.005f;
        oCurve.SetInterpolationSteps(10);
        oCurve.SetLineDiameter(0.01f);
        oCurve.GetComponentInChildren<Renderer>().material.color = new Color(0, 0.2f, 0.5f, 0.7f);
    }
    void createDCurve() {
        print("ofakosfksaf");
        dCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        dCurve.minimumControlPointDistance = 0.005f;
        dCurve.SetLineDiameter(0.01f);
        dCurve.SetInterpolationSteps(10);
    }

    // Update is called once per frame
    void Update()
    {
        
        SteamVR_Action_Pose poseActionR;
        poseActionR = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");

        vPosition = poseActionR[SteamVR_Input_Sources.RightHand].localPosition;
        qRotation = poseActionR[SteamVR_Input_Sources.RightHand].localRotation;

        PenPoint.transform.position = vPosition;
        PenPoint.transform.rotation = qRotation;

        rangeIndicatorScaling();

        oCurve.name = "oCurve";

        if (Mathf.Abs(weightSlider.axis.y) > Mathf.Abs(weightSlider.axis.x))
            weight += weightSlider.axis.y * 10;
        if (Mathf.Abs(weightSlider.axis.y) < Mathf.Abs(weightSlider.axis.x))
            range += weightSlider.axis.x * 0.01f;


        if (drawCurve.state)
        {

            Invoker.ExecuteCommand(new AddControlPointContinuousCommand(dCurve, vPosition));
            print("ollaaaa");
        }
        if (drawCurve.lastStateUp)
        {
            createDCurve();
        }

        if (undo.state)
        {
            Invoker.Undo();
        }
        if (redo.state)
        {
            Invoker.Redo();
        }
        if (drawOverSketchingCurve.state)
            {

            //OVERSKETCH

            //   createOCurve();
            //InvokerOCurve.ExecuteCommand(new AddControlPointContinuousCommand(oCurve, vPosition));


            //DELETE

            foreach (LineSketchObject lineSketchObject in FindObjectsOfType<LineSketchObject>())
            {
                Invoker.ExecuteCommand(new DeleteControlPointsByRadiusCommand(lineSketchObject, vPosition, range/2));
            }

        }

        //OVERSKETCH
        /*
        
            if (drawOverSketchingCurve.lastStateUp)
            {

                foreach (LineSketchObject lineSketchObject in FindObjectsOfType<LineSketchObject>())
                {
                Invoker.ExecuteCommand(new OverSketchLineCommand(lineSketchObject, oCurve, weight, range));
                  // lineSketchObject.SetControlPoints(overSketch.calculateScaledCurve(weight, range/2, oCurve, lineSketchObject));

                smoothingCurve(lineSketchObject, oCurve, 0.005f);
                //Invoker.ExecuteCommand(new RefineMeshCommand(lineSketchObject));
            }

                InvokerOCurve.ExecuteCommand(new DeleteObjectCommand(oCurve, SketchWorld));
                oCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
                
        }
        */

        //DELETION
        if (drawOverSketchingCurve.lastStateUp)
            {
                createDCurve();
            }
    }

    IEnumerator fadeOCurve(LineSketchObject oCurve)
    {
        Color color = oCurve.GetComponent<Renderer>().material.color;
        color.r = 0.5f;
        color.g = 0.1f;
        while (color.a > 0)
        {
            print("color " + color);
            color = new Color(color.r, color.g, color.b, color.a - 10f * Time.deltaTime);

            print("color " + color);
            yield return new WaitForSeconds(0.5f);
        }
        Invoker.ExecuteCommand(new DeleteObjectCommand(oCurve, SketchWorld));
    }
    void startFading(LineSketchObject oCurve)
    {
        StartCoroutine("fadeOCurve", oCurve);
    }

    /// <summary>
    /// Adjusts the range indicator.
    /// </summary>
    void rangeIndicatorScaling()
    {
        float currentSize = RangeIndicator.GetComponent<Renderer>().bounds.size.x;

        Vector3 scale = RangeIndicator.transform.localScale;

        scale.x = (range * scale.x / currentSize);
        scale.y = (range * scale.y / currentSize);
        scale.z = (range * scale.z / currentSize);

        RangeIndicator.transform.position = PenPoint.transform.position;
        RangeIndicator.transform.localScale = scale;

    }

    void smoothingCurve(LineSketchObject dCurve,LineSketchObject oCurve ,float minDistance) 
    {

        List<Vector3> controlPoints=dCurve.GetControlPoints();
        List<Vector3> OcontrolPoints = dCurve.GetControlPoints();



        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            for (int j = 0; j < OcontrolPoints.Count - 1; j++)
            {
                if ((controlPoints[i] - OcontrolPoints[j]).magnitude < range)
                {

                    int closePoints = 0;
                    if ((controlPoints[i] - controlPoints[i + 1]).magnitude < minDistance)
                    {




                        while ((controlPoints[i] - controlPoints[i + closePoints]).magnitude < minDistance)
                        {
                            if (closePoints + i < controlPoints.Count - 1)
                            {

                                closePoints++;
                            }
                            else break;
                        }






                        controlPoints.RemoveRange(i + 1, closePoints - 1);



                    }
                }
            }
        }
        dCurve.SetControlPoints(controlPoints);
        
    }

}
