using UnityEngine;

public class tur3 : MonoBehaviour
{
    public float Radius;

    public int MaxCircleNum;

    public GameObject SpawnCube;

    const float TAU = 6.28f;

    const float CircleRound = 5;

    void Start()
    {

        
        
        for(int i = 0; i < CircleRound*2; i++)
        {
            float degree = 90f - 18 * i;
            CircleSurface(degree);
        }
    }

    void OneCircle(int tmpCircleNum, float tmpRadius)
    {

        var eachDegree = 360f / tmpCircleNum;
        for (var i = 0; i < tmpCircleNum; i++)
        {
            var obj = Instantiate(SpawnCube);
            obj.transform.SetParent(transform);

            obj.transform.position = transform.position + new Vector3(tmpRadius * Mathf.Cos(eachDegree * i), 0f, tmpRadius * Mathf.Sin(eachDegree * i));
            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 0.5f;
        }
    }

    void CircleSurface (float degree)
    {
        var eachDegree = 360f / MaxCircleNum;

        float tmpRadius = Radius - Radius*Mathf.Cos(degree);

        for (var j = 0; j < CircleRound; j++)
        {
            var tmpCircleNum = MaxCircleNum - MaxCircleNum / CircleRound * j;
            for (var i = 0; i < tmpCircleNum; i++)
            {
                var obj = Instantiate(SpawnCube);
                obj.transform.SetParent(transform);

                tmpRadius = tmpRadius * (tmpCircleNum / MaxCircleNum);

                obj.transform.position = transform.position + new Vector3(tmpRadius * Mathf.Cos(eachDegree * i), Radius * Mathf.Sin(degree), tmpRadius * Mathf.Sin(eachDegree * i));
                obj.transform.rotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one * 0.5f;
            }
        }
    }

}
