using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 관리..

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);    // 나중에 인덱스 로 바꾸세용
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);    // 나중에 인덱스 로 바꾸세용
    }
}
