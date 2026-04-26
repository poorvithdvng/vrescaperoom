using UnityEngine;

public class ReopenClue : MonoBehaviour
{
    public CluePopup cluePopup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cluePopup.ReopenClue();
    }
}