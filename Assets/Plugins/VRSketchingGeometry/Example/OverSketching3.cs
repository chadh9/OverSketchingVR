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

public class OverSketching3 : MonoBehaviour
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

    public GameObject rangeIndicator;

    void Start()
    {





        //Create a SketchWorld, many commands require a SketchWorld to be present
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();



        test0 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();
        /*
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -2, 1.5f)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -1, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 0, 2.5f)));


        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 1, 3)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 2, 2.5f)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 3, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 4, 1.5f)));

        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 5, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 6, 1.5f)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 7, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 8, 2.5f)));
        
    */
        test0.name = "test0";


        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, -1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 0, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 2, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 4, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 5, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test0, new Vector3(1, 6, 1)));


        test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        test2.name = "test2";
        Invoker = new CommandInvoker();

   
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 3, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 4, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 3, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 4, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 3, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 4, 2)));


        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 10, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(1, 0, 2)));

        test0.GetComponent<Renderer>().material.SetColor("_Color",Color.red);

        SketchObjectGroup = Instantiate(Defaults.SketchObjectGroupPrefab).GetComponent<SketchObjectGroup>();
        SketchObjectGroup.name = "SketchObjectGroup";
        Invoker.ExecuteCommand(new AddToGroupCommand(SketchObjectGroup, test0));



        SketchObjectSelection = Instantiate(Defaults.SketchObjectSelectionPrefab).GetComponent<SketchObjectSelection>();
        Invoker.ExecuteCommand(new AddToSelectionAndHighlightCommand(SketchObjectSelection, SketchObjectGroup));
        Invoker.ExecuteCommand(new RemoveFromSelectionAndRevertHighlightCommand(SketchObjectSelection,test0));


        Vector3 xs = test2.GetControlPoints()[0];
        //Xs = End of changing curve
        Vector3 xe = test2.GetControlPoints()[test2.GetControlPoints().Count - 1];

        /*
        Invoker.ExecuteCommand(new OverSketchLineCommand(test0, test2, 1));
        Invoker.Undo();
        Invoker.Redo();
        */

        test0.SetControlPoints(calculateScaledCurve(1,1, test2, test0));

        // test0.SetControlPoints(calculateScaledCurve(10,test2,test0));


        // test0.SetControlPoints(calculateScaledCurve3(test2, test0));
        // calculateScaledCurve3(test0.getNumberOfControlPoints(), test2.getNumberOfControlPoints(), shortestPath(xs, test0), shortestPath(xe, test0), test0, test2);


        // test0.SetControlPoints(calculateScaledCurve(test2, test0));
        /*     test0.SetControlPoints(calculateScaledCurve2(test2, test0));
             test0.SetControlPoints(calculateScaledCurve2(test2, test0));
             test0.SetControlPoints(calculateScaledCurve2(test2, test0));
             test0.SetControlPoints(calculateScaledCurve2(test2, test0));
             test0.SetControlPoints(calculateScaledCurve2(test2, test0));
             test0.SetControlPoints(calculateScaledCurve2(test2, test0));*/
        foreach (var x in test0.GetControlPoints())
        {
            var test5 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker.ExecuteCommand(new AddControlPointCommand(test5, x));
        }

    }


    List<Vector3> calculateScaledCurve2(LineSketchObject oCurve, LineSketchObject dCurve)
    {

        test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

        Invoker = new CommandInvoker();

        List<Vector3> scaledPoints = dCurve.GetControlPoints();

        foreach (var x in oCurve.GetControlPoints())
        {
            for (int i = 0; i < dCurve.GetControlPoints().Count; i++)
            {
                Vector3 scaledPoint1 = (dCurve.GetControlPoints()[i] + (x - dCurve.GetControlPoints()[i]) * 0.5f);
                scaledPoints[i] = (dCurve.GetControlPoints()[i] + (x - dCurve.GetControlPoints()[i]) * (1 / Mathf.Pow(2, (x - dCurve.GetControlPoints()[i]).sqrMagnitude)));
                print("test " + ((x - dCurve.GetControlPoints()[i]).sqrMagnitude));

            }


        }
        foreach (var x in scaledPoints)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(test2, x));
        }
        return new List<Vector3>();
    }




    public List<Vector3> calculateScaledCurve(float weight,float range, LineSketchObject oCurve, LineSketchObject dCurve)
    {
        //standard weight : 1000

        List<Vector3> list = new List<Vector3>();

        for (int i = 0; i < dCurve.getNumberOfControlPoints(); i++)
        {
            list.Add(new Vector3());


            for (int j = 0; j < oCurve.GetControlPoints().Count; j++)
            {

                if ((oCurve.GetControlPoints()[j]-dCurve.GetControlPoints()[i]).magnitude > range)
                {

                    //list[i] += new Vector3(0,0,0);
                }
                else {

                    list[i] += ((oCurve.GetControlPoints()[j] - dCurve.GetControlPoints()[i]) * (1 / Mathf.Pow(1.1f, weight * (oCurve.GetControlPoints()[j] - dCurve.GetControlPoints()[i]).sqrMagnitude)));
                }
            }
            list[i] = dCurve.GetControlPoints()[i] + (list[i] / oCurve.getNumberOfControlPoints());
        }

        return list;
    }


    int shortestPath(Vector3 controllPoint, LineSketchObject line)
    {

        int nearestPointIndex = 0;

        Vector3 distance = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        for (int i = 0; i < line.GetControlPoints().Count; i++)
        {
            if ((controllPoint - line.GetControlPoints()[i]).sqrMagnitude < (controllPoint - distance).sqrMagnitude)
            {
                distance = line.GetControlPoints()[i];
                nearestPointIndex = i;
            }
        }
        return nearestPointIndex;
    }


    public List<Vector3> calculateScaledCurve3(LineSketchObject oCurve, LineSketchObject dCurve)
    {

        float n = dCurve.getNumberOfControlPoints();
        float m = oCurve.getNumberOfControlPoints();

        Vector3 xs = oCurve.GetControlPoints()[0];
        //Xs = End of changing curve
        Vector3 xe = oCurve.GetControlPoints()[test2.GetControlPoints().Count - 1];

        int s = shortestPath(xs, test0);
        int e = shortestPath(xe, test0);

        List<Vector3> dCurveControlPoints = dCurve.GetControlPoints();
        List<Vector3> list = new List<Vector3>();


        int l = e - s;
        int wt;
        int t = 0;

        while (t < m)
        {
          
            wt = Mathf.FloorToInt(t * (l / (m - 1)));
            float vt = (t * (l / (m - 1))) - wt;
            Vector3 it;
            // if (t == m - 1) { it = (line[s + wt] + ((line[s + wt]) * vt)); }

            if (dCurveControlPoints.Count - 1 < s + wt + 1) { it = dCurveControlPoints[s + wt]; }
            else
            {
                it = (dCurveControlPoints[s + wt] + ((dCurveControlPoints[s + wt + 1] - dCurveControlPoints[s + wt]) * vt));
            }
            list.Add(it);
            t++;
        }



        LineSketchObject test5 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        test5.name = "scaled curve";

        List<Vector3> scaledList = new List<Vector3>();




        float scale = 0.5f;

        Debug.Log("______________________________________________________________________");

        for (int i = 0; i < m; i++)
        {

            scaledList.Add(list[i] + (scale * ((oCurve.GetControlPoints()[i] - list[i]))));
        }



        return calculateTransitionInterval(Mathf.RoundToInt(n), s, e, 0.4f, dCurveControlPoints, scaledList); 

    }


    List<Vector3> calculateTransitionInterval(int n, int s, int e, float v, List<Vector3> dCurve, List<Vector3> oCurve)
    {
        List<Vector3> t1 = new List<Vector3>();
        List<Vector3> t2 = new List<Vector3>();

        Debug.Log("size i : " + n * v / 2 + " " + Mathf.RoundToInt(n * v / 2));
        
        float i = n * v / 2;
        int is1;
        int ie1;
        int is2;
        int ie2;

        if (s - 1 - i < 0)
        {
            is1 = 0;
            ie1 = 0;
        }
        else
        {
            is1 = s - 1 - Mathf.RoundToInt(i);
            ie1 = s - 1;
        }
        if (e + 1 + i > n)
        {
            is2 = n; 
            ie2 = n;
        }
        else
        {
            is2 = e + 1;
            ie2 = e + 1 + Mathf.RoundToInt(i);
        }



        for (int t = 0; t <= ie1 - is1; t++)
        {
            //it/t?
            // float transT = 0.5f * Mathf.Sin(t + 0.5f *Mathf.PI)+0.5f;
            float temp1 = ie1 - is1;

            //      float transT = 0.5f * Mathf.Sin(t/(temp1));
            float transT = 0.5f * Mathf.Sin(0.5f * (t / temp1) * Mathf.PI);

            Vector3 vsmooth = (oCurve[0] - dCurve[s]) * transT;
            Vector3 xt = dCurve[is1 + t] + vsmooth;


            //  transitionalInterval.name = "interval";

            t1.Add(xt);


        }
        for (int t = 0; t < ie2 - is2; t++)
        {
            float temp2 = (ie2 - is2);
            float transT2 = 0.5f * Mathf.Cos(0.5f * (t / temp2) * Mathf.PI);
            Vector3 vsmooth2 = (oCurve[oCurve.Count - 1] - dCurve[e]) * transT2;
            Vector3 xt2 = dCurve[is2 + t] + vsmooth2;
            t2.Add(xt2);
        }


        t1.AddRange(oCurve);
        t1.AddRange(t2);
        return t1;
    }
   

}
