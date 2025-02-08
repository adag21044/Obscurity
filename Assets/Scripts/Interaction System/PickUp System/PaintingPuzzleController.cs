using UnityEngine;

public class PaintingPuzzleController : MonoBehaviour
{
    public static PaintingPuzzleController Instance;
    public Frame[] frames; // Tüm çerçeveleri tutar
    private bool puzzleCompleted = false;
    public GameObject key;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckPuzzleCompletion()
    {
        if (puzzleCompleted) return; // Zaten çözüldüyse tekrar kontrol etme

        foreach (Frame frame in frames)
        {
            if (!frame.HasCorrectPainting())
            {
                Debug.Log("❌ Puzzle henüz tamamlanmadı.");
                return;
            }
        }

        // Tüm çerçeveler doğru resme sahip, puzzle çözüldü!
        puzzleCompleted = true;
        PuzzleCompleted();
    }

    private void PuzzleCompleted()
    {
        Debug.Log("🎉 Tüm resimler doğru yerleştirildi! Puzzle çözüldü! 🎉");
        // Burada kapı açma, ışıkları açma gibi olaylar tetiklenebilir.
    }
}
