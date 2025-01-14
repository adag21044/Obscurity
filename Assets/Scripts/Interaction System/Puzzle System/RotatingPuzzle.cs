using UnityEngine;

public class RotatingPuzzle : BasePuzzle
{
    public Transform[] tiles; // Puzzle kareleri
    public Vector3[] correctRotations; // Doğru rotasyonlar
    private bool isActive = false; // Puzzle aktif mi?
    private Transform selectedTile; // Seçilen kare
    private Vector3[] initialRotations; // Başlangıç rotasyonları

    public Collider puzzleArea; // Puzzle alanını tanımlamak için collider

    public override void Initialize()
    {
        initialRotations = new Vector3[tiles.Length];

        for (int i = 0; i < tiles.Length; i++)
        {
            // Rastgele başlangıç rotasyonu ayarla (90 veya 270 derece)
            int[] randomAngles = { 90, 270 };
            int randomIndex = Random.Range(0, randomAngles.Length);
            tiles[i].eulerAngles = new Vector3(0, 0, randomAngles[randomIndex]);

            // Başlangıç rotasyonunu kaydet
            initialRotations[i] = tiles[i].eulerAngles;
        }

        Debug.Log("Puzzle initialized with random rotations.");
    }

    public override void Interact()
    {
        // Puzzle'ı aktif et ve input kilitle
        isActive = true;
        Cursor.lockState = CursorLockMode.None; // Fareyi serbest bırak
        Cursor.visible = true;
        Debug.Log("Puzzle interaction started.");
    }

    private void Update()
    {
        if (!isActive) return; // Puzzle aktif değilse inputları dinleme

        // ESC tuşuna basıldığında puzzle'dan çıkış
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPuzzle();
        }

        // R tuşuna basıldığında puzzle'ı sıfırla
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPuzzle();
        }

        // Fare ile kare seçimi
        if (Input.GetMouseButtonDown(0)) // Sol tık ile kare seç
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (System.Array.Exists(tiles, t => t == hit.transform))
                {
                    selectedTile = hit.transform;
                    Debug.Log("Tile selected: " + selectedTile.name);
                }
            }
        }

        // Seçilen kareyi döndürme
        if (selectedTile != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateTile(selectedTile, -90f);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RotateTile(selectedTile, 90f);
            }
        }

        // Puzzle tamamlandı mı kontrol et
        if (CheckCompletion())
        {
            NotifyCompletion();
            ExitPuzzle();
        }
    }

    public override bool CheckCompletion()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (!Mathf.Approximately(tiles[i].eulerAngles.z, correctRotations[i].z))
            {
                return false; // Eğer herhangi bir kare doğru değilse
            }
        }

        return true; // Hepsi doğru pozisyondaysa
    }

    public override void ResetPuzzle()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].eulerAngles = initialRotations[i];
        }

        Debug.Log("Puzzle has been reset to initial state.");
    }

    private void ExitPuzzle()
    {
        isActive = false;
        selectedTile = null; // Seçilen kareyi sıfırla
        Cursor.lockState = CursorLockMode.Locked; // Fareyi tekrar kilitle
        Cursor.visible = false;
        Debug.Log("Exited puzzle interaction.");
    }

    public void RotateTile(Transform tile, float angle)
    {
        tile.eulerAngles += new Vector3(0, 0, angle);
        tile.eulerAngles = new Vector3(
            tile.eulerAngles.x,
            tile.eulerAngles.y,
            Mathf.Round(tile.eulerAngles.z / 90) * 90
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact(); // Oyuncu puzzle alanına girdiğinde etkileşim başlar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExitPuzzle(); // Oyuncu puzzle alanından çıktığında etkileşim sonlanır
        }
    }
}
