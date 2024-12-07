using UnityEngine;
using UnityEngine.AI;
public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door Door;
    private int AgentsInRange = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            AgentsInRange++;
            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            AgentsInRange--;
            if (Door.IsOpen && AgentsInRange == 0)
            {
                Door.Close();
            }
        }
    }
}
