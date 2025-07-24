using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject Asteroid;
    float maxSpawnRateInSeconds = 5f;
    private bool isSpawning = false;

    void Start()
    {
        // Không làm gì ở đây
    }

    void SpawnAsteroid()
    {
        if (!isSpawning) return; // Ngừng nếu đang GameOver

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anAsteroid = Instantiate(Asteroid);
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        if (isSpawning) // Chỉ lên lịch tiếp nếu còn đang spawn
            ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        if (!isSpawning) return;

        float spawnInNSeconds = (maxSpawnRateInSeconds > 1f)
            ? Random.Range(1f, maxSpawnRateInSeconds)
            : 1f;

        Invoke("SpawnAsteroid", spawnInNSeconds);
    }

    public void UnscheduleEnemySpawn()
    {
        isSpawning = false;
        CancelInvoke("SpawnAsteroid");
        CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduledEnemySpawn()
    {
        float maxSpawnRateInSeconds = 5f;
        isSpawning = true;
        Invoke("SpawnAsteroid", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void IncreaseSpawnRate()
    {
        if (!isSpawning) return;

        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void Init()
    {
        maxSpawnRateInSeconds = 5f;
        ScheduledEnemySpawn();
    }
}
