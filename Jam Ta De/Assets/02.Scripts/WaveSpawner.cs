using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0; // 죽은 수 카운트

    public Wave[] waves;

    public Transform spawnPoint;    // 스폰포인트 위치(start)

    public float timeBetweenWaves;  // 라운드 간격시간
    public float countDown;  // 시작시 카운트

    private int waveIndex = 0;  // 웨이브 인덱스

    public Text roundText;  // 라운드 텍스트
    public Text waveCountDownText;  // 웨이브 카운트 텍스트

    private void Start()
    {
        EnemiesAlive = 0;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)   // 적이 살아 있다면 리턴시킴.
        {
            //Debug.Log(EnemiesAlive);
            return;
        }

        if (countDown <= 0.0f)
        {
            StartCoroutine(SpawnWave());    // 카운트가 0이되면 웨이브 스폰 코루틴함수 이용해서 했죻.
            countDown = timeBetweenWaves;   // 라운드 간격을 위해서
            return;
        }
        countDown -= Time.deltaTime;    // 카운트 해주는거

        waveCountDownText.text = "Time : " + Mathf.Round(countDown).ToString();
    }

    IEnumerator SpawnWave() // 코루틴이죠 ㅎㅎ.
    {
        PlayerStats.Rounds++;   //   라운드 올리고
        roundText.text = "Round : " + PlayerStats.Rounds.ToString();
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.0f / wave.rate);
        }

        waveIndex++;    // 웨이브 인덱스 올리고

        if (waveIndex == waves.Length)   // 모든 웨이브가 끝나면.
        {
            Debug.Log("END");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation); // 소환
        EnemiesAlive++;
    }
}
