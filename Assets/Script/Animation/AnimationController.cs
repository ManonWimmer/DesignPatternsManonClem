using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Die")]
    [SerializeField] private HealthController _healthController = null;
    [SerializeField] private string _dieAnimTrigger = "Die";

    [Header("Attack")]
    [SerializeField] private AttackController _attackController = null;
    [SerializeField] private string _attackAnimTrigger = "Attack";
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_healthController)
            _healthController.OnDie += OnDie;

        if (_attackController)
            _attackController.OnAttack += OnAttack;
    }

    private void OnDestroy()
    {
        if (_healthController)
            _healthController.OnDie -= OnDie;

        if (_attackController)
            _attackController.OnAttack += OnAttack;
    }

    private void OnAttack()
    {
        if (!_animator)
            return; 

        _animator.SetTrigger(_attackAnimTrigger);
    }

    private void OnDie()
    {
        if (!_animator)
            return;

        _animator.SetTrigger(_dieAnimTrigger);
    }
}
