using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckDistance", story: "Check if [agent]'s distance to [target] is [type] to [targetDistance]", category: "Action", id: "23f2b865eaa160a135002d578da3464d")]
public partial class CheckDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<CheckDistanceType> Type;
    [SerializeReference] public BlackboardVariable<float> TargetDistance;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null || TargetDistance <= 0)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || Target.Value == null)
            return Status.Failure;

        float distance = Vector3.Distance(Target.Value.transform.position, Agent.Value.transform.position);

        if (Type.Value == CheckDistanceType.Inferior)
        {
            if (distance < TargetDistance.Value)
                return Status.Success;
        }
        else
        {
            if (distance > TargetDistance.Value)
                return Status.Success;
        }

        return Status.Running;
    }
}

public enum CheckDistanceType
{
    Inferior, 
    Superior
}

