using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int totalScore;
    public TextMeshProUGUI scoreText;

    public static GameController instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText() 
    { 
        scoreText.text = totalScore.ToString();
    }

}
