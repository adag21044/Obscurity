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

    private void HandleRaycast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            NoteComponent note = hit.collider.GetComponent<NoteComponent>();

            if (note != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                currentNote = note;
                interactionText.text = "Open Note: E";
                interactionText.gameObject.SetActive(true);
                return;
            }
        }

        interactionText.gameObject.SetActive(false);
        currentNote = null;
    }

    private void HandleInput()
    {
        if (currentNote != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Current Note Data: " + currentNote);
            Debug.Log("Note Content: " + currentNote.GetNoteContent());
            Debug.Log("Has Special Effect: " + currentNote.HasSpecialEffect());

            if (NoteUIController.instance != null)
            {
                NoteUIController.instance.DisplayNote(currentNote);
                currentNote.UpdateTextFromData();
                Debug.Log("DisplayNote called successfully");
            }
            else
            {
                Debug.LogError("NoteUIController instance is null!");
            }
        }
    }
}
