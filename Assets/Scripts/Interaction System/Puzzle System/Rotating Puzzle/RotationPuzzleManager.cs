using UnityEngine;

public class RotationPuzzleManager : MonoBehaviour
{
    public static RotationPuzzleManager Instance;

    private RotatingPuzzlePiece[] puzzlePieces;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        puzzlePieces = FindObjectsOfType<RotatingPuzzlePiece>();
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var piece in puzzlePieces)
        {
            if (!piece.IsInCorrectRotation())
            {
                Debug.Log($"❌ {piece.name} not in correct rotation");
                return;
            }
        }

        Debug.Log("✅ Rotation Puzzle Completed!");
        LockAllPieces();
    }


    private void LockAllPieces()
    {
        foreach (var piece in puzzlePieces)
        {
            piece.LockPiece();
        }
    }
}