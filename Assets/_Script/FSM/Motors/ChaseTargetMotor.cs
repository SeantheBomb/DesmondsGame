using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IDestination))]
public class ChaseTargetMotor : AgentMotor
{
    protected override bool ScheduleTask => true;

    protected override bool LoopBehavior => true;

    IDestination nav;

    public override IEnumerator RunTask(AgentController agent)
    {
        nav.SetDestination(agent.target.position);
        yield return null;
    }

    public override void StopTask(AgentController agent)
    {
        nav.ClearDestination();
    }

    public override void Start()
    {
        base.Start();
        nav = GetComponent<IDestination>();
    }


}
