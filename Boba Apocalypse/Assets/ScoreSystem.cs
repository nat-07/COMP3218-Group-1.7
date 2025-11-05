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
    void Start()
    {
        score = 0;
        ScoreVar.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CustomerScript != null && CustomerScript.gotBoba)
        {
            score += 10;
            ScoreVar.text = score.ToString();
            CustomerScript.gotBoba = false;
        }
    }
}
