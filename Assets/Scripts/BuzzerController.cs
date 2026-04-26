using UnityEngine;
using UnityEngine.SceneManagement;

public class BuzzerController : MonoBehaviour
{
    public int congratsSceneIndex = 3;
    private bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BUZZER HIT BY: " + other.name + " TAG: " + other.tag);
        
        if (pressed) return;
        if (other.CompareTag("Hand") ||
            other.CompareTag("Player") ||
            other.name.Contains("Hand") ||
            other.name.Contains("Controller"))
        {
            pressed = true;
            Debug.Log("BUZZER PRESSED - Loading scene " + congratsSceneIndex);
            SceneManager.LoadScene(congratsSceneIndex);
        }
    }
}