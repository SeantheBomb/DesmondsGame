using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageSensor : AgentSensor, IDamageReceiver
{

    public AgentState hurt, dead;

    public float stunTime = 1f;

    AgentState previous;

    public float Priority => 1000;

    protected override bool LoopBehavior => true;

    public DamageInfo ReceiveDamage(Health health, DamageInfo damageInfo)
    {
        if (health.health - damageInfo.healthTaken <= 0)
        {
            agent.SetState(dead);
        }
        else
        {
            if (agent.CurrentState != hurt) previous = agent.CurrentState;
            agent.SetState(hurt);
        }
        return damageInfo;
    }

    public override IEnumerator RunTask(AgentController agent)
    {
        if(agent.CurrentState == hurt) {
            yield return new WaitForSeconds(stunTime);
            agent.SetState(previous);
        }
    }
}
