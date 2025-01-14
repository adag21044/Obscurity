public interface IPuzzle 
{
    void Initialize(); // Initialize puzzle
    void Interact();
    bool CheckCompletion(); // Check if puzzle is completed
    void ResetPuzzle();
}