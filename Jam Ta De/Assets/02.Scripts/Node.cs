using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;    // 마우스찍었을시 변환될 칼라
    public Color notEnoughMoneyColor;   // 돈이 없을시 변환될 카라

    public Vector3 positionOffset;  // 터렛위치를 올려주려공..

    [HideInInspector]
    public GameObject turret;  // 터렛 오브젝
    [HideInInspector]
    public TurretBluePrint turretBluePrint; // 터렛 블루 프린트

    private Renderer rend;  // 컴퍼넌트 접을 위한 변수
    private Color startColor;   // 기본칼라

    BuildManager buildManager;  // 빌드메니저

    private void Start()
    {
        rend = GetComponent<Renderer>();    // 컴퍼넌트 설정
        startColor = rend.material.color;

        buildManager = BuildManager.instance; //빌드메니저 인스턴스
    }

    public Vector3 GetBuildPosition()   // 터렛만들 해당 노드의 위치
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // 마우스 클릭 이벤트가 겹치지 않게하기위해서.
        if (turret != null)
        {
            buildManager.SelectNode(this);  // 터렛이있는 노드를 클릭시 해당 노드를 넘기죠.터렛ui를 위해서
            return;
        }
        if (!buildManager.CanBuild) return; // 터렛 만들수 없을시..
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // 마우스 클릭 이벤트가 겹치지 않게하기위해서.
        if (!buildManager.CanBuild) return; // 터렛 만들수 없을시..
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor; 
    }

    private void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            return;
        }
        PlayerStats.Money -= bluePrint.cost;
        
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);    // Quaternion.identity(회전없음 을 의미..)
        turret = _turret;
        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3.0f);

        buildManager.checkNode(this);   // 3조합 업그레이드 할 노드 체크를 위해서 노드를 빌드매니저로 보냅니다.
        buildManager.ReSetTurret(); // 타워를 랜덤하게 설치하기 위해서 터렛정보를 초기화 합니다.
    }

    public void SellTurret()    // 터렛 팔기
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount(); // 터렛 팔기

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3.0f);

        Destroy(turret);
        turretBluePrint = null;
    }

    public void UpgradeTurret(TurretBluePrint bluePrint)    // 3조합 업그레이드 시..
    {
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3.0f);
    }
}
