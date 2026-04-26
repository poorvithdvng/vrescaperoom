using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    // You can call this function from your Button component
    public void GoToLevelTwo()
    {
        // Option 1: Load by the specific scene name (Type the exact name of your scene)
        SceneManager.LoadSceneAsync("4 - abandoned");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(3);
    }
}