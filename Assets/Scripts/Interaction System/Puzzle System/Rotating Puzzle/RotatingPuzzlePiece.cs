using UnityEngine;

public class RotatingPuzzlePiece : MonoBehaviour, IInteractable
{
    public RightRotation correctRotation;  // Target rotation data (ScriptableObject)
    private bool isLocked = false;         // Lock after puzzle is completed


    public string GetDescription()
    {
        return isLocked ? "" : "Press E to rotate the piece!";
    }

    public void Interact()
    {
        if (isLocked) return;

        transform.Rotate(0, 90, 0);
        RotationPuzzleManager.Instance.CheckPuzzleCompletion(); // Notify manager after rotation
    }

    public bool IsInCorrectRotation()
    {
        const float tol = 1f;                             // ±1° tolerans

        float currentX = transform.localEulerAngles.x % 360f;
        float targetX  = correctRotation.rotationX  % 360f;

        bool xMatch = Mathf.Abs(Mathf.DeltaAngle(currentX, targetX)) < tol;

        Debug.Log($"{name}: X = {currentX:0.#}°  ➜ hedef {targetX}°  ⇒  {xMatch}");
        return xMatch;                                    // ⬅️  SADECE X
    }



    public void LockPiece()
    {
        isLocked = true;
    }
}