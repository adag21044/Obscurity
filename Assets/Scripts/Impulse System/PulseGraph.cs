using UnityEngine;

public class PulseGraph : MonoBehaviour
{
    public ParticleSystem healthParticleSystem; // Particle System bileşeni
    [Range(0, 100)]
    public float healthPercentage = 100f; // Sağlık yüzdesi (0-100)

    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        if (healthParticleSystem == null)
        {
            Debug.LogError("Particle System is not assigned!");
            return;
        }

        mainModule = healthParticleSystem.main;
        UpdateParticleColor();
    }

    void Update()
    {
        UpdateParticleColor();
    }

    void UpdateParticleColor()
    {
        // Renk paleti: Yeşil → Açık Yeşil → Sarı → Turuncu → Kırmızı
        Color green = new Color(0.004f, 1f, 0f); // #01FF00
        Color lightGreen = new Color(0.5f, 1f, 0f); // #80FF00
        Color yellow = new Color(1f, 1f, 0f);    // #FFFF00
        Color orange = new Color(1f, 0.5f, 0f);  // #FF8000
        Color red = new Color(1f, 0f, 0f);       // #FF0000

        Color targetColor;

        if (healthPercentage >= 75)
        {
            // %100 - %75: Yeşil → Açık Yeşil geçişi
            float t = Mathf.SmoothStep(0, 1, (healthPercentage - 75) / 25f);
            targetColor = Color.Lerp(lightGreen, green, t);
        }
        else if (healthPercentage >= 50)
        {
            // %75 - %50: Açık Yeşil → Sarı geçişi
            float t = Mathf.SmoothStep(0, 1, (healthPercentage - 50) / 25f);
            targetColor = Color.Lerp(yellow, lightGreen, t);
        }
        else if (healthPercentage >= 25)
        {
            // %50 - %25: Sarı → Turuncu geçişi
            float t = Mathf.SmoothStep(0, 1, (healthPercentage - 25) / 25f);
            targetColor = Color.Lerp(orange, yellow, t);
        }
        else
        {
            // %25 - %0: Turuncu → Kırmızı geçişi
            float t = Mathf.SmoothStep(0, 1, healthPercentage / 25f);
            targetColor = Color.Lerp(red, orange, t);
        }

        // Particle System rengini güncelle
        mainModule.startColor = targetColor;
    }
}
