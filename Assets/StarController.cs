using UnityEngine;

public class StarController : MonoBehaviour
{
    public int scoreValue = 200;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShipTag"))
        {
            GameManager.Instance.AddScore(scoreValue);

            // Gọi tới StarSpawner (nếu có)
            StarSpawner spawner = Object.FindFirstObjectByType<StarSpawner>();
            if (spawner != null)
                spawner.OnStarCollected();

            // Phát âm thanh
            if (audioSource != null && audioSource.clip != null)
                audioSource.Play();

            // Delay hủy để kịp phát âm
            Destroy(gameObject, 0.2f);
        }
    }
}
