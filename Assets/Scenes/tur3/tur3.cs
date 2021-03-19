using System.Collections.Generic;
using UnityEngine;

public class tur3 : MonoBehaviour
{
    #region Paras
    List<TransBase> TransBaseList;
    #endregion

    #region Sys
    private void Awake()
    {

        TransBaseList = new List<TransBase>();

        if (RoundNum %2 == 1)
        {
            RoundNum += 1;
        }
        GenerateSphere();
    }
    void Start()
    {
        InitUpdateMatrix();
    }

    private void Update()
    {
        UpdateTransMatrix();

        UpdateTransInfo();
    }
    #endregion

    #region Generate Target Sphere
    public GameObject SpawnSphere;

    public float MaxTargetRadius = 3;//目标半径.

    public float MaxTargetCircleNum = 20;//围绕的圈数.

    public float SpereScale = 0.5f;//每个小球的缩放比例

    public float RoundNum = 5;//需要圈数

    void CircleSurface (float _rate, float index)
    {
        var tmpRate = Mathf.Abs(_rate);

        var tmpRoundNum = (1 - tmpRate) * RoundNum;

        var tmpMaxTargetRadius = (1 - tmpRate) * MaxTargetRadius;

        var tmpMaxTargetCircleNum = (1 - tmpRate) * MaxTargetCircleNum;


        if(tmpRate == 1)
        {
            tmpRoundNum = 1;
            tmpMaxTargetRadius = 0f;
            tmpMaxTargetCircleNum = 1;
        }

        var tmpDegree = 90f - (180f/ RoundNum) * index;

        var height = MaxTargetRadius * Mathf.Sin(tmpDegree * Mathf.Deg2Rad);

        for (var j = 0; j < tmpRoundNum; j++)
        {
            var rate = (tmpRoundNum - j) / tmpRoundNum;
            var CurRadius = rate * tmpMaxTargetRadius;
            var CurCircleNum = rate * tmpMaxTargetCircleNum;
            var eachDegree = 360f / tmpMaxTargetCircleNum;

            for (var i = 0; i < CurCircleNum; i++)
            {
                var obj = Instantiate(SpawnSphere);
                
                obj.transform.SetParent(transform);

                var tb = obj.GetComponent<TransBase>();

                var tmpPos = new Vector3(CurRadius * Mathf.Cos(eachDegree * i), height, CurRadius * Mathf.Sin(eachDegree * i));
                
                tb.SetTransOrigPos(tmpPos);

                obj.transform.localPosition = tmpPos;

                obj.transform.localRotation = Quaternion.identity;
                
                obj.transform.localScale = Vector3.one * SpereScale;

                var mat = obj.GetComponent<MeshRenderer>().material;

                mat.SetColor("_Color", new Color(
                    Mathf.Abs(Mathf.Cos(eachDegree * i * Mathf.Deg2Rad)),
                    Mathf.Abs(Mathf.Sin(tmpDegree * Mathf.Deg2Rad)),
                    Mathf.Abs(Mathf.Sin(eachDegree * i * Mathf.Deg2Rad))
                    ));

                TransBaseList.Add(tb);
            }
        }
    }
    void GenerateSphere()
    {

        for(var i = 0; i < RoundNum + 1;i++)
        {
            var rate = (RoundNum*0.5f - i) / (RoundNum*0.5f);
            CircleSurface(rate, i);
        }
    }
    #endregion

    #region Update Transform Matrix

    List<Transformation> TransList;

    Matrix4x4 transformation;

    void InitUpdateMatrix()
    {
        TransList = new List<Transformation>();
    }

    void UpdateTransMatrix()
    {
        GetComponents(TransList);

        transformation = TransList[0].Matrix;

        if(TransList.Count > 1)
        {
            for(var i = 1; i< TransList.Count; i++)
            {
                transformation = TransList[i].Matrix * transformation;
            }
        }
    }


    void UpdateTransInfo()
    {
        for(var i = 0; i < TransBaseList.Count; i++)
        {
            var item = TransBaseList[i];
            item.transform.localPosition = transformation.MultiplyPoint(item.OrigTransPos);
        }
    }

    #endregion
}
