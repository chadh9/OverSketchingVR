﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KochanekBartelsSplines;

public class ExtrudeTest : MonoBehaviour
{
    private Vector3[] ControlPoints;

    KochanekBartelsSpline kochanekBartelsSpline;

    [SerializeField]
    private GameObject[] ControlPointObjects;

    private List<Vector3> InterpolatedPoints;

    [SerializeField]
    private GameObject extraControlPoint;

    private Meshing.LineExtruder lineExtruder;
    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> crossSectionShape = new List<Vector3> { new Vector3(1f, 0f, 0.5f), new Vector3(1f, 0f, -0.5f), new Vector3(0f, 0f, -1f), new Vector3(-1f, 0f, -0.5f), new Vector3(-1f, 0f, 0.5f), new Vector3(0f, 0f, 1f) };
        crossSectionShape.Reverse();
        List<Vector3> crossSectionShapeNormals = new List<Vector3>();
        foreach (Vector3 point in crossSectionShape) {
            crossSectionShapeNormals.Add(point.normalized);
        }

        kochanekBartelsSpline = new KochanekBartelsSpline();

        ControlPoints = new Vector3[ControlPointObjects.Length];
        for (int i = 0; i < ControlPointObjects.Length; i++)
        {
            ControlPoints[i] = ControlPointObjects[i].transform.position;
        }

        InterpolatedPoints = kochanekBartelsSpline.InterpolatedPoints;
        kochanekBartelsSpline.setControlPoints(ControlPoints);

        lineExtruder = new Meshing.LineExtruder(crossSectionShape, crossSectionShapeNormals, new Vector3(.2f, .2f, .2f));
        Mesh mesh = lineExtruder.getMesh(InterpolatedPoints);
        //SplineModificationInfo addInfo = kochanekBartelsSpline.insertControlPoint(0, extraControlPoint.transform.position);
        //SplineModificationInfo addInfo = kochanekBartelsSpline.deleteControlPoint(6);
        //SplineModificationInfo addInfo = kochanekBartelsSpline.setControlPoint(0,extraControlPoint.transform.position);
        //Debug.Log(addInfo);
        //mesh = lineExtruder.replacePoints(InterpolatedPoints.GetRange(InterpolatedPoints.Count - 40, 40),InterpolatedPoints.Count-40, 20);
        //mesh = lineExtruder.replacePoints(InterpolatedPoints.GetRange(addInfo.Index, addInfo.AddCount), addInfo.Index, addInfo.RemoveCount);


        MeshFilter meshFilter = GetComponent<MeshFilter>();

        //mesh.UploadMeshData(false);
        meshFilter.mesh = mesh;
    }

    private void Update()
    {
        Mesh mesh = lineExtruder.getMesh(InterpolatedPoints);
        //SplineModificationInfo addInfo = kochanekBartelsSpline.insertControlPoint(0, extraControlPoint.transform.position);
        //SplineModificationInfo addInfo = kochanekBartelsSpline.deleteControlPoint(6);
        SplineModificationInfo addInfo = kochanekBartelsSpline.setControlPoint(4,ControlPointObjects[4].transform.position);
        //Debug.Log(addInfo);
        //mesh = lineExtruder.replacePoints(InterpolatedPoints.GetRange(InterpolatedPoints.Count - 40, 40),InterpolatedPoints.Count-40, 20);
        mesh = lineExtruder.replacePoints(InterpolatedPoints.GetRange(addInfo.Index, addInfo.AddCount), addInfo.Index, addInfo.RemoveCount);


        MeshFilter meshFilter = GetComponent<MeshFilter>();

        //mesh.UploadMeshData(false);
        meshFilter.mesh = mesh;
    }
}