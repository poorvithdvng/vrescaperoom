using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject key;

    public bool condition1;
    public bool condition2;
    public bool condition3;

    private bool activated = false;

    void Update()
    {
        if (!activated && condition1 && condition2 && condition3)
        {
            activated = true;
            key.SetActive(true);
        }
    }
}