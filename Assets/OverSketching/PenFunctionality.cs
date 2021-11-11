using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.Commands.Group;
using VRSketchingGeometry.SketchObjectManagement;
using UnityEngine.UI;

using Valve.VR;

public class PenFunctionality : MonoBehaviour
{

    public bool isDeletionPrototype=false;

    public DefaultReferences Defaults;
    public SketchWorld SketchWorld;

    private SteamVR_Action_Pose poseActionR;
    public SteamVR_Action_Boolean drawCurveButton;
    public SteamVR_Action_Boolean drawOverSketchingCurveButton;
    public SteamVR_Action_Boolean undoButton;
    public SteamVR_Action_Boolean redoButton;
    public SteamVR_Action_Boolean tutorialButton;
    public SteamVR_Action_Vector2 weightSlider;
    public SteamVR_Action_Vector2 rangeSlider;
    public GameObject displayButton;
    private CommandInvoker Invoker;
    private CommandInvoker InvokerOCurve;
    private SketchObjectGroup SketchObjectGroup;

  
    public Vector3 vPosition = new Vector3();
    public Quaternion qRotation = new Quaternion();

    public GameObject PenPoint;
    public GameObject tutorialImage;


    public GameObject RangeIndicator;

    private LineSketchObject dCurve;
    private LineSketchObject oCurve;

    public float weight;
    public float range;
    public float maxWeight;
    public float minWeight;
    private float unrestrictedWeight = 5000;
    private float unrestrictedRange = 0.1f;
    private float minDistance = 0.01f;
    private Text displayButtonText;


    void Start()
    {
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();

        SketchObjectGroup = Instantiate(Defaults.SketchObjectGroupPrefab).GetComponent<SketchObjectGroup>();

        displayButtonText = displayButton.gameObject.GetComponent<Text>();


        Invoker = new CommandInvoker();
        InvokerOCurve = new CommandInvoker();

        dCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        dCurve.minimumControlPointDistance = 0.05f;
        dCurve.SetLineDiameter(0.01f);
        dCurve.SetInterpolationSteps(10);

        oCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        oCurve.minimumControlPointDistance = minDistance;
        oCurve.SetInterpolationSteps(10);
        oCurve.SetLineDiameter(0.01f);
        oCurve.GetComponentInChildren<Renderer>().material.color = new Color(0, 0.1f, 1f, 0.7f);


        maxWeight = 50f ;
        minWeight= 1f;
        unrestrictedWeight = maxWeight/2;
        range = 0.1f;

    }

    private void createOCurve()
    {
        oCurve.minimumControlPointDistance = minDistance;
        oCurve.SetInterpolationSteps(10);
        oCurve.SetLineDiameter(0.01f);
        oCurve.GetComponentInChildren<Renderer>().material.color = new Color(0, 0.2f, 0.5f, 0.7f);
    }
    private void createDCurve()
    {


        Invoker.ExecuteCommand(new AddToGroupCommand(SketchObjectGroup, dCurve));
        dCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        dCurve.minimumControlPointDistance = minDistance;
        dCurve.SetLineDiameter(0.01f);
        dCurve.SetInterpolationSteps(10);
    }

    // Update is called once per frame
    void Update()
    {
        Oversketching();

    }

    private void Oversketching()
    {

        poseActionR = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");
        vPosition = poseActionR[SteamVR_Input_Sources.RightHand].localPosition;
        qRotation = poseActionR[SteamVR_Input_Sources.RightHand].localRotation;

        PenPoint.transform.position = vPosition;
        PenPoint.transform.rotation = qRotation;

        rangeIndicatorScaling();

        oCurve.name = "oCurve";



        weight = Mathf.Clamp(unrestrictedWeight, minWeight, maxWeight);
        range = Mathf.Clamp(unrestrictedRange, 0.09f, 0.8f);


        if (drawCurveButton.state)
        {
            displayButtonText.text = "Drawing";
            Invoker.ExecuteCommand(new AddControlPointContinuousCommand(dCurve, vPosition));
            print("ollaaaa");
        }
        else if (drawCurveButton.lastStateUp)
        {
            createDCurve();
        }

        else if (isDeletionPrototype && undoButton.state)
        {
            displayButtonText.text = "Undo";
            Invoker.Undo();
        }
        else if (isDeletionPrototype && redoButton.state)
        {
            displayButtonText.text = "Redo";
            Invoker.Redo();
        }

        else if (drawOverSketchingCurveButton.state)
        {
            displayButtonText.text = "Oversketching";
            createOCurve();

            InvokerOCurve.ExecuteCommand(new AddControlPointContinuousCommand(oCurve, vPosition));

        }
        else if (tutorialButton.state)
        {
            tutorialImage.SetActive(true);
        }
        else if (tutorialButton.lastStateUp)
        {
            tutorialImage.SetActive(false);
        }
        else if (drawOverSketchingCurveButton.lastStateUp)
        {
            if (isDeletionPrototype)
            {
                Deletion();
            }
            else
            {
                foreach (LineSketchObject lineSketchObject in SketchObjectGroup.GetComponentsInChildren<LineSketchObject>())
                {
                    List<Vector3> changedControlPoints = new List<Vector3>(lineSketchObject.GetControlPoints());
                    Invoker.ExecuteCommand(new OverSketchLineCommand(lineSketchObject, oCurve, Mathf.Pow(2,weight), range / 2));
                    if (!lineSketchObject.GetControlPoints().SequenceEqual(changedControlPoints))
                    {
                        Invoker.ExecuteCommand(new SimplifyLineCommand(lineSketchObject, minDistance / 20));
                        Invoker.ExecuteCommand(new PopulateLineCommand(lineSketchObject, minDistance * 1.5f));
                    }
                }

                InvokerOCurve.ExecuteCommand(new DeleteObjectCommand(oCurve, SketchWorld));
            }
            oCurve = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        }

        else if (Mathf.Abs(weightSlider.axis.y) > Mathf.Abs(weightSlider.axis.x))
        {
            unrestrictedWeight = weight - weightSlider.axis.y * maxWeight*Time.deltaTime;
            displayButtonText.text = "Adjusting weight";
        }
        else if (Mathf.Abs(weightSlider.axis.y) < Mathf.Abs(weightSlider.axis.x))
        {
            unrestrictedRange = range + weightSlider.axis.x * 0.01f;
            displayButtonText.text = "Adjusting range";
        }

        else displayButtonText.text = "";

    }
    /// <summary>
    /// Deletes control points within a certain radius around the pen
    /// </summary>
    private void Deletion()
    {

            displayButtonText.text = "Deletion";
            foreach (LineSketchObject lineSketchObject in FindObjectsOfType<LineSketchObject>())
            {
                Invoker.ExecuteCommand(new DeleteControlPointsByRadiusCommand(lineSketchObject, vPosition, range / 2));
            }

    }


    /// <summary>
    /// Adjusts the range indicator.
    /// </summary>
    private void rangeIndicatorScaling()
    {
        float currentSize = RangeIndicator.GetComponent<Renderer>().bounds.size.x;

        Vector3 scale = RangeIndicator.transform.localScale;

        scale.x = (range * scale.x / currentSize);
        scale.y = (range * scale.y / currentSize);
        scale.z = (range * scale.z / currentSize);

        RangeIndicator.transform.position = PenPoint.transform.position;
        RangeIndicator.transform.localScale = scale;

    }

    }

