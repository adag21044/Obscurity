using UnityEngine;

public class Check : MonoBehaviour
{
    public static Check Instance { get; private set; }

    public Frame[] allFrames;
    public bool isPuzzleSolved { get; private set; } = false;

    public bool isKeyActive = false;
    public GameObject key;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CheckPuzzle()
    {
        if (isPuzzleSolved) return; // already solved

        foreach (var frame in allFrames)
        {
            if (!frame.HasCorrectPainting())
            {
                Debug.Log("🧩 Puzzle is not solved yet.");
                return;
            }
        }

        isPuzzleSolved = true;
        Debug.Log("🎉 Puzzle solved!");
        // Add reward, animation, or door unlock etc.
        isKeyActive = true; // Set the key active when puzzle is solved

        if(isKeyActive)
        {
            key.SetActive(true); // Activate the key GameObject
            Debug.Log("Key is now active!");
        }
    }
}
