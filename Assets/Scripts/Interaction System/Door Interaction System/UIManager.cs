using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages UI elements, such as displaying messages and key icons.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Text element for displaying messages.")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Tooltip("List of key icon images to represent collected keys.")]
    [SerializeField] private List<Image> keyIcons;

    private bool isMessageActive = false;

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

        if (messageText == null)
        {
            Debug.LogError("UIManager: Message Text is not assigned.");
        }

        if (keyIcons == null || keyIcons.Count == 0)
        {
            Debug.LogError("UIManager: Key Icons are not assigned or empty.");
        }
    }

    /// <summary>
    /// Displays a temporary message on the screen.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">Duration in seconds to display the message.</param>
    public void DisplayMessage(string message, float duration = 2f)
    {
        if (isMessageActive)
            return;

        if (messageText == null)
            return;

        StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    /// <summary>
    /// Coroutine to handle displaying messages for a set duration.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">Duration in seconds to display the message.</param>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator<WaitForSeconds> DisplayMessageCoroutine(string message, float duration)
    {
        isMessageActive = true;
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        ClearMessage();
    }

    /// <summary>
    /// Clears the currently displayed message.
    /// </summary>
    private void ClearMessage()
    {
        if (messageText == null)
            return;

        messageText.text = string.Empty;
        messageText.gameObject.SetActive(false);
        isMessageActive = false;
    }

    /// <summary>
    /// Updates the UI to reflect collected keys.
    /// </summary>
    public void UpdateKeyIconsUI()
    {
        if (keyIcons == null || keyIcons.Count == 0)
            return;

        List<string> collectedKeys = KeyManager.Instance.GetAllKeys();

        for (int i = 0; i < keyIcons.Count; i++)
        {
            if (i < collectedKeys.Count)
            {
                keyIcons[i].gameObject.SetActive(true);
                // Optionally, set the sprite based on the key
                // keyIcons[i].sprite = GetSpriteForKey(collectedKeys[i]);
            }
            else
            {
                keyIcons[i].gameObject.SetActive(false);
            }
        }
    }

    // Optional: Method to get the sprite based on key tag
    /*
    private Sprite GetSpriteForKey(string keyTag)
    {
        // Implement logic to return the appropriate sprite based on keyTag
    }
    */
}
