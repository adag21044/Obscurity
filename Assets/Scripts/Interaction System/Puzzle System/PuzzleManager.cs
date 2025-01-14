using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public List<IPuzzle> puzzles = new List<IPuzzle>();
    
    public event System.Action<IPuzzle> PuzzleCompleted;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void RegisterPuzzle(IPuzzle puzzle)
    {
        puzzles.Add(puzzle);
        puzzle.Initialize();
    }

    public void OnPuzzleCompleted(IPuzzle puzzle)
    {
        Debug.Log("Puzzle completed: " + puzzle);
        PuzzleCompleted?.Invoke(puzzle);
    }

}