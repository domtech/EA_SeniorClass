using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public Vector3 TransPos;

    Vector3 CurTransPos;

    public void Init ()
    {
        CurTransPos = TransPos;
    }

    #region Trans Pos
    /// <summary>
    ///检测位置是否发生变化,如果发生变化，那么执行改变操作.
    /// </summary>
    public void OnTransPos(List<TransformBase> list)
    {
        if (CurTransPos != TransPos)
        {
            var delta = TransPos - CurTransPos;
            CurTransPos = TransPos;
            for (var i = 0; i < list.Count; i++)
            {
                list[i].TransPos(delta);
            }
        }
    }
    #endregion
}
