using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text messageText; // Genel mesaj gösterimi
    [SerializeField] private List<Image> keyIcons; // Anahtar simgelerini tutan liste

    private bool isMessageActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Display a temporary message
    public void DisplayMessage(string message, float duration = 2f)
    {
        if (isMessageActive) return;

        isMessageActive = true;
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        Invoke(nameof(ClearMessage), duration);
    }

    private void ClearMessage()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);
        isMessageActive = false;
    }

    // Update the UI to display collected keys
    public void UpdateKeyIconsUI()
    {
        // Tüm anahtar simgelerini başlangıçta kapat
        foreach (var icon in keyIcons)
        {
            icon.gameObject.SetActive(false);
        }

        // Toplanan anahtarların tag'ine göre simgeleri aç
        List<string> collectedKeys = KeyManager.Instance.GetAllKeys();

        for (int i = 0; i < collectedKeys.Count; i++)
        {
            // Her anahtar için sıradaki simgeyi aktif et
            if (i < keyIcons.Count)
            {
                keyIcons[i].gameObject.SetActive(true);
            }
        }
    }
}
