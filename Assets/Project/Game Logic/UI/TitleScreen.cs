using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Tags.Scenes.PlayerRegistration); // skipping main menu for now as it doesnt do anything different from title screen
    }
    public void Play()
    {
        SceneManager.LoadScene(Tags.Scenes.PlayerRegistration);
    }
    public void Options()
    {
        SceneManager.LoadScene(Tags.Scenes.Options);
    }
}
