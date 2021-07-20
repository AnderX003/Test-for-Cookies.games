using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public BallController currentBall { get; private set; }
    public static BallsManager instance { get; private set; }
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ballPrefab, ballDeathParticle;

    private void Start()
    {
        instance = this;
    }

    public void CreateBall()
    {
        GameObject newBall = Instantiate(ballPrefab, transform);
        currentBall = newBall.GetComponent<BallController>();
        currentBall.AddForce();
    }

    public  void OnBallDestroyed(Vector3 ballPosition)
    {
            Instantiate(ballDeathParticle).transform.position = ballPosition;
            CreateBall();
            gameManager.OnPlayerMissedBall();
    }
}
