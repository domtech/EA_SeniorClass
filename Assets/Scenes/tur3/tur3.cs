using UnityEngine;
public class tur3 : MonoBehaviour
{
    public float Radius;
    public int MaxCircleNum;
    public GameObject SpawnCube;
    const float TAU = 6.28f;
    const float CircleRound = 10;
    public float Scale;
    public float RotSpeed;
    private void Start()
    {
        Generate();
    }

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
            }
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

    void Generate()
    {
        for (int i = 0; i < 10; i++)
        {
            float degree = 90f - 18 * i;
            CircleSurface(degree, Radius * Mathf.Cos(degree));
        }
    }


    private void Update()
    {
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
    }

}