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

public class OverSketchingExample : MonoBehaviour
{
    public bool toggle=true;
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

    // Start is called before the first frame update
    void Start()
    {
        //Create a SketchWorld, many commands require a SketchWorld to be present
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();




        LineSketchObject test1 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();

        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, -1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 0, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 2, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 4, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 5, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 6, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 7, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 8, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 9, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 10, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 11, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test1, new Vector3(1, 12, 1)));
        test1.name = "Curve D";


        LineSketchObject test11 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();

        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, -1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 0, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 1, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 2, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 3, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 4, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 5, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 6, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 7, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 8, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 9, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 10, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 11, 1)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test11, new Vector3(10, 12, 1)));

        LineSketchObject test2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        Invoker = new CommandInvoker();

        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(10, 1, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(10, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test2, new Vector3(10, 3, 2)));

  
        LineSketchObject test3 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

        /*
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(3, 2, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 1, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(-5, 12, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(12, -6, 2)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(-3, 2, 12)));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, -1, -2)));
        */

         Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 7, 1)));
         Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 6, 2)));
         Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 5, 3)));
         Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 4, 2)));
         Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(1, 3, 1)));

        //Xe = Start of changing curve
        Vector3 controlPoint1 = test2.GetControlPoints()[0];
        //Xs = End of changing curve
        Vector3 controlPoint2 = test2.GetControlPoints()[test2.GetControlPoints().Count-1];

        Debug.Log("nearest control point: "+shortestPath(controlPoint1, test1));
        Debug.Log("nearest control point coordinates: "+test1.GetControlPoints()[shortestPath(controlPoint1, test1)]);
        Debug.Log("nearest control point: " + shortestPath(controlPoint2, test1));
        Debug.Log("nearest control point coordinates: " + test1.GetControlPoints()[shortestPath(controlPoint2, test1)]);

        //       LineSketchObject test3 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

        float scale = 0.5f;

        List<Vector3> t1 = test1.GetControlPoints();
        List<Vector3> t2 = test2.GetControlPoints();


            calculateReplacementInterval(test11.getNumberOfControlPoints(), test2.getNumberOfControlPoints(),
                shortestPath(controlPoint1, test11), shortestPath(controlPoint2, test11), test11, test2);
        

        //Xe = Start of changing curve
          Vector3 controlPoint3 = test3.GetControlPoints()[0];
        //Xs = End of changing curve
          Vector3 controlPoint4 = test3.GetControlPoints()[test3.GetControlPoints().Count - 1];
        
        Debug.Log("nearest control point: " + shortestPath(controlPoint3, test1));
        Debug.Log("nearest control point coordinates: " + test1.GetControlPoints()[shortestPath(controlPoint3, test1)]);
        Debug.Log("nearest control point: " + shortestPath(controlPoint4, test1));
        Debug.Log("nearest control point coordinates: " + test1.GetControlPoints()[shortestPath(controlPoint4, test1)]);


            calculateReplacementInterval(test1.getNumberOfControlPoints(), test3.getNumberOfControlPoints(),
               shortestPath(controlPoint3, test1), shortestPath(controlPoint4, test1), test1, test3);
        



        /*
         foreach(var x in t1)
         {
             Debug.Log(x);
             var y = new Vector3(x.x, x.y, x.z);
             Invoker.ExecuteCommand(new AddControlPointCommand(test3, new Vector3(x.x, x.y, x.z)));
         }
         */

        /*
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, scale * (new Vector3(1, 3, 2) + new Vector3(0, 0, 1))));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, scale * (new Vector3(1, 4, 2) + new Vector3(0, 0, 1))));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, scale * (new Vector3(1, 5, 2) + new Vector3(0, 0, 1))));
        Invoker.ExecuteCommand(new AddControlPointCommand(test3, scale * (new Vector3(1, 6, 2) + new Vector3(0, 0, 1))));
        */
        /* LineSketchObject test6 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
         Invoker.ExecuteCommand(new AddControlPointCommand(test6, scale * (new Vector3(1, 3, 2) + new Vector3(0, 0, 1))));
 Invoker.ExecuteCommand(new AddControlPointCommand(test6, scale * (new Vector3(1, 4, 2) + new Vector3(0, 0, 1))));
 Invoker.ExecuteCommand(new AddControlPointCommand(test6, scale * (new Vector3(1, 5, 2) + new Vector3(0, 0, 1))));
 Invoker.ExecuteCommand(new AddControlPointCommand(test6, scale * (new Vector3(1, 6, 2) + new Vector3(0, 0, 1))));
 */
    }



    int shortestPath(Vector3 controllPoint, LineSketchObject line)
    {

        int nearestPointIndex=0;
        
        Vector3 distance= new Vector3(Mathf.Infinity,Mathf.Infinity,Mathf.Infinity);
        for (int i=0;i<line.GetControlPoints().Count;i++)
        {
            if ((controllPoint - line.GetControlPoints()[i]).sqrMagnitude < (controllPoint - distance).sqrMagnitude)
            {
                distance = line.GetControlPoints()[i];
                nearestPointIndex = i;
            }
        }
        return nearestPointIndex;
    }



   
    List<Vector3> calculateReplacementInterval(float n, float m, int s , int e, LineSketchObject dCurve, LineSketchObject oCurve)
    {
        List<Vector3> dCurveControlPoints = dCurve.GetControlPoints();
        List<Vector3> list = new List<Vector3>();
        
        //Debug.Log("interval "+l);
        int l = e - s;
        int wt;
        int t = 0 ;
        //for (int t = 0; t <= m; t++)
        while(t<m)
        {
            Debug.Log("______________ "+ t+" ______________________");
            wt = Mathf.FloorToInt(t * (l / (m-1)));
            float vt = (t * (l/(m-1))) - wt;
            Vector3 it;
            // if (t == m - 1) { it = (line[s + wt] + ((line[s + wt]) * vt)); }

            if (dCurveControlPoints.Count-1<s+wt+1) { it = (dCurveControlPoints[s + wt]); }
            else {
                it = (dCurveControlPoints[s + wt] + ((dCurveControlPoints[s + wt + 1] - dCurveControlPoints[s + wt]) * vt));
                }
            Debug.Log("wt: " +Mathf.FloorToInt(t * (l / m)) + " vt: "+ "it: ");
            list.Add(it);
            t++;
        }
        

        foreach (var x in list)
        {
            LineSketchObject test4 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            test4.name = "points";
            Invoker.ExecuteCommand(new AddControlPointCommand(test4, x));
            Debug.Log(x);
        }
        LineSketchObject test5 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        test5.name="scaled curve";

        List<Vector3> scaledList = new List<Vector3>();
        



        float scale = 0.5f;
        float weight;

        Debug.Log("______________________________________________________________________");

        for (int i=0; i<m ; i++)
        {
            /*
            float j = i;
            weight = (j)/(list.Count-1);
            scale =  0.3f * Mathf.Sin(weight * Mathf.PI);

            Debug.Log("scale "+scale + "weight :" +weight);
    */        
    
    scaledList.Add(list[i] + (scale *((oCurve.GetControlPoints()[i] - list[i]))));
            Debug.Log(list[i]+" controlpoint " + oCurve.GetControlPoints()[i] +"  unscaled " +list[i]);
            Debug.Log("scaled "+scaledList[i]);
            //Invoker.ExecuteCommand(new AddControlPointCommand(test5, scaledList[i]));
        }

        //calculateTransitionInterval(Mathf.RoundToInt(n), s, e, 0.4f, dCurveControlPoints, oCurve.GetControlPoints());
        calculateTransitionInterval(Mathf.RoundToInt(n), s, e, 0.4f, dCurveControlPoints, scaledList);

        return list;

    }


    void calculateTransitionInterval(int n, int s, int e, float v, List<Vector3> dCurve, List<Vector3> oCurve)
    {
        List<Vector3> t1=new List<Vector3>();
        List<Vector3> t2= new List<Vector3>();

        Debug.Log("size i : "+ n * v / 2 + " "+ Mathf.RoundToInt(n * v / 2) );
        float i = n * v / 2;
        int is1 = s - 1 - Mathf.RoundToInt(i);
        
        int ie1 = s - 1;
        int is2 = e + 1;
        int ie2 = e + 1 + Mathf.RoundToInt(i);
        if (s - 1 - i < 0) {
            is1 = 0;
        }
        if (e + 1 + i > n)
        {
            ie2 = n;
        }


        Debug.Log(Mathf.RoundToInt(i));

        for (int t = 0 ; t<= ie1-is1 ; t++) {
            //it/t?
            // float transT = 0.5f * Mathf.Sin(t + 0.5f *Mathf.PI)+0.5f;
            float temp1 = ie1 - is1;

      //      float transT = 0.5f * Mathf.Sin(t/(temp1));
            float transT = 0.5f * Mathf.Sin(0.5f * (t / temp1) * Mathf.PI);
            Debug.Log(" i: "+i +" t "+ t+" sin:"+ Mathf.Sin(0.5f* (t/i)  *Mathf.PI));
            Debug.Log("inside: "+ (t/temp1) );
            Debug.Log("transt: "+ transT);
            Vector3 vsmooth = (oCurve[0] - dCurve[s])*transT;
            Vector3 xt = dCurve[is1+t] + vsmooth;



            LineSketchObject transitionalInterval = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();

          //  transitionalInterval.name = "interval";

            Invoker.ExecuteCommand(new AddControlPointCommand(transitionalInterval, xt));

            t1.Add(xt);

            Debug.Log("xt: " +xt);
        
  
        }
        for (int t = 0; t < ie2 - is2; t++)
        {
            float temp2 = (ie2 - is2);
            Debug.Log("temp2 "+temp2);
            float transT2 = 0.5f * Mathf.Cos(0.5f * (t / temp2) * Mathf.PI);
            Vector3 vsmooth2 = (oCurve[oCurve.Count - 1] - dCurve[e]) * transT2;
            Vector3 xt2 = dCurve[is2 + t] + vsmooth2;
            LineSketchObject transitionalInterval2 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker.ExecuteCommand(new AddControlPointCommand(transitionalInterval2, xt2));
            Debug.Log("transt2: "+transT2);
            t2.Add(xt2);
        }

        LineSketchObject scaled = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        scaled.name = "scaledCurve";
        foreach (var x in t1) {
            Invoker.ExecuteCommand(new AddControlPointCommand(scaled, x));
        }
        foreach (var x in oCurve)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(scaled, x));
        }
        foreach (var x in t2)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(scaled, x));
        }
    }

    List<Vector3> calculateReplacementInterval2(float n, float m, int s, int e, LineSketchObject dCurve, LineSketchObject oCurve)
    {
        List<Vector3> list = new List<Vector3>();
        foreach (var cp in oCurve.GetControlPoints())
        {
            shortestPath(cp, dCurve);
            LineSketchObject test4 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker.ExecuteCommand(new AddControlPointCommand(test4, dCurve.GetControlPoints()[shortestPath(cp, dCurve)]));
            list.Add(dCurve.GetControlPoints()[shortestPath(cp, dCurve)]);
        }

        LineSketchObject test5 = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        List<Vector3> scaledList = new List<Vector3>();

        float scale = 0.2f;

        for (int i = 0; i < m; i++)
        {
            scaledList.Add(list[i] + (scale * ((oCurve.GetControlPoints()[i] - list[i]))));
            Debug.Log(list[i] + " controlpoint " + oCurve.GetControlPoints()[i] + "  unscaled " + list[i]);
            Debug.Log("scaled " + scaledList[i]);
            Invoker.ExecuteCommand(new AddControlPointCommand(test5, scaledList[i]));
        }

        return new List<Vector3>();
    }
}
