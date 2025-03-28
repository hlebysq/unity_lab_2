using UnityEngine;
using TMPro;
using Mono.Cecil;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        score += (int)Mathf.Pow(2, value);
        highScore = Mathf.Max(highScore, score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Current: \n{score}";
        highScoreText.text = $"High:\n{highScore}";
    }
}
