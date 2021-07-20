using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallsManager ballsManager;
    [SerializeField] private UIManager uiManager;
    private int playerScore;
    private int enemyScore;

    private void Start()
    {
        ballsManager.CreateBall();
    }

    public void HitTheBall(float variation, float localScaleX, float localPositionZ)
    {
        if (!ballsManager.currentBall.HitTheBall(variation, localScaleX, localPositionZ)) return;
        playerScore++;
        uiManager.UpdateScoreText(playerScore, enemyScore);
    }

    public void OnPlayerMissedBall()
    {
        enemyScore++;
        uiManager.UpdateScoreText(playerScore, enemyScore);
    }
}
