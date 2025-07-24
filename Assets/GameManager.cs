using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawn;
    public GameObject scoreUIText;
    public GameObject InstructionButton;
    public GameObject starSpawner;
    public GameObject GameOver;


    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver
    }

    GameManagerState GMState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    void UpdateGameManagementState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                GameOver.SetActive(false);

                playButton.SetActive(true);
                if (InstructionButton != null)
                    InstructionButton.SetActive(true);
                break;

            case GameManagerState.GamePlay:
                if (scoreUIText != null)
                    scoreUIText.GetComponent<GameScore>().Score = 0;

                playButton.SetActive(false);
                if (InstructionButton != null)
                    InstructionButton.SetActive(false);

                playerShip.GetComponent<PlayController>().Init();
                enemySpawn.GetComponent<AsteroidSpawner>().Init();
                starSpawner.GetComponent<StarSpawner>().Init();

                break;

            case GameManagerState.GameOver:
                enemySpawn.GetComponent<AsteroidSpawner>().UnscheduleEnemySpawn();

                GameOver.SetActive(true);

                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagementState();
    }

    public void StartGamePlay()
    {
        SetGameManagerState(GameManagerState.GamePlay);
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }


    public void AddScore(int amount)
    {
        if (scoreUIText != null)
        {
            GameScore gameScore = scoreUIText.GetComponent<GameScore>();
            if (gameScore != null)
            {
                gameScore.Score += amount;
            }
        }
    }

}
