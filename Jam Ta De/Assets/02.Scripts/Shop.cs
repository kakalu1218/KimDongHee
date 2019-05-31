using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;  // 일반 터렛
    public TurretBluePrint missileLauncher; // 미사일 터렛
    public TurretBluePrint laserBeamer; // 레이저 터렛

    BuildManager buildManager;

    private void Update()
    {
        if (Input.GetKeyDown("z")) UpgradeStandardTurret();
        if (Input.GetKeyDown("x")) UpgradeMissileLauncher();
        if (Input.GetKeyDown("c")) UpgradeLaserBeamer();
        if (Input.GetKeyDown("v")) SelectBuildTurret();
    }

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void UpgradeStandardTurret()
    {
        //buildManager.SelectTurretToBuild(standardTurret);   // 빌드메니저에 스탠다드 터렛인자 전송
    }

    public void UpgradeMissileLauncher()
    {
        //buildManager.SelectTurretToBuild(missileLauncher);  // 빌드메니저에 미사일 터렛인자 전송
    }

    public void UpgradeLaserBeamer()
    {
        //buildManager.SelectTurretToBuild(laserBeamer);  // 빌드메니저에 레이저 터렛인자 전송
    }

    public void SelectBuildTurret()
    {
        int val = Random.Range(0, 3);
        switch (val)
        {
            case 0:
                buildManager.SelectTurretToBuild(standardTurret);
                break;
            case 1:
                buildManager.SelectTurretToBuild(missileLauncher);
                break;
            case 2:
                buildManager.SelectTurretToBuild(laserBeamer);
                break;
        }
    }
}
