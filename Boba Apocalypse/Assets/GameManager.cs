using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ScoreSystem scoreSystem;
    public static int score;

    void Awake()
    {
        score = scoreSystem.getScore();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ✅ This keeps the object between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static int getFinalScore()
    {
        return score;
    }
}

