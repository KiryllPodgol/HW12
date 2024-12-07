using UnityEngine;
public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f; 
    public float maxDistance = 2f;
    private Vector3 _startPos; 
    private Vector3 _targetPos; 

    void Start()
    {
        _startPos = transform.position; 
        RandomMove();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _targetPos) < 0.1f)
        {
            RandomMove(); 
        }
    }
    void RandomMove()
    {
        float offsetX = Random.Range(-maxDistance, maxDistance);
        float offsetZ = Random.Range(-maxDistance, maxDistance);
        _targetPos = new Vector3(_startPos.x + offsetX, _startPos.y, _startPos.z + offsetZ);
    }
}