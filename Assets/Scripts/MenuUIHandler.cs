using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public Text HighScoreText;

    public void StartNew()
    {
        if (!NameIsFilledup())
            return;

        MoreOneGameInstance.Instance.Player = new Player()
        {
            Name = GetPlayerName(),
            Score = 0            
        };

        SceneManager.LoadScene(1);
    }

    private bool NameIsFilledup()
    {

        return !string.IsNullOrEmpty(GetPlayerName());
    }

    private string GetPlayerName()
    {
        var text = GameObject.Find("NameText").GetComponent<Text>();

        return text.text;
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
}
