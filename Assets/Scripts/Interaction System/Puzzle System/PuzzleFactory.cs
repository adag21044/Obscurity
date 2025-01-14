using UnityEngine;

public class PuzzleFactory
{
    public static T CreatePuzzle<T>(GameObject prefab, Vector3 position) where T : BasePuzzle
    {
        GameObject puzzleObject = Object.Instantiate(prefab, position, Quaternion.identity);
        return puzzleObject.GetComponent<T>();
    }
}
