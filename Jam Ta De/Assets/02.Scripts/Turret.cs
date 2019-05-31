using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15.0f; // 터렛 사거리

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab; // 총알 프리펩
    public float fireRate = 1.0f;   // 발사체 속도
    private float fireCountDown = 0.0f;

    [Header("Use Laser (default)")]
    public bool useLaser = false;   // 레이저 무기인가여?
    public LineRenderer lineRenderer;   // 레이저 라인랜더러
    public ParticleSystem impactEffect; // 레이저 파티클
    public int damageOverTime = 10;  // 레이저 초당 대미지(레이저 터렛은 총알이 없죠 ㅎㅎ)
    public float slowAmount = 0.5f;    // 느려지게 하는 수치

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";   // 적태그

    public Transform partToRotate;
    public float turnSpeed = 10.0f; // 회전속도

    public Transform firePoint; // 발사포인트

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f); // 코루틴처럼 반복함수 2번째 인자시간부터 3번째 시간주기로 반복 ㅎㅎ. 
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled) // 적없으면 라인지워야죠(레이저말입니다.)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();    // 파티클도 꺼주고
                }
            }
            return; // 타겟이 없으면 아무것도 안하죠 ㅎㅎ.
        }

        LockOnTarget(); // 타겟 락온
        if (useLaser)  // 레이저 무기일경우
        {
            Laser();
        }
        else  // 레이저 무기가 아닐경우
        {
            if (fireCountDown <= 0.0f)
            {
                Shoot();
                fireCountDown = 1.0f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        targetEnemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);   // 레이저로 조지자
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled) // 라인안그리면  그리게 해야죠
        {
            lineRenderer.enabled = true;
            impactEffect.Play();    // 파티클도 켜주고
        }
        lineRenderer.SetPosition(0, firePoint.position);    // 라인렌더러의 첫번째 인자가 나의 발사 위치에
        lineRenderer.SetPosition(1, target.position);   // 두번째는 적의 위치에 ㅇㅋ?

        Vector3 dir = firePoint.position - target.position; // 바라보는 쪽이 파이어 포인트 쪽입니다.
        impactEffect.transform.position = target.position + dir.normalized * 0.5f;  // 파티클은 적위치에서 나와야죠
        impactEffect.transform.rotation = Quaternion.LookRotation(dir); // 파티클이 파이어 포인트 쪽으로 로테이션 되야하죵
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position; // 타겟과의 위치 차이 알죠(찡긋).
        Quaternion lookRotation = Quaternion.LookRotation(dir); // 쿼터니언을 이용해서 회전시키죠.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;    // 정규화 해줍니다.
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);   // 값만큼 회전.
    }

    private void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);    // 총알 배치
        Bullet bullet = bulletGo.GetComponent<Bullet>();    // 배치한 총알 bullet 변수로 저장

        if (bullet != null)
            bullet.Seek(target);    // Bullet 클래스 함수 Seek에 타겟보내주죵.
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // enemyTag게임 오브젝트를 찾습니다.
        float shortestDistance = Mathf.Infinity;    // 가장 거리가 짧은 적인식을 위해서 변수선언을 합니다.
        GameObject neareatEnemy = null; // 가장 가까운 적..

        foreach (GameObject enemy in enemies)   // enemy에 enemies 삽입 ㅎㅎ.
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 터렛과 적의 거리를 변수로 ㅎㅎ.
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                neareatEnemy = enemy;
            }
        }
        if (neareatEnemy != null && shortestDistance <= range)
        {
            target = neareatEnemy.transform;    // 타겟을 가장 가까운 적으로.
            targetEnemy = neareatEnemy.GetComponent<Enemy>();   //   가장 가까운적을 targetEnemy로 이것은 레이저로 이동속도를 늦추게하기 위해서 설정합니다.
            // 컴포넌트를 호출해야 수정이 가능하겠죠 그래서 이렇게 하는겁니다.
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
