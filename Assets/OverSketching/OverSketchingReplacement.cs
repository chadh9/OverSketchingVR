using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.SketchObjectManagement;


public class OverSketchingReplacement : MonoBehaviour
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

    //This is the implementation of the discarded Segment Replacement algorithm. It is not 100% done, but can be improved/repurposed

    private int shortestPath(Vector3 controllPoint, LineSketchObject line)
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


    public List<Vector3> calculateScaledCurve(LineSketchObject oCurve, LineSketchObject dCurve)
    {

        float n = dCurve.getNumberOfControlPoints();
        float m = oCurve.getNumberOfControlPoints();

        Vector3 xs = oCurve.GetControlPoints()[0];
        Vector3 xe = oCurve.GetControlPoints()[oCurve.GetControlPoints().Count - 1];

        int s = shortestPath(xs, dCurve);
        int e = shortestPath(xe, dCurve);

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

            if (dCurveControlPoints.Count - 1 < s + wt + 1) { it = dCurveControlPoints[s + wt]; }
            else
            {
                it = (dCurveControlPoints[s + wt] + ((dCurveControlPoints[s + wt + 1] - dCurveControlPoints[s + wt]) * vt));
            }
            list.Add(it);
            t++;
        }

        List<Vector3> scaledList = new List<Vector3>();
        float scale = 0.5f;


        for (int i = 0; i < m; i++)
        {

            scaledList.Add(list[i] + (scale * (oCurve.GetControlPoints()[i] - list[i])));
        }

        return calculateTransitionInterval(Mathf.RoundToInt(n), s, e, 0.4f, dCurveControlPoints, scaledList);

    }


    private List<Vector3> calculateTransitionInterval(int n, int s, int e, float v, List<Vector3> dCurve, List<Vector3> oCurve)
    {
        List<Vector3> t1 = new List<Vector3>();
        List<Vector3> t2 = new List<Vector3>();

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
            float temp1 = ie1 - is1;
            float transT = 0.5f * Mathf.Sin(0.5f * (t / temp1) * Mathf.PI);

            Vector3 vsmooth = (oCurve[0] - dCurve[s]) * transT;
            Vector3 xt = dCurve[is1 + t] + vsmooth;
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
