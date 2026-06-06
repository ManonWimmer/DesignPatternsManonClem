using TMPro;
using UnityEngine;

public class StatWidget : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _value;
    // ----- FIELDS ----- //

    public void SetStat(string label, float value)
    {
        _label.text = label;
        _value.text = value.ToString("0.##");
    }
}
