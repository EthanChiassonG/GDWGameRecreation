using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
public string mainLevelSceneName = "ForRealTheFInalscene";

public void StartGame()
{
    SceneManager.LoadScene(mainLevelSceneName);
}

public void QuitGame()
{
    Application.Quit();
    Debug.Log("Quit button pressed.");
}
}