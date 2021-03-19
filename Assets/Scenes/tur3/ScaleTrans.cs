using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTrans : Transformation
{
    public Vector3 Scale = Vector3.one;

    public override Matrix4x4 Matrix
    {
        get
        {
            var matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(Scale.x, 0, 0, 0));
            matrix.SetRow(1, new Vector4(0, Scale.y, 0, 0));
            matrix.SetRow(2, new Vector4(0, 0, Scale.z, 0));
            matrix.SetRow(3, new Vector4(0, 0, 0, 1));
            return matrix;

        }
    }
}
