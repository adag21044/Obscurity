using System;
using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    public NoteSO noteData; // The note data
    public string objectData;
    public GameObject readingNote;
    public TMP_Text noteText;
    public bool isNoteOpened = false;

    public string GetDescription()
    {
        return "";
    }

    public void Interact()
    {
        
    }

    

    public Camera mainCamera; // Ana kamera
    public float rayDistance = 10f; // Ray mesafesi

    void Update()
    {
        // Mouse sol tık kontrolü (isteğe bağlı)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Kameranın merkezinden ray gönder
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                // Objeyi tespit ettik
                Note objScript = hit.collider.GetComponent<Note>();
                if (objScript != null && objScript.objectData != null)
                {
                    Debug.Log($"Baktığınız obje: {hit.collider.name}");
                    Debug.Log($"Objenin metni: {objScript.objectData}");

                    noteText.text = objScript.objectData;
                    readingNote.SetActive(true);
                    isNoteOpened = true;

                    
                }
                else
                {
                    Debug.Log("Baktığınız obje ScriptableObject referansı taşımıyor.");
                }
            }
            else
            {
                Debug.Log("Hiçbir obje algılanmadı.");
            }
        }
        else 
        if(isNoteOpened && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Close Note");
            readingNote.SetActive(false);
        }
    }
}
