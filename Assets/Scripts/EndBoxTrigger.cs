using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBoxTrigger : MonoBehaviour
{
    public GameObject boxLid;   // top of box
    public string sceneName;    // next scene name

    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            opened = true;

            // 🔹 open box
            if (boxLid != null)
                boxLid.transform.Rotate(-90, 0, 0);

            // 🔹 load next scene after delay
            Invoke(nameof(LoadScene), 2f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}