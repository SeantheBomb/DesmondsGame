using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(IDestination))]
public class PatrolMotor : AgentMotor
{

    public Transform[] waypoints;

    public bool loop = false;


    IDestination nav;

    int index;


    protected override bool ScheduleTask => false;

    protected override bool LoopBehavior => true;

    public override IEnumerator RunTask(AgentController agent)
    {
        nav.SetDestination(waypoints[index].position);
        yield return new WaitUntil(() => nav.IsDestinationReached());
        index++;
        if(index == waypoints.Length)
        {
            if (loop == false)
            {
                waypoints = waypoints.Reverse().ToArray();
                index = 1;
            }
            else
            {
                index = 0;
            }
        }
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
