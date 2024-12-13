using UnityEngine;

public class BillboardText : MonoBehaviour
{
    void Update()
    {
        // Kamera yönüne bakmasını sağla
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
