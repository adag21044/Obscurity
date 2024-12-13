using UnityEngine;
using TMPro;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f; // Maximum distance for raycast detection
    [SerializeField] private TMP_Text interactionText; // UI text to show "Open Note: E"
    private NoteComponent currentNote; // Reference to the currently detected note

    void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    // Sends a raycast from the center of the camera and checks for notes
    private void HandleRaycast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if raycast hits a note
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            NoteComponent note = hit.collider.GetComponent<NoteComponent>();

            if (note != null)
            {
                currentNote = note;
                interactionText.text = "Open Note: E";
                interactionText.gameObject.SetActive(true);
                return;
            }
        }

        // Hide the interaction text if no note is detected
        interactionText.gameObject.SetActive(false);
        currentNote = null;
    }

    // Handles input for opening the note
    private void HandleInput()
    {
        if (currentNote != null && Input.GetKeyDown(KeyCode.E))
        {
            // Display the note content in the UI
            NoteUIController.instance.DisplayNote(currentNote.GetNoteContent());
        }
    }
}
