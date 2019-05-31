using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;    // 싱글턴

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    private TurretBluePrint turretToBuild;  //  만들터렛 shop에서 인자 받아와서 설정되죠.
    private Node selectedNode;  // 노드선택시 노드 할당..

    public NodeUI nodeUI;

    public GameObject buildEffect;
    public GameObject sellEffect;

    public bool CanBuild { get { return turretToBuild != null; } }  // 빌드가능할경우
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }  // 빌드가능한 돈이있는경우

    public void SelectTurretToBuild(TurretBluePrint turret) // shop에서 받아옴..
    {
        turretToBuild = turret;
        DeselectNode();
    }

    // 스탠다드 터렛업그레이드 블루 프린트 2 ~ 5
    public TurretBluePrint standardTurretLevel02;
    public TurretBluePrint standardTurretLevel03;
    public TurretBluePrint standardTurretLevel04;
    public TurretBluePrint standardTurretLevel05;
    // 미사일 터렛업그레이드 블루 프린트 2 ~ 5
    public TurretBluePrint missileTurretLevel02;
    public TurretBluePrint missileTurretLevel03;
    public TurretBluePrint missileTurretLevel04;
    public TurretBluePrint missileTurretLevel05;
    // 레이저 터렛업그레이드 블루 프린트 2 ~ 5
    public TurretBluePrint laserTurretLevel02;
    public TurretBluePrint laserTurretLevel03;
    public TurretBluePrint laserTurretLevel04;
    public TurretBluePrint laserTurretLevel05;

    // 스탠다드 3조합 업글을 위한 조합 카운트 1 ~ 4
    private int standardTurretLevel01count = 0;
    private int standardTurretLevel02count = 0;
    private int standardTurretLevel03count = 0;
    private int standardTurretLevel04count = 0;
    // 미사일 3조합 업글을 위한 조합 카운트 1 ~ 4
    private int missileTurretLevel01count = 0;
    private int missileTurretLevel02count = 0;
    private int missileTurretLevel03count = 0;
    private int missileTurretLevel04count = 0;
    // 레이저 3조합 업글을 위한 조합 카운트 1 ~ 4
    private int laserTurretLevel01count = 0;
    private int laserTurretLevel02count = 0;
    private int laserTurretLevel03count = 0;
    private int laserTurretLevel04count = 0;

    // 스탠다드 조합할 터렛이 설치된 노드들의 정보를 저장할 노드 배열..
    private Node[] standardTurretLevel01Nodes;
    private Node[] standardTurretLevel02Nodes;
    private Node[] standardTurretLevel03Nodes;
    private Node[] standardTurretLevel04Nodes;
    // 미사일 조합할 터렛이 설치된 노드들의 정보를 저장할 노드 배열..
    private Node[] missileTurretLevel01Nodes;
    private Node[] missileTurretLevel02Nodes;
    private Node[] missileTurretLevel03Nodes;
    private Node[] missileTurretLevel04Nodes;
    // 레이저 조합할 터렛이 설치된 노드들의 정보를 저장할 노드 배열..
    private Node[] laserTurretLevel01Nodes;
    private Node[] laserTurretLevel02Nodes;
    private Node[] laserTurretLevel03Nodes;
    private Node[] laserTurretLevel04Nodes;

    public void Start()
    {
        // 노드 배열 초기화.
        standardTurretLevel01Nodes = new Node[3];
        standardTurretLevel02Nodes = new Node[3];
        standardTurretLevel03Nodes = new Node[3];
        standardTurretLevel04Nodes = new Node[3];

        missileTurretLevel01Nodes = new Node[3];
        missileTurretLevel02Nodes = new Node[3];
        missileTurretLevel03Nodes = new Node[3];
        missileTurretLevel04Nodes = new Node[3];

        laserTurretLevel01Nodes = new Node[3];
        laserTurretLevel02Nodes = new Node[3];
        laserTurretLevel03Nodes = new Node[3];
        laserTurretLevel04Nodes = new Node[3];
    }

    public void checkNode(Node node)    // 3조합을 위한 노드 정보를 받아옵니다. 터렛이 설치되면 여기로 정보가 쓩~~ 하고 날라 오겠죵.
    {
        // 스탠다드 터렛 조합 부분.
        if (node.turret.tag == "StandardTurretLevel01")
        {
            standardTurretLevel01Nodes[standardTurretLevel01count] = node;
            standardTurretLevel01count++;
        }

        if (standardTurretLevel01count == 3)
        {
            StandardUpgradeLevel02(node);
        }

        if (standardTurretLevel02count == 3)
        {
            StandardUpgradeLevel03(node);
        }

        if (standardTurretLevel03count == 3)
        {
            StandardUpgradeLevel04(node);
        }

        if (standardTurretLevel04count == 3)
        {
            StandardUpgradeLevel05(node);
        }

        // 미사일 터렛 조합 부분.
        if (node.turret.tag == "MissileLauncherLevel01")
        {
            missileTurretLevel01Nodes[missileTurretLevel01count] = node;
            missileTurretLevel01count++;
        }

        if (missileTurretLevel01count == 3)
        {
            MissileUpgradeLevel02(node);
        }

        if (missileTurretLevel02count == 3)
        {
            MissileUpgradeLevel03(node);
        }

        if (missileTurretLevel03count == 3)
        {
            MissileUpgradeLevel04(node);
        }

        if (missileTurretLevel04count == 3)
        {
            MissileUpgradeLevel05(node);
        }

        // 레이저 터렛 조합 부분.
        if (node.turret.tag == "LaserBeamerLevel01")
        {
            laserTurretLevel01Nodes[laserTurretLevel01count] = node;
            laserTurretLevel01count++;
        }

        if (laserTurretLevel01count == 3)
        {
            LaserUpgradeLevel02(node);
        }

        if (laserTurretLevel02count == 3)
        {
            LaserUpgradeLevel03(node);
        }

        if (laserTurretLevel03count == 3)
        {
            LaserUpgradeLevel04(node);
        }

        if (laserTurretLevel04count == 3)
        {
            LaserUpgradeLevel05(node);
        }
    }

    void StandardUpgradeLevel02(Node node)  // 스탠다드 2Level 터렛 으로 바꾸는 작업
    {
        Destroy(standardTurretLevel01Nodes[0].turret);
        standardTurretLevel01Nodes[0].turretBluePrint = null;
        Destroy(standardTurretLevel01Nodes[1].turret);
        standardTurretLevel01Nodes[1].turretBluePrint = null;
        Destroy(standardTurretLevel01Nodes[2].turret);
        standardTurretLevel01Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(standardTurretLevel02);
        standardTurretLevel01count = 0;

        standardTurretLevel02Nodes[standardTurretLevel02count] = standardTurretLevel01Nodes[2];

        standardTurretLevel01Nodes[0] = null;
        standardTurretLevel01Nodes[1] = null;
        standardTurretLevel01Nodes[2] = null;

        standardTurretLevel02count++;
    }
    void StandardUpgradeLevel03(Node node)  // 스탠다드 3Level 터렛 으로 바꾸는 작업
    {
        Destroy(standardTurretLevel02Nodes[0].turret);
        standardTurretLevel02Nodes[0].turretBluePrint = null;
        Destroy(standardTurretLevel02Nodes[1].turret);
        standardTurretLevel02Nodes[1].turretBluePrint = null;
        Destroy(standardTurretLevel02Nodes[2].turret);
        standardTurretLevel02Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(standardTurretLevel03);
        standardTurretLevel02count = 0;

        standardTurretLevel03Nodes[standardTurretLevel03count] = standardTurretLevel02Nodes[2];

        standardTurretLevel02Nodes[0] = null;
        standardTurretLevel02Nodes[1] = null;
        standardTurretLevel02Nodes[2] = null;

        standardTurretLevel03count++;
    }
    void StandardUpgradeLevel04(Node node)  // 스탠다드 4Level 터렛 으로 바꾸는 작업
    {
        Destroy(standardTurretLevel03Nodes[0].turret);
        standardTurretLevel03Nodes[0].turretBluePrint = null;
        Destroy(standardTurretLevel03Nodes[1].turret);
        standardTurretLevel03Nodes[1].turretBluePrint = null;
        Destroy(standardTurretLevel03Nodes[2].turret);
        standardTurretLevel03Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(standardTurretLevel04);
        standardTurretLevel03count = 0;

        standardTurretLevel04Nodes[standardTurretLevel04count] = standardTurretLevel03Nodes[2];

        standardTurretLevel03Nodes[0] = null;
        standardTurretLevel03Nodes[1] = null;
        standardTurretLevel03Nodes[2] = null;

        standardTurretLevel04count++;
    }
    void StandardUpgradeLevel05(Node node)  // 스탠다드 5Level 터렛 으로 바꾸는 작업
    {
        Destroy(standardTurretLevel04Nodes[0].turret);
        standardTurretLevel04Nodes[0].turretBluePrint = null;
        Destroy(standardTurretLevel04Nodes[1].turret);
        standardTurretLevel04Nodes[1].turretBluePrint = null;
        Destroy(standardTurretLevel04Nodes[2].turret);
        standardTurretLevel04Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(standardTurretLevel05);
        standardTurretLevel04count = 0;

        standardTurretLevel04Nodes[0] = null;
        standardTurretLevel04Nodes[1] = null;
        standardTurretLevel04Nodes[2] = null;
    }

    void MissileUpgradeLevel02(Node node)  // 미사일 2Level 터렛 으로 바꾸는 작업
    {
        Destroy(missileTurretLevel01Nodes[0].turret);
        missileTurretLevel01Nodes[0].turretBluePrint = null;
        Destroy(missileTurretLevel01Nodes[1].turret);
        missileTurretLevel01Nodes[1].turretBluePrint = null;
        Destroy(missileTurretLevel01Nodes[2].turret);
        missileTurretLevel01Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(missileTurretLevel02);
        missileTurretLevel01count = 0;

        missileTurretLevel02Nodes[missileTurretLevel02count] = missileTurretLevel01Nodes[2];

        missileTurretLevel01Nodes[0] = null;
        missileTurretLevel01Nodes[1] = null;
        missileTurretLevel01Nodes[2] = null;

        missileTurretLevel02count++;
    }
    void MissileUpgradeLevel03(Node node)  // 미사일 3Level 터렛 으로 바꾸는 작업
    {
        Destroy(missileTurretLevel02Nodes[0].turret);
        missileTurretLevel02Nodes[0].turretBluePrint = null;
        Destroy(missileTurretLevel02Nodes[1].turret);
        missileTurretLevel02Nodes[1].turretBluePrint = null;
        Destroy(missileTurretLevel02Nodes[2].turret);
        missileTurretLevel02Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(missileTurretLevel03);
        missileTurretLevel02count = 0;

        missileTurretLevel03Nodes[missileTurretLevel03count] = missileTurretLevel02Nodes[2];

        missileTurretLevel02Nodes[0] = null;
        missileTurretLevel02Nodes[1] = null;
        missileTurretLevel02Nodes[2] = null;

        missileTurretLevel03count++;
    }
    void MissileUpgradeLevel04(Node node)  // 미사일 4Level 터렛 으로 바꾸는 작업
    {
        Destroy(missileTurretLevel03Nodes[0].turret);
        missileTurretLevel03Nodes[0].turretBluePrint = null;
        Destroy(missileTurretLevel03Nodes[1].turret);
        missileTurretLevel03Nodes[1].turretBluePrint = null;
        Destroy(missileTurretLevel03Nodes[2].turret);
        missileTurretLevel03Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(missileTurretLevel04);
        missileTurretLevel03count = 0;

        missileTurretLevel04Nodes[missileTurretLevel04count] = missileTurretLevel03Nodes[2];

        missileTurretLevel03Nodes[0] = null;
        missileTurretLevel03Nodes[1] = null;
        missileTurretLevel03Nodes[2] = null;

        missileTurretLevel04count++;
    }
    void MissileUpgradeLevel05(Node node)  // 미사일 5Level 터렛 으로 바꾸는 작업
    {
        Destroy(missileTurretLevel04Nodes[0].turret);
        missileTurretLevel04Nodes[0].turretBluePrint = null;
        Destroy(missileTurretLevel04Nodes[1].turret);
        missileTurretLevel04Nodes[1].turretBluePrint = null;
        Destroy(missileTurretLevel04Nodes[2].turret);
        missileTurretLevel04Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(missileTurretLevel05);
        missileTurretLevel04count = 0;

        missileTurretLevel04Nodes[0] = null;
        missileTurretLevel04Nodes[1] = null;
        missileTurretLevel04Nodes[2] = null;
    }

    void LaserUpgradeLevel02(Node node)  // 레이저 2Level 터렛 으로 바꾸는 작업
    {
        Destroy(laserTurretLevel01Nodes[0].turret);
        laserTurretLevel01Nodes[0].turretBluePrint = null;
        Destroy(laserTurretLevel01Nodes[1].turret);
        laserTurretLevel01Nodes[1].turretBluePrint = null;
        Destroy(laserTurretLevel01Nodes[2].turret);
        laserTurretLevel01Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(laserTurretLevel02);
        laserTurretLevel01count = 0;

        laserTurretLevel02Nodes[laserTurretLevel02count] = laserTurretLevel01Nodes[2];

        laserTurretLevel01Nodes[0] = null;
        laserTurretLevel01Nodes[1] = null;
        laserTurretLevel01Nodes[2] = null;

        laserTurretLevel02count++;
    }
    void LaserUpgradeLevel03(Node node)  // 레이저 3Level 터렛 으로 바꾸는 작업
    {
        Destroy(laserTurretLevel02Nodes[0].turret);
        laserTurretLevel02Nodes[0].turretBluePrint = null;
        Destroy(laserTurretLevel02Nodes[1].turret);
        laserTurretLevel02Nodes[1].turretBluePrint = null;
        Destroy(laserTurretLevel02Nodes[2].turret);
        laserTurretLevel02Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(laserTurretLevel03);
        laserTurretLevel02count = 0;

        laserTurretLevel03Nodes[laserTurretLevel03count] = laserTurretLevel02Nodes[2];

        laserTurretLevel02Nodes[0] = null;
        laserTurretLevel02Nodes[1] = null;
        laserTurretLevel02Nodes[2] = null;

        laserTurretLevel03count++;
    }
    void LaserUpgradeLevel04(Node node)  // 레이저 4Level 터렛 으로 바꾸는 작업
    {
        Destroy(laserTurretLevel03Nodes[0].turret);
        laserTurretLevel03Nodes[0].turretBluePrint = null;
        Destroy(laserTurretLevel03Nodes[1].turret);
        laserTurretLevel03Nodes[1].turretBluePrint = null;
        Destroy(laserTurretLevel03Nodes[2].turret);
        laserTurretLevel03Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(laserTurretLevel04);
        laserTurretLevel03count = 0;

        laserTurretLevel04Nodes[laserTurretLevel04count] = laserTurretLevel03Nodes[2];

        laserTurretLevel03Nodes[0] = null;
        laserTurretLevel03Nodes[1] = null;
        laserTurretLevel03Nodes[2] = null;

        laserTurretLevel04count++;
    }
    void LaserUpgradeLevel05(Node node)  // 레이저 5Level 터렛 으로 바꾸는 작업
    {
        Destroy(laserTurretLevel04Nodes[0].turret);
        laserTurretLevel04Nodes[0].turretBluePrint = null;
        Destroy(laserTurretLevel04Nodes[1].turret);
        laserTurretLevel04Nodes[1].turretBluePrint = null;
        Destroy(laserTurretLevel04Nodes[2].turret);
        laserTurretLevel04Nodes[2].turretBluePrint = null;
        node.UpgradeTurret(laserTurretLevel05);
        laserTurretLevel04count = 0;

        laserTurretLevel04Nodes[0] = null;
        laserTurretLevel04Nodes[1] = null;
        laserTurretLevel04Nodes[2] = null;
    }

    public void SelectNode(Node node)   // 노트클릭시.
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()  // 노드 클릭해제(ui쪽)
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void ReSetTurret()   // 설치후 turretToBuild(터렛) 초기화.
    {
        turretToBuild = null;
    }
}
