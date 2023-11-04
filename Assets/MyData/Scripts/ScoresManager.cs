using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoresManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoresText;

    private int score;

    private void Start()
    {
        score = -1;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoresText.text = (++score).ToString();
    }
}
