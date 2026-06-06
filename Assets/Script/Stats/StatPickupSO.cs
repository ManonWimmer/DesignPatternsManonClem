using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatPickupSO", menuName = "Scriptable Objects/StatPickupSO")]
public class StatPickupSO : ScriptableObject
{
    // ----- FIELDS ----- //
    public List<StatModifier> Modifiers;
    public float Duration; // 0 = pas de duration
    // ----- FIELDS ----- //
}
