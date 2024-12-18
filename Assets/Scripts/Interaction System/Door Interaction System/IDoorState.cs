/// <summary>
/// Interface representing a door's state.
/// </summary>
public interface IDoorState
{
    /// <summary>
    /// Defines the interaction behavior based on the door's current state.
    /// </summary>
    /// <param name="door">The door being interacted with.</param>
    void Interact(Door door);
}
