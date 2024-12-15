using UnityEngine;
using System;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    private List<string> collectedKeys = new List<string>();

    // Event for notifying UI updates
    public event Action OnKeyUpdated;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Collect a key and store its tag
    public void CollectKey(string keyTag)
    {
        if (!collectedKeys.Contains(keyTag))
        {
            collectedKeys.Add(keyTag);
            Debug.Log($"Key collected: {keyTag}");

            // UI'yi güncelle
            UIManager.Instance.UpdateKeyIconsUI();

            // Display message
            UIManager.Instance.DisplayMessage($"You found {keyTag}!", 2f);

            OnKeyUpdated?.Invoke();
        }
    }


    // Check if the player has a specific key
    public bool HasKey(string keyTag)
    {
        return collectedKeys.Contains(keyTag);
    }

    // Return all collected keys
    public List<string> GetAllKeys()
    {
        return collectedKeys;
    }
}
