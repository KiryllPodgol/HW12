using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset; 

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Цель для слежения не установлена!");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            
            transform.position = target.position + offset;
        }
    }
}