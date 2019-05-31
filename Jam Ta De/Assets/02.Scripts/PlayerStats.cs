using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;    // 돈입니다. 전역선언했죠 초기화 안되니까이 믿에 스타트 머니 있죠..
    public int startMoney = 300;

    public static int Lives;    // 목숨입니다. 전역선언했죠 초기화 안되니까이 믿에 스타트 라이브즈 있죠..
    public int startLives = 20;

    public static int Rounds;   // 라운드

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
}
