using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float correction;
    [SerializeField] private float range;
    private float localScaleX;
    private float localPositionZ;

    private void Start()
    {
        localScaleX = transform.localScale.x;
        localPositionZ = transform.localPosition.z;
    }

    private void Update()
    {
        float x = (-Screen.width / 2f + Input.mousePosition.x) * correction;
        if (Mathf.Abs(x) > 12) x = x > 0 ? range : -range;
        transform.position = new Vector3( x, 1f, localPositionZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        gameManager.HitTheBall(( other.transform.position.x-transform.position.x )/(localScaleX/2), localScaleX,localPositionZ );
    }
}
