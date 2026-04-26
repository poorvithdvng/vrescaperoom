using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public CluePopup cluePopup;
    private bool clicked = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TOUCHED BY: " + other.name + " TAG: " + other.tag);
        
        if (clicked) return;
        if (other.CompareTag("Hand"))
        {
            clicked = true;
            cluePopup.OnCloseClicked();
            Invoke(nameof(ResetClick), 1f);
        }
    }

    void ResetClick()
    {
        clicked = false;
    }
}