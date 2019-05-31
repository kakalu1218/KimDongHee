using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static Transform[] points; // 전역으로 위치정보를 나타내죠 ㅎㅎ.

    private void Start()
    {
        points = new Transform[transform.childCount]; // 배열을 하위객체들의 숫자만큼 채우고 ㅎㅎ.
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);  // 하위객체 순서대로 집어넣어주세요(찡긋).
        }
    }
}
