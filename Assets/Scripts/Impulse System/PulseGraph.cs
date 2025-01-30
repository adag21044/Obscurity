using UnityEngine;

public class PulseGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points = 50;  // Nabız çizgisindeki nokta sayısı
    public float amplitude = 0.05f;  // Nabız dalgasının yüksekliği
    public float frequency = 2.0f;  // Nabız hızı
    public float speed = 5.0f;  // Animasyon hızı

    private float time;

    void Start()
    {
        lineRenderer.positionCount = points;
    }

    void Update()
    {
        DrawPulse();
    }

    void DrawPulse()
    {
        time += Time.deltaTime * speed;
        
        for (int i = 0; i < points; i++)
        {
            float x = i * 0.02f;  // X ekseni boyunca ilerleme
            float y = Mathf.Sin((x + time) * frequency) * amplitude;  // Sinüs dalgası

            // Arada nabız piki (sert yükseliş) oluştur
            if (i == points / 3)
                y += amplitude * 2;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
