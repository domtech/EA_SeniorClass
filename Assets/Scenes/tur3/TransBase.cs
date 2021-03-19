using UnityEngine;

public class TransBase : MonoBehaviour
{

    private Vector3 origLocalPos;
    public Vector3 OrigLocalPos => origLocalPos;

    public void SetOrigLocalPos (Vector3 pos)
    {
        origLocalPos = pos;
    }

}
