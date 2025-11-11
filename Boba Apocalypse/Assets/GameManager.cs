using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ScoreSystem scoreSystem;
    public static int score;

    void Update()
    {
        score = scoreSystem.getScore();
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static int getFinalScore()
    {
        
        return score;
    }
}

