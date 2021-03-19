using System.Collections.Generic;
using UnityEngine;
public class tur3 : MonoBehaviour
{
    #region Params
    public float Radius;
    public int MaxCircleNum;
    public GameObject SpawnCube;
    const float CircleRound = 10;
    public float Scale;
    List<Transformation> TransList;
    List<TransBase> TransBaseList;
    #endregion

    #region Sys
    private void Awake()
    {
        TransBaseList = new List<TransBase>();
        TransList = new List<Transformation>();
    }

    private void Start()
    {
        Generate();

    }

    private void Update()
    {
        UpdateMatrixTrans();
        UpdateTransInfo();
    }
    #endregion

    #region Genrate Sphere
    void Generate()
    {
        for (int i = 0; i < 10; i++)
        {
            float degree = 90f - 18 * i;
            CircleSurface(degree, Radius * Mathf.Cos(degree));
        }
    }

    void CircleSurface(float degree, float radius)
    {
        float tmpRadius = radius;
        for (var j = 0; j < CircleRound; j++)
        {
            var tmpCircleNum = MaxCircleNum - MaxCircleNum / CircleRound * j;
            var eachDegree = 360f / tmpCircleNum;
            tmpRadius = radius * (tmpCircleNum / MaxCircleNum);
            for (var i = 0; i < tmpCircleNum; i++)
            {
                var obj = Instantiate(SpawnCube);
                var transbase = obj.GetComponent<TransBase>();
                obj.transform.SetParent(transform);
                transbase.SetOrigLocalPos(new Vector3(tmpRadius * Mathf.Cos(eachDegree * i), Radius * Mathf.Sin(degree), tmpRadius * Mathf.Sin(eachDegree * i)));
                obj.transform.localPosition = transbase.OrigLocalPos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one * Scale;
                obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(
                  Mathf.Abs(Mathf.Cos(eachDegree * i)),
                  Mathf.Abs(Mathf.Sin(degree)),
                  Mathf.Abs(Mathf.Sin(eachDegree * i))
                 ));

                TransBaseList.Add(transbase);
            }
        }
    }

    #endregion

    #region Update Matrix Transformation
    Matrix4x4 transformation;
    void UpdateMatrixTrans ()
    {
        GetComponents<Transformation>(TransList);

        transformation = TransList[0].Matrix;

        if(TransList.Count > 1)
        {
            for(var i = 1; i < TransList.Count; i++)
            {
                transformation = TransList[i].Matrix * transformation;
            }
        }

    }

    void UpdateTransInfo()
    {
        for(var i = 0; i < TransBaseList.Count; i++)
        {
            var tb = TransBaseList[i];
            tb.transform.localPosition = transformation.MultiplyPoint(tb.OrigLocalPos);
        }
    }
    #endregion
}







//void OneCircle(int tmpCircleNum, float tmpRadius)
//{
//    var eachDegree = 360f / tmpCircleNum;
//    for (var i = 0; i < tmpCircleNum; i++)
//    {
//        var obj = Instantiate(SpawnCube);
//        obj.transform.SetParent(transform);
//        obj.transform.position = transform.position + new Vector3(tmpRadius * Mathf.Cos(eachDegree * i), 0f, tmpRadius * Mathf.Sin(eachDegree * i));
//        obj.transform.rotation = Quaternion.identity;
//        obj.transform.localScale = Vector3.one * 0.5f;
//    }
//}