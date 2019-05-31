using UnityEngine;

[RequireComponent(typeof(Enemy))]   // 특정 컴포넌트를 자동으로 추가해줍니다 이경우 Enemy가 자동으로 추가되지요..
public class EnemyMovement : MonoBehaviour
{
    private Transform target;   // 웨이포인트 위치
    private int wavePointIndex = 0; // 웨이포인트 인데스죠.

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = WayPoint.points[0];    // 처음 시작시 웨이포인트(당연히 첫번째 웨이포인트를 가리킵니다.)
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position; // 이객체(적)의 위치와 타겟(웨이포인트)의 위치를 시실간으로(업데이트니깐) 갱신하는 벡터죠.
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);  // 월드 좌표기준으로 적의 위초로 이동 시키죠. 몰루겠으면 Translate 참조하세요 ㅎㅎ.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)  // 가까이 붙으면 버버버버버버 거리겠죠 ㅎㅎ. 그러니깐 타겟과 위치가차이가 어느정도 날경우 다음 웨이포인트를 갈수있게 설정합시다 ㅇㅋ?
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed; //   기존 이동속도로 복기

        Quaternion lookRotation = Quaternion.LookRotation(dir); // 쿼터니언을 이용해서 회전시키죠.
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemy.turnSpeed).eulerAngles;   // 정규화입니다 ㅎㅎ 유니티 스크립트 참조하세용..
        transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);   // 값만큼 회전
    }

    private void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoint.points.Length - 1)   // 마지막 웨이포인트인덱스라면 객체를 없에겠죠 ㅎㅎ.
        {
            EndPath();
        }
        if (wavePointIndex < WayPoint.points.Length - 1)    // 배열 초과를 막기위해서
        {
            wavePointIndex++;   // 인덱스 추가
            target = WayPoint.points[wavePointIndex];   // 타겟(웨이포인트)인덱스 갱신
        }
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
