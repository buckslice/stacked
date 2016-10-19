using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Tags.Scenes.MainMenu);
    }
    public void Play()
    {
        SceneManager.LoadScene(2);
    }
    public void Options()
    {
        SceneManager.LoadScene(Tags.Scenes.Options);
    }
}
