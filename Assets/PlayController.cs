using UnityEngine;
using TMPro;
using System.Collections;

public class PlayController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float hInput;
    private float vInput;

    public GameObject gameManagerObject;
    public GameObject PlayerBullet;
    public GameObject bulletPosition;
    public GameObject explosion;

    public TMP_Text LivesUIText;
    const int MaxLives = 1;
    int lives;

    bool isInvulnerable = false;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
        isInvulnerable = false;
    }

    void Start()
    {
        if (gameManagerObject == null)
        {
            gameManagerObject = GameObject.Find("GameManager");
            if (gameManagerObject == null)
            {
                Debug.LogError("⚠️ Không tìm thấy GameManager!");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = bulletPosition.transform.position;
        }

        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(hInput, vInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private bool justHit = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (justHit) return; // ✅ Đã bị xử lý va chạm → bỏ qua

        if (col.CompareTag("PlayerBulletTag")) return;

        if (col.CompareTag("AsteroidTag") && !isInvulnerable)
        {
            justHit = true; // 🔒 chặn va chạm tiếp theo tạm thời

            isInvulnerable = true;
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();
            Debug.Log($"→ Lives sau khi trừ: {lives}");

            Destroy(col.gameObject);

            if (lives <= 0)
            {
                gameManagerObject?.GetComponent<GameManager>()
                    .SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(InvulnerabilityCoroutine());
            }

            Invoke(nameof(ResetJustHit), 0.2f); // Mở lại va chạm sau 0.2 giây
        }
    }

    void ResetJustHit()
    {
        justHit = false;
    }



    void PlayExplosion()
    {
        if (explosion != null)
        {
            GameObject expl = Instantiate(explosion);
            expl.transform.position = transform.position;
        }
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Collider2D col = GetComponent<Collider2D>();

        if (col != null) col.enabled = false;

        for (int i = 0; i < 6; i++)
        {
            if (sr != null) sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            if (sr != null) sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        if (col != null) col.enabled = true;

        isInvulnerable = false;
    }
}
