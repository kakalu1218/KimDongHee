using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 50.0f; // 총알 속도
    public float damage = 10.0f; // 총알 데미지
    public float explosionRadius = 0.0f;    // 폭파범위
    public GameObject impactEffect; // 파티클

    public void Seek(Transform _target) // 터렛에서 타겟인자를 받아왔죠
    {
        target = _target;   // 타겟으로 저장
    }

    private void Update()
    {
        if (target == null) // 타겟이 없으면 총알 없에죵
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceTimeFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceTimeFrame) // 벡터 길이 비교해서 (총알속도와 거리를 비교)
        {
            HitTarget();    // 총알이 맞았다 이말이야
            return;
        }
        transform.Translate(dir.normalized * distanceTimeFrame, Space.World);   // 총알 날라감..
        transform.LookAt(target);   // 목표를 바라보게.
    }

    private void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);   // 파티클 임팩트
        Destroy(effectIns, 2.0f);   // 2초뒤 삭제

        if (explosionRadius > 0.0f)
        {
            Explode();  // 폭파범위가 있는 총알이면 요기.
        }
        else
        {
            Damage(target); // 아니면 요기.
        }
        Destroy(gameObject);    // 총알도 삭제
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);  // 폭파범위에있는 적을 콜라이더에 삽입
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")    // 태그가 "Enemy"인 트랜포 점보를 댐지에 보내버리죵.
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);   // 적에게 대미지 전달..
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
