using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackAction", story: "[Agent] attack", category: "Action", id: "c55dde1767a02304ff1e63d0d1315cc0")]
public partial class AttackAction : Action
{
    // ----- FIELDS ----- //
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<AttackController> AttackController;
    // ----- FIELDS ----- //
    protected override Status OnStart()
    {
        if (!AttackController.Value)
            return Status.Failure;

        if (AttackController.Value.CanAttack())
        {
            AttackController.Value.Attack();
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }
}

