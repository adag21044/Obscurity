using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ICatch, IFollowable
{
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;

    


    public void Catch()
    {
        throw new System.NotImplementedException();
    }

    public void Follow()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent bileşeni eksik! Lütfen Enemy nesnesine ekleyin.");
            return;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player GameObject bulunamadı! Lütfen sahnede 'Player' tag'ine sahip bir obje ekleyin.");
            }
        }

        // Enemy'nin NavMesh üzerinde olup olmadığını kontrol et
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Enemy başlangıçta NavMesh üzerinde değil! Düzeltme yapılıyor...");
            SnapToNearestNavMesh();
        }

        agent.SetDestination(player.transform.position);

    }

    private void SnapToNearestNavMesh()
    {
        NavMeshHit hit;
        float searchRadius = 10f; // NavMesh arama yarıçapı
        float stepSize = 1f; // Adım büyüklüğü
        int maxAttempts = 15; // Maksimum deneme sayısı

        // Öncelikle bulunduğu yerde NavMesh olup olmadığını kontrol et
        if (NavMesh.SamplePosition(transform.position, out hit, searchRadius, NavMesh.AllAreas))
        {
            MoveToNavMesh(hit.position);
            return;
        }

        Debug.LogWarning("Mevcut konumda NavMesh bulunamadı, sola doğru tarama başlatılıyor...");

        // X ekseni boyunca sola git ve NavMesh ara
        for (int i = 1; i <= maxAttempts; i++)
        {
            Vector3 leftCheck = transform.position + new Vector3(-stepSize * i, 0, 0);

            if (NavMesh.SamplePosition(leftCheck, out hit, searchRadius, NavMesh.AllAreas))
            {
                MoveToNavMesh(hit.position);
                return;
            }
        }

        Debug.LogWarning("X ekseninde solda NavMesh bulunamadı, sağa doğru arama yapılıyor...");

        // X ekseni boyunca sağa git ve NavMesh ara
        for (int i = 1; i <= maxAttempts; i++)
        {
            Vector3 rightCheck = transform.position + new Vector3(stepSize * i, 0, 0);

            if (NavMesh.SamplePosition(rightCheck, out hit, searchRadius, NavMesh.AllAreas))
            {
                MoveToNavMesh(hit.position);
                return;
            }
        }

        Debug.LogWarning("X ekseninde NavMesh bulunamadı, Z ekseninde ileri-geri arama yapılıyor...");

        // Z ekseni boyunca ileri ve geri NavMesh ara
        for (int i = 1; i <= maxAttempts; i++)
        {
            Vector3 forwardCheck = transform.position + new Vector3(0, 0, stepSize * i);
            Vector3 backwardCheck = transform.position + new Vector3(0, 0, -stepSize * i);

            if (NavMesh.SamplePosition(forwardCheck, out hit, searchRadius, NavMesh.AllAreas))
            {
                MoveToNavMesh(hit.position);
                return;
            }

            if (NavMesh.SamplePosition(backwardCheck, out hit, searchRadius, NavMesh.AllAreas))
            {
                MoveToNavMesh(hit.position);
                return;
            }
        }

        Debug.LogError("Düşman hiçbir yönde NavMesh bulamadı, son çare olarak en yakın güvenli noktaya ışınlanıyor!");

        // Eğer hiçbir yerde NavMesh yoksa, en yakın güvenli noktaya ışınla
        if (NavMesh.SamplePosition(Vector3.zero, out hit, searchRadius * 2, NavMesh.AllAreas))
        {
            MoveToNavMesh(hit.position);
        }
        else
        {
            Debug.LogError("Hiçbir yerde NavMesh bulunamadı, düşman sabit kalıyor!");
        }
    }



    private void MoveToNavMesh(Vector3 position)
    {
        transform.position = position;
        agent.Warp(position); // NavMeshAgent'i yeni konuma adapte et
        Debug.Log($"Enemy en yakın NavMesh noktasına taşındı: {position}");
    }



    private void Update()
    {
        if (agent == null || player == null) return;

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent henüz NavMesh'e oturmadı, en yakın yürünebilir alan aranıyor...");
            SnapToNearestNavMesh();
            return;
        }

        agent.SetDestination(player.transform.position);
    }


    private void FixNavMeshPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

}
