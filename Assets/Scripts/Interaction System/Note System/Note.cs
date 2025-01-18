using System;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    public NoteSO noteData; // The note data
    public string objectData;
    public GameObject readingNote;

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public bool IsAvailable()
    {
        throw new System.NotImplementedException();
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
    }
}
