using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour, IPuzzle
{
    public abstract void Initialize();
    public abstract void Interact();
    public abstract bool CheckCompletion();
    public abstract void ResetPuzzle();

    protected void NotifyCompletion()
    {
        PuzzleManager.Instance.OnPuzzleCompleted(this);
    }
}