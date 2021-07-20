using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float ballStartSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float ricochetGain;
    [SerializeField] [Range(0f, 1f)] private float maxVariation;
    [SerializeField] [Range(0f, 1f)] private float minVariation;
    private bool canHit = true;

    private void FixedUpdate()
    {
        if (ballRigidbody.velocity.magnitude < maxSpeed)
            ballRigidbody.velocity *= acceleration;
    }

    public void AddForce()
    {
        Vector3 force = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(0.5f, 1f)).normalized * ballStartSpeed;
        ballRigidbody.AddForce(force, ForceMode.VelocityChange);
        AddRandomToque(force);
    }

    private void AddRandomToque()
    {
        AddRandomToque(ballRigidbody.velocity);
    }
    
    private void AddRandomToque(Vector3 force)
    {
        ballRigidbody.AddTorque(new Vector3(force.z, 0, -force.x), ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                canHit = true;
                AddRandomToque();
                break;
            case "playerWall":
                Destroy(gameObject);
                BallsManager.instance.OnBallDestroyed(transform.position);
                break;
        }
    }

    public bool HitTheBall(float x, float localScaleX, float localPositionY)
    {
        if (transform.position.z < localPositionY + 0.5f)
        {
            Vector3 velocity = ballRigidbody.velocity;
            ballRigidbody.velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            return false;
        }

        if (!canHit) return false;
        if (Mathf.Abs(x) > maxVariation) x = x > maxVariation ? maxVariation : -maxVariation;
        else if (Mathf.Abs(x) < minVariation) x = x < minVariation ? minVariation : -minVariation;
        
        x *= localScaleX / 2;
        float y = Mathf.Sqrt(localScaleX - Mathf.Pow(x, 2));
        ballRigidbody.velocity = new Vector3(x * ricochetGain, 0, y).normalized * ballRigidbody.velocity.magnitude;
        canHit = false;
        AddRandomToque();
        return true;
    }
}
