using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRBoxController : MonoBehaviour
{
    [Header("References")]
    public Transform lid;
    public Transform keySnapPoint;
    public GameObject clueObject;

    [Header("Clue Popup")]
    public CluePopup cluePopup;          // ← ADD THIS

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip unlockSound;

    [Header("Lid Settings")]
    public float openAngle = -110f;
    public float openSpeed = 2f;

    private bool isOpening = false;
    private bool isUnlocked = false;

    void Update()
    {
        if (isOpening)
        {
            Quaternion targetRotation = Quaternion.Euler(openAngle, 0, 0);
            lid.localRotation = Quaternion.Lerp(
                lid.localRotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void InsertKey(Collider keyCollider)
    {
        if (isUnlocked) return;
        isUnlocked = true;

        Transform key = keyCollider.transform;
        key.position = keySnapPoint.position;
        key.rotation = keySnapPoint.rotation;

        var grab = key.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null) grab.enabled = false;

        if (audioSource && unlockSound)
            audioSource.PlayOneShot(unlockSound);

        Invoke(nameof(OpenBox), 0.5f);
    }

    void OpenBox()
{
    isOpening = true;
    if (clueObject != null)
        clueObject.SetActive(true);
    if (cluePopup != null)
        Invoke(nameof(TriggerClue), 1.0f);
}

void TriggerClue()
{
    cluePopup.ShowClue();
}
}