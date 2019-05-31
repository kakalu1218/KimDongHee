using UnityEngine;

[System.Serializable]   // 직렬화..
public class TurretBluePrint
{
    public GameObject prefab;   // 터렛 프리팹
    public int cost;    // 가격

    public int GetSellAmount()  // 팔때 가격
    {
        return cost / 2;
    }
}
