using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;      // Kéo prefab sao vào đây
    public int maxStars = 10;          // Số sao tối đa cùng lúc
    public Vector2 spawnAreaMin;       // Góc trái dưới vùng spawn
    public Vector2 spawnAreaMax;       // Góc phải trên vùng spawn

    private int currentStarCount = 0;
    private bool isSpawning = false;
    private float maxSpawnRateInSeconds = 3f;

    public void Init()
    {
        isSpawning = true;
        maxSpawnRateInSeconds = 15f;
        Debug.Log("StarSpawner started");
        Invoke("SpawnStar", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f); // Tùy chọn
    }

    void SpawnStar()
    {
        if (!isSpawning || currentStarCount >= maxStars)
        {
            Debug.Log("Skip star spawn. Spawning: " + isSpawning + ", count: " + currentStarCount);
            ScheduleNextSpawn();
            return;
        }

        Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Debug.Log("Spawn star at: " + spawnPos);
        GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
        currentStarCount++;

        Destroy(star, 10f); // Optional
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        if (!isSpawning) return;

        float nextSpawnIn = (maxSpawnRateInSeconds > 1f)
            ? Random.Range(1f, maxSpawnRateInSeconds)
            : 1f;

        Invoke("SpawnStar", nextSpawnIn);
    }

    public void OnStarCollected()
    {
        currentStarCount--;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnStar");
        CancelInvoke("IncreaseSpawnRate");
    }

    void IncreaseSpawnRate()
    {
        if (!isSpawning) return;

        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds <= 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
