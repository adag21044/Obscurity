using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages the collection and tracking of keys.
/// </summary>
public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { get; private set; }

    private readonly List<string> collectedKeys = new List<string>();

    /// <summary>
    /// Event triggered when a key is collected.
    /// </summary>
    public event Action OnKeyCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optional: Make this object persistent across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Collects a key and adds it to the collection.
    /// </summary>
    /// <param name="keyTag">The tag of the collected key.</param>
    public void CollectKey(string keyTag)
    {
        if (!collectedKeys.Contains(keyTag))
        {
            collectedKeys.Add(keyTag);
            Debug.Log($"Key collected: {keyTag}");

            // Update UI
            UIManager.Instance.UpdateKeyIconsUI();

            // Display message
            UIManager.Instance.DisplayMessage($"You found a {keyTag}!", 2f);

            // Invoke event
            OnKeyCollected?.Invoke();
        }
        else
        {
            Debug.Log($"Key already collected: {keyTag}");
        }
    }

    /// <summary>
    /// Checks if the player has a specific key.
    /// </summary>
    /// <param name="keyTag">The tag of the key.</param>
    /// <returns>True if the key is collected; otherwise, false.</returns>
    public bool HasKey(string keyTag)
    {
        return collectedKeys.Contains(keyTag);
    }

    /// <summary>
    /// Gets all collected keys.
    /// </summary>
    /// <returns>A list of collected key tags.</returns>
    public List<string> GetAllKeys()
    {
        return new List<string>(collectedKeys);
    }
}
