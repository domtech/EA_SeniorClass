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

    List<TransformBase> TransList;
    Transformation transInst;

    #endregion

    #region Sys
    private void Awake()
    {
        TransList = new List<TransformBase>();
        transInst = gameObject.GetComponent<Transformation>();
    }

    private void Start()
    {
        Generate();
        transInst.Init();
    }

    private void Update()
    {
        transInst.OnTransPos(TransList);
    }
    #endregion

    #region Genrate Sphere
    void CircleSurface(float degree, float radius)
    {
        float tmpRadius = radius;
        for (var j = 0; j < CircleRound; j++)
        {
            var tmpCircleNum = MaxCircleNum - MaxCircleNum / CircleRound * j;
            var eachDegree = 360f / tmpCircleNum;
            tmpRadius = radius * (tmpCircleNum/ MaxCircleNum);
            for (var i = 0; i < tmpCircleNum; i++)
            {
                var obj = Instantiate(SpawnCube);
                
                obj.transform.SetParent(transform);
               
                obj.transform.localPosition = new Vector3(tmpRadius * Mathf.Cos(eachDegree * i), Radius * Mathf.Sin(degree), tmpRadius * Mathf.Sin(eachDegree * i));
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one * Scale;
                obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(
                  Mathf.Abs(Mathf.Cos(eachDegree * i)),
                  Mathf.Abs(Mathf.Sin(degree)),
                  Mathf.Abs(Mathf.Sin(eachDegree * i))
                 ));

                TransList.Add(obj.GetComponent<TransformBase>());
            }
        }
    }

    void Generate()
    {
        for (int i = 0; i < 10; i++)
        {
            float degree = 90f - 18 * i;
            CircleSurface(degree, Radius * Mathf.Cos(degree));
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