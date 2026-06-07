using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private AlterableStatsController _stats;
    [SerializeField] private StatWidget _widgetPrefab;
    [SerializeField] private Transform _container;
    
    private Dictionary<StatType, StatWidget> _widgets = new();
    // ----- FIELDS ----- //

    private void Start()
    {
        if (!_stats)
            return;

        foreach (StatType stat in System.Enum.GetValues(typeof(StatType)))
        {
            var widget = Instantiate(_widgetPrefab, _container);
            widget.SetStat(stat.ToString(), _stats.GetStat(stat));
            _widgets[stat] = widget;
        }

        _stats.OnStatChanged += HandleStatChanged;
    }

    private void OnDestroy()
    {
        if (!_stats)
            return;

        _stats.OnStatChanged -= HandleStatChanged;
    }

    private void HandleStatChanged(StatType stat)
    {
        _widgets[stat].SetStat(stat.ToString(), _stats.GetStat(stat));
    }
}
