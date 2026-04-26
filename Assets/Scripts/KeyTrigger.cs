using UnityEngine;

public class KeyTriggerZone : MonoBehaviour
{
    public VRBoxController box;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            box.InsertKey(other);
        }
    }
}