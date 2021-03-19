using UnityEngine;

public class tur3 : MonoBehaviour
{
    #region Paras
    #endregion

    #region Sys
    private void Awake()
    {
        GenerateSphere();
    }
    void Start()
    {
        
    }
    #endregion

    #region Generate Target Sphere
    public GameObject SpawnSphere;

    public float MaxTargetRadius = 3;//目标半径.

    public float MaxTargetCircleNum = 20;//围绕的圈数.

    public float SpereScale = 0.5f;//每个小球的缩放比例

    void CircleSurface ()
    {
        //一圈要放多少个球体

        var eachDegree = 360f / MaxTargetCircleNum;
        for (var i = 0; i < MaxTargetCircleNum; i++)
        {
            var obj = Instantiate(SpawnSphere);

            obj.transform.SetParent(transform);

            obj.transform.localPosition = new Vector3(MaxTargetRadius * Mathf.Cos(eachDegree * i), 0, MaxTargetRadius * Mathf.Sin(eachDegree * i));

            obj.transform.localRotation = Quaternion.identity;

            obj.transform.localScale = Vector3.one * SpereScale;
        }

    }
    void GenerateSphere()
    {
        CircleSurface();
    }
    #endregion
}
