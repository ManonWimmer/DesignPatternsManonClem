using UnityEngine;

public class StatsProxy : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private AlterableStatsController _stats = null;
    // ----- FIELDS ----- //
    private void Start()
    {
        if (_stats == null)
            Debug.LogError("Alterable stats controller null in health proxy");
    }

    public AlterableStatsController GetStats()
    {
        return _stats;
    }
}
