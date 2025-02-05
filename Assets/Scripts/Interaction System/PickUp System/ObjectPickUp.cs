using UnityEngine;

// Bu script, IInteractable arayüzünü uygulayarak, oyuncu ile etkileşime girildiğinde eşyanın 
// alınıp bırakılmasını sağlar.
public class ObjectPickUp : MonoBehaviour, IInteractable
{
    
    [Header("Pickup Settings")]
    public bool isPickedUp = false;          // Eşyanın taşınıp taşınmadığını takip eder
    public Transform originalParent;         // Eşyanın sahnedeki orijinal parent’ı
    public Rigidbody rb;                     // Eşyanın Rigidbody bileşeni
    public Transform holdPoint;              // Eşyanın taşındığı nokta (oyuncunun "tuttuğu" yer)

    [Header("Smooth Movement Settings")]
    public float holdDistance = 2.5f;          // Kameradan objeye istenen mesafe
    public float smoothSpeed = 10f;            // Yumuşak geçiş hızı (büyük değer daha hızlı, küçük değer daha yavaş)
    private Vector3 currentVelocity = Vector3.zero; // SmoothDamp için geçici hız değeri

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;

        // Sahnedeki hold point’i bulmak için, bu nesnenin "HoldPoint" tag’ine sahip bir GameObject olması gerekir.
        GameObject holdPointObj = GameObject.FindGameObjectWithTag("HoldPoint");
        if (holdPointObj != null)
        {
            holdPoint = holdPointObj.transform;
        }
        else
        {
            Debug.LogWarning("HoldPoint bulunamadı! Lütfen sahnede 'HoldPoint' tag’ine sahip bir GameObject ekleyin.");
        }
    }

    // IInteractable arayüzündeki metot; oyuncu etkileşime geçtiğinde çağrılır.
    public void Interact()
    {
        if (!isPickedUp)
        {
            Pickup();
        }
        else
        {
            Drop();
        }
    }

    // Eşyayı oyuncuya “alır”: 
    // - Fizik etkileşimleri devre dışı bırakılır,
    // - Eşya holdPoint’e parent olarak atanır ve konumu sıfırlanır.
    public void Pickup()
    {
        isPickedUp = true;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        
        // Eğer dinamik olarak kameraya göre pozisyonlandıracaksan re-parent yapmana gerek yok:
        // transform.SetParent(null); // ya da olduğu gibi bırak

        // İlk an için objeyi hemen pozisyonlandırabilirsin
        UpdateHeldObjectPosition();
    }

    // Eşyayı oyuncudan “bırakır”:
    // - Fizik etkileşimleri yeniden aktif edilir,
    // - Eşya orijinal parent’ına geri döner.
    public void Drop()
    {
        isPickedUp = false;
        transform.SetParent(originalParent);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    // IInteractable arayüzündeki açıklama metodu
    public string GetDescription()
    {
    
        return "Eşyayı al/koy";
    }

    void Update()
    {
        if (isPickedUp)
        {
            UpdateHeldObjectPosition();
            if (Input.GetMouseButtonDown(0)) // Sol tıklama ile resmi çerçeveye yerleştirme
            {
                TryPlaceOnFrame();
            }
        }
    }

   

    void UpdateHeldObjectPosition()
    {
        float holdDistance = 2.5f;
        // Hedef konumu, kameranın ileri yönünde holdDistance kadar önde olacak şekilde hesaplıyoruz.
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
        
        // Vector3.SmoothDamp kullanarak, mevcut pozisyondan hedef pozisyona yumuşak geçiş yapıyoruz.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 1f / smoothSpeed);

        
        
    }

    private void TryPlaceOnFrame()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) // Raycast mesafesini artırdım
        {
            Debug.Log("Tıklanan obje: " + hit.collider.name); // Tıklanan objeyi görmek için

            if (hit.collider.CompareTag("Frame")) 
            {
                Debug.Log("Çerçeve tespit edildi!"); 
                PlaceOnFrame(hit.collider.transform);
            }
        }
        else
        {
            Debug.Log("Hiçbir objeye tıklanmadı!");
        }
    }


    private void PlaceOnFrame(Transform frame)
    {
        Frame frameComponent = frame.GetComponent<Frame>();
        
        if(frameComponent == null)
        {
            Debug.LogWarning("Çerçeve bileşeni bulunamadı!");
            return;
        }

        isPickedUp = false;
        transform.SetParent(frame);
        


        // RigidBody ayarları, resmin düşmemesi için güncellendi
        if (rb != null)
        {
            rb.isKinematic = true; // Resmi yerine sabitle
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Çerçevenin yüzeyine düzgün hizala
        transform.localPosition = new Vector3(0, 0, -0.01f); // Resmi hafif önde tut
         

        Debug.Log("Resim başarıyla yerleştirildi!"); // Hata ayıklamak için
        PaintingPuzzleController.Instance.CheckPuzzleCompletion();
    }
}
