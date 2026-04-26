using UnityEngine;

public class DoorKeyTrigger : MonoBehaviour
{
    public DoorOpen door;
    public Transform snapPoint;   // ✅ THIS SHOULD BE HERE (INSIDE CLASS)

    private void OnTriggerEnter(Collider other)   // ✅ private is valid HERE
    {
        if (other.CompareTag("Key"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // SNAP
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;
            other.transform.SetParent(snapPoint);

            door.OpenDoor();

            var grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab != null) grab.enabled = false;
        }
    }
}