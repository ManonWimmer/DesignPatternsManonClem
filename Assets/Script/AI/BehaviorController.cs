using Unity.Behavior;
using UnityEngine;

public class BehaviorController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private BehaviorGraphAgent _agent;
    [SerializeField] private HealthController _healthController;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (!_agent)
            return;

        if (_healthController)
            _healthController.OnDie += HandleDeath;

    }

    private void OnDestroy()
    {
        if (_healthController)
            _healthController.OnDie -= HandleDeath;
    }

    private void HandleDeath()
    {
        _agent.End();
    }

    public void RestartBehavior()
    {
        _agent.Restart();
        _agent.BlackboardReference.SetVariableValue("AIState", AIState.Patrol);
    }
}
