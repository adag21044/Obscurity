using UnityEngine;

public class PaintingPuzzleController : MonoBehaviour
{
    [SerializeField] private Frame[] frames;
    [SerializeField] private GameObject key;

    private bool puzzleCompleted;

    private void Awake()
    {
        // Subscribe to every frame’s event
        foreach (var frame in frames)
            frame.OnPaintingChanged += HandleFrameChanged;
    }

    private void Start()
    {
        // Start aşamasında sahne yüklenir yüklenmez puzzle durumu kontrol edilsin
        HandleFrameChanged(null);
    }


    private void HandleFrameChanged(Frame changedFrame)
    {
        if (puzzleCompleted) return;

        CheckPuzzleCompletion();
    }

    private void CheckPuzzleCompletion()
    {
        foreach (var frame in frames)
        {
            if (!frame.HasCorrectPainting())
            {
                Debug.Log("[Puzzle] Not solved yet.");
                return;
            }
        }

        puzzleCompleted = true;
        CompletePuzzle();
    }


    private void CompletePuzzle()
    {
        Debug.Log("🎉 All paintings are in the right place! Puzzle solved!");
        key.SetActive(true);
    }
}
