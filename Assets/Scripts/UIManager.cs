using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text playerScoreText, enemyScoreText;

    public void UpdateScoreText(int playerScore, int enemyScore)
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
    }
}
