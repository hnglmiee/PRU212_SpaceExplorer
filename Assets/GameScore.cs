using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public TMP_Text scoreUIText;

    private int _score;

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            UpdateScoreTextUI();
        }
    }

    void Start()
    {
        if (scoreUIText == null)
        {
            scoreUIText = GetComponent<TMP_Text>();
        }

        Score = 0;
    }

    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000}", _score);
        scoreUIText.text = scoreStr;
    }
}
