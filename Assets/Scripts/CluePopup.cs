using UnityEngine;
using System.Collections;

public class CluePopup : MonoBehaviour
{
    [Header("UI")]
    public GameObject riddleCanvas;

    [Header("Positions")]
    public Transform boxAnchor;
    public Transform playerCamera;

    [Header("Settings")]
    public float distanceFromPlayer = 1.0f;
    public float animDuration = 0.8f;

    [Header("Next Object to Reveal")]
    public GameObject objectToReveal;

    private bool isReading = false;
    private bool alreadyRevealed = false;

    void Start()
    {
        riddleCanvas.SetActive(false);
    }

    void Update()
    {
        if (!isReading) return;

        bool yPressed = false;
        bool bPressed = false;

        UnityEngine.XR.InputDevices.GetDeviceAtXRNode(
            UnityEngine.XR.XRNode.LeftHand)
            .TryGetFeatureValue(
            UnityEngine.XR.CommonUsages.secondaryButton, out yPressed);

        UnityEngine.XR.InputDevices.GetDeviceAtXRNode(
            UnityEngine.XR.XRNode.RightHand)
            .TryGetFeatureValue(
            UnityEngine.XR.CommonUsages.secondaryButton, out bPressed);

        if (yPressed || bPressed)
        {
            OnCloseClicked();
        }
    }

    public void ShowClue()
    {
        // ✅ Don't call SetActive here - already handled by VRBoxController
        isReading = false;
        alreadyRevealed = false;
        StartCoroutine(FloatOutAndShow());
    }

    public void ReopenClue()
    {
        if (isReading) return;
        isReading = true;
        riddleCanvas.SetActive(true);

        Vector3 canvasPos = playerCamera.position
                          + playerCamera.forward * distanceFromPlayer;
        riddleCanvas.transform.position = canvasPos;
        riddleCanvas.transform.rotation = Quaternion.LookRotation(
            playerCamera.position - canvasPos
        );
    }

    IEnumerator FloatOutAndShow()
    {
        Vector3 forward = playerCamera.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 targetPos = playerCamera.position
                          + forward * distanceFromPlayer
                          + Vector3.up * -0.1f;

        Vector3 startPos = boxAnchor.position;
        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / animDuration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        isReading = true;

        yield return new WaitForSeconds(0.2f);
        riddleCanvas.SetActive(true);

        Vector3 canvasPos = playerCamera.position
                          + playerCamera.forward * distanceFromPlayer;
        riddleCanvas.transform.position = canvasPos;
        riddleCanvas.transform.rotation = Quaternion.LookRotation(
            playerCamera.position - canvasPos
        );
    }

    public void OnCloseClicked()
    {
        if (!isReading) return;
        isReading = false;
        riddleCanvas.SetActive(false);
        StartCoroutine(FloatBackToBox());
    }

    IEnumerator FloatBackToBox()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / animDuration);
            transform.position = Vector3.Lerp(startPos, boxAnchor.position, t);
            yield return null;
        }

        transform.position = boxAnchor.position;

        // ✅ Reveal first
        if (!alreadyRevealed && objectToReveal != null)
        {
            alreadyRevealed = true;
            objectToReveal.SetActive(true);
            Debug.Log("REVEALED: " + objectToReveal.name);
        }

        // ✅ Then hide paper
        gameObject.SetActive(false);
    }
}