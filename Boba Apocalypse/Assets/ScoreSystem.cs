using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public CustomerScript CustomerScript;
    [SerializeField] public TextMeshProUGUI ScoreVar;
    [SerializeField] public TextMeshProUGUI Level;
    // Start is called before the first frame update
    private int score;
    private int level;
    void Start()
    {
        score = 0;
        level = 1;
        ScoreVar.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CustomerScript != null && CustomerScript.gotBoba)
        {
            score += 20;
            ScoreVar.text = score.ToString();
            CustomerScript.gotBoba = false;
        }
        if (score >= (30 + 20 * (level - 1)))
        {
            level += 1;
            Level.text = string.Format("Level {0}", level);
        }
    }
}
