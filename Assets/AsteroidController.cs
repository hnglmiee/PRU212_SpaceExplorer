using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject scoreUIText;
    public GameObject explosion;
    float speed;

    void Start()
    {
        speed = 2f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        Vector2 postion = transform.position;
        postion = new Vector2(postion.x, postion.y - speed * Time.deltaTime);
        transform.position = postion;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShipTag" || col.tag == "PlayerBulletTag")
        {
            PlayExplosion();

            scoreUIText.GetComponent<GameScore>().Score += 100;

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        if (explosion != null)
        {
            GameObject expl = Instantiate(explosion);
            expl.transform.position = transform.position;
        }
    }
}
