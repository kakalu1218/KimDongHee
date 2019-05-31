using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text sellAmount;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition(); // 해당노드(타겟) 위치에..
       
        sellAmount.text = "$ " + target.turretBluePrint.GetSellAmount();
        ui.SetActive(true);
    }

    public void Hide()  // UI숨기기
    {
        ui.SetActive(false);
    }

    public void Sell()  // 터렛 팔기
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();   // 노드 선택 해제
    }
}
