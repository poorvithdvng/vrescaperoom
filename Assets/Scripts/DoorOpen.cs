using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public GameObject clue;

    public Transform player;          // XR Rig
    public Transform teleportPoint;   // destination

    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (opened) return;

        opened = true;

        // 🔹 open door
        door.transform.Rotate(0, 90, 0);

        // 🔹 teleport after delay
        Invoke(nameof(TeleportPlayer), 1.5f);
    }

    void TeleportPlayer()
    {
        // 🔹 XR-safe teleport (keeps head position correct)
        Vector3 offset = player.position - Camera.main.transform.position;
        player.position = teleportPoint.position + offset;
    }
}