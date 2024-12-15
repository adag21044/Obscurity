using UnityEngine;
using TMPro;

public class NoteUIController : MonoBehaviour
{
    private bool isNoteOpen = false; // Tracks if the note is currently open
    [SerializeField] private GameObject noteCanvas; // Reference to the note UI canvas
    [SerializeField] private TMP_Text noteText; // Text component to display note content
    [SerializeField] private AudioClip screamSound; // Scream sound effect
    [SerializeField] private float cameraShakeDuration = 10f; // Duration of camera shake
    [SerializeField] private float cameraShakeIntensity = 0.1f; // Intensity of camera shake

    private AudioSource screamAudioSource; // Separate AudioSource for scream sound
    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    private bool isCameraShaking = false;

    public static NoteUIController instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        mainCamera = Camera.main;

        // Add a separate AudioSource for scream sound
        screamAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isCameraShaking)
        {
            ShakeCamera();
        }
        
        if (isNoteOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    public void CloseNote()
    {
        noteCanvas.SetActive(false);
        noteText.text = ""; // İçeriği sıfırla
        isNoteOpen = false;
        LockPlayerControls(false);
    }

    public void DisplayNote(string noteContent, bool hasSpecialEffect)
    {
        if (noteText != null)
        {
            noteText.text = noteContent; // Not içeriğini güncelle
            Debug.Log("Text successfully updated.");
        }
        else
        {
            Debug.LogError("noteText reference is null!");
        }

        noteCanvas.SetActive(true);
        isNoteOpen = true;

        if (hasSpecialEffect)
        {
            PlayScreamAndShake();
        }
        else
        {
            LockPlayerControls(true);
        }
    }




    

    private void PlayScreamAndShake()
    {
        if (screamSound != null)
        {
            screamAudioSource.PlayOneShot(screamSound);
        }

        StartCameraShake();
        LockPlayerControls(true);
    }

    private void StartCameraShake()
    {
        isCameraShaking = true;
        originalCameraPosition = mainCamera.transform.localPosition;
        Invoke(nameof(StopCameraShake), cameraShakeDuration);
    }

    private void ShakeCamera()
    {
        mainCamera.transform.localPosition = originalCameraPosition + Random.insideUnitSphere * cameraShakeIntensity;
    }

    private void StopCameraShake()
    {
        isCameraShaking = false;
        mainCamera.transform.localPosition = originalCameraPosition;
        LockPlayerControls(false);
    }

    private void LockPlayerControls(bool isLocked)
    {
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null) playerInput.enabled = !isLocked;

        var cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null) cameraController.enabled = !isLocked;
    }
}
