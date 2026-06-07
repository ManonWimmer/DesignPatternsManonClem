using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Health")]
    [SerializeField] private HealthController _healthController = null;
    [SerializeField] private string _hitAnimTrigger = "Hit";
    [SerializeField] private string _dieAnimTrigger = "Die";

    [Header("Attack")]
    [SerializeField] private AttackController _attackController = null;
    [SerializeField] private string _attackAnimTrigger = "Attack";

    [Header("Move")]
    [SerializeField] private Movable _movable = null;
    [SerializeField] private string _walkSpeedParam = "WalkSpeed";
    [SerializeField] private string _reverseParam = "Reverse";
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_healthController)
        {
            _healthController.OnDie += OnDie;
            _healthController.OnHit += OnHit;
        }

        if (_attackController)
            _attackController.OnAttack += OnAttack;

        if (_movable)
        {
            _movable.OnSpeedChanged += OnSpeedChanged;
            _movable.OnMoveAnimationRequested += OnMoveAnimationRequested;
            OnSpeedChanged(0);
        }
    }

    private void OnDestroy()
    {
        if (_healthController)
        {
            _healthController.OnDie -= OnDie;
            _healthController.OnHit -= OnHit;
        }

        if (_attackController)
            _attackController.OnAttack -= OnAttack;

        if (_movable)
        {
            _movable.OnSpeedChanged -= OnSpeedChanged;
            _movable.OnMoveAnimationRequested -= OnMoveAnimationRequested;
        }
    }

    private void OnAttack()
    {
        print("on attack anim");

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

    private void OnHit()
    {
        if (!_animator)
            return;

        _animator.SetTrigger(_hitAnimTrigger);
    }

    private void OnSpeedChanged(float speed)
    {
        if (!_animator) 
            return;

        _animator.SetFloat(_walkSpeedParam, speed);
    }

    private void OnMoveAnimationRequested(float speed, float reverse)
    {
        if (!_animator) 
            return;

        _animator.SetFloat(_reverseParam, reverse);
        _animator.SetFloat(_walkSpeedParam, speed);
    }
}
