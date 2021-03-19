using UnityEngine;

public class TransBase : MonoBehaviour
{

    private Vector3 origTransPos;

    public Vector3 OrigTransPos => origTransPos;

    public void SetTransOrigPos (Vector3 orig)
    {
        origTransPos = orig;
    }

}
