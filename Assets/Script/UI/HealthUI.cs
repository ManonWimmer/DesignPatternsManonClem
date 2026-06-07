using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("UI")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthTxt;

    [Header("Refs")]
    [SerializeField] private HealthController _healthController;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_healthController)
            _healthController.OnHealthChanged += OnHealthChanged;

        SetupHealth();
    }

    private void OnDestroy()
    {
        if (_healthController)
            _healthController.OnHealthChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth / maxHealth;
        _healthTxt.text = $"{currentHealth} / {maxHealth}";
    }

    private void SetupHealth()
    {
        OnHealthChanged(_healthController.Health, _healthController.MaxHealth);
    }
}
