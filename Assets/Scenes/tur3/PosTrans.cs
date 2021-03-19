
using UnityEngine;

public class PosTrans : Transformation
{

    public Vector3 Position;

    public override Matrix4x4 Matrix
    {
        get
        {
            var matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(1, 0, 0, Position.x));
            matrix.SetRow(1, new Vector4(0, 1, 0, Position.y));
            matrix.SetRow(2, new Vector4(0, 0, 1, Position.z));
            matrix.SetRow(3, new Vector4(0, 0, 0, 1));
            return matrix;

        }
    }
}
