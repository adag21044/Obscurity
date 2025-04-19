using UnityEngine;

[CreateAssetMenu(menuName = "RotationPuzzle/RightRotation")]
public class RightRotation : ScriptableObject
{
    [Range(0,360)] public int rotationX;
}