using UnityEngine;

public class TransformBase : MonoBehaviour
{

    public void TransPos(Vector3 deltaPos)
    {
        transform.localPosition += deltaPos;
    }
}
