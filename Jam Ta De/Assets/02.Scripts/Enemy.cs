using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10.0f;    // 시작 이동속도
    [HideInInspector]   // 변수 인스펙터를 숨김..
    public float speed = 10.0f;  // 이동속도

    public float startHealth = 100.0f;    // 적 체력
    private float health;
    private bool die;

    public int value = 10;

    public GameObject deathEffect;  // 죽을시 파티클.

    public float turnSpeed = 10.0f; // 회전속도

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        die = false;
    }

    private void Update()
    {
        if (die)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)  // 데미지 받으면..(bullet 클래스에서 옵니다)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            die = true;
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1.0f - amount);    // amount 퍼센트 만큼 느려지죵..
    }

    private void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);  // 파티클
        Destroy(effect, 3.0f);  // 파티클
        WaveSpawner.EnemiesAlive--; // 웨이브 스폰에서 사용(죽은 수 카운트)
        //Debug.Log("AA" + WaveSpawner.EnemiesAlive);
        PlayerStats.Money += value; // 웨이브 보상
        Destroy(gameObject);    // 죽습니다..
    }
}
