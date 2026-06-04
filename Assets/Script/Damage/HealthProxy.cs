using UnityEngine;

public class HealthProxy : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private HealthController _healthController = null;
    // ----- FIELDS ----- //
    private void Start()
    {
        if (_healthController == null)
            Debug.LogError("Health controller null in health proxy");
    }

    public HealthController GetHealthController()
    {
        return _healthController;
    }
}
