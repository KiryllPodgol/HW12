using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private InputAsset _inputAsset;
    public float normalSpeed = 3.5f; // Обычная скорость
    public float minSpeed = 2.25f;    // Минимальная скорость в зоне
    private int _slowZoneArea;
    private float _targetSpeed;// Целевая скорость
    public float speedChangeRate = 1f; // Скорость изменения (чем больше, тем быстрее смена)
    private bool _isInSlowZone = false; 

    void Start()
    {
     
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        _targetSpeed = normalSpeed;
        _inputAsset = new InputAsset();
        _inputAsset.Gameplay.Mousb.started += Mousb_started;
        _inputAsset.Enable();

        
        _slowZoneArea = NavMesh.GetAreaFromName("Slow");
        if (_slowZoneArea == -1)
        {
            Debug.LogError("Область 'Slow' не найдена! Проверьте настройки NavMesh Areas.");
        }
    }

    private void OnDisable()
    {
        
        _inputAsset.Gameplay.Mousb.started -= Mousb_started;
        _inputAsset.Disable();
    }

    private void Mousb_started(InputAction.CallbackContext obj)
    {
        
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void Update()
    {
        if (agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                
                if (hit.mask == (1 << _slowZoneArea))
                {
                    if (!_isInSlowZone)
                    {
                        _isInSlowZone = true;
                        Debug.Log("Вошел в зону замедления.");
                    }
                }
                else
                {
                    if (_isInSlowZone)
                    {
                        _isInSlowZone = false; 
                        Debug.Log("Вышел из зоны замедления.");
                    }
                }
            }
            _targetSpeed = _isInSlowZone ? minSpeed : normalSpeed;
            agent.speed = Mathf.Lerp(agent.speed, _targetSpeed, Time.deltaTime * speedChangeRate);

            // отслеживания изменения скорости
            if (Mathf.Abs(agent.speed - _targetSpeed) > 0.01f)
            {
                Debug.Log($"Изменение скорости: {agent.speed:F2} -> {_targetSpeed}");
            }
        }
    }
}
