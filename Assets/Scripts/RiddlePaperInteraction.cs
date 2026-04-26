using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RiddlePaperInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject riddleCanvas;
    public Transform tableAnchor;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    // ✅ PASTE THE NEW OnGrab HERE — replaces the old one
    void OnGrab(SelectEnterEventArgs args)
    {
        riddleCanvas.SetActive(true);

        // Makes the canvas appear in front of the player's face
        Camera cam = Camera.main;
        riddleCanvas.transform.position = cam.transform.position + cam.transform.forward * 0.5f;
        riddleCanvas.transform.rotation = Quaternion.LookRotation(
            riddleCanvas.transform.position - cam.transform.position
        );
    }

    void OnRelease(SelectExitEventArgs args)
    {
        riddleCanvas.SetActive(false);
        StartCoroutine(SnapBackToTable());
    }

    System.Collections.IEnumerator SnapBackToTable()
    {
        yield return new WaitForSeconds(0.5f);

        if (tableAnchor != null)
        {
            transform.position = tableAnchor.position;
            transform.rotation = tableAnchor.rotation;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}