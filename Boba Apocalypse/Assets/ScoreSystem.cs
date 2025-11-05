using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public CustomerScript CustomerScript;
    [SerializeField] public TextMeshProUGUI ScoreVar;
    [SerializeField] public TextMeshProUGUI Level;
    [SerializeField] public GameObject[] toppings;
    // Start is called before the first frame update
    private int score;
    private int level;
    private int currentUnlockedToppings;
    private int[] levelsToUnlockToppings = { 4, 8, 12, 16, 20 };
    void Start()
    {
        score = 140;
        level = 1;
        ScoreVar.text = score.ToString();
        for (int i = 0; i < toppings.Length; i++)
        {
            toppings[i].SetActive(false);
        }
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
        if (levelsToUnlockToppings.Contains(level))
        {
            for (int i = 0; i <= levelsToUnlockToppings.Length; i++) {
                if (levelsToUnlockToppings[i] == level)
                {
                    currentUnlockedToppings = i;
                    break;
                }
            }
            toppings[currentUnlockedToppings].SetActive(true);
        }
    }
}
