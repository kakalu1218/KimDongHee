using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0.0f;  // 시간아 멈추어다오....
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(1);    // 나중에 인덱스 로 바꾸세용
    }

    public void Menu()
    {
        Toggle();
        SceneManager.LoadScene(0);    // 나중에 인덱스 로 바꾸세용
    }
}
