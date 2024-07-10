using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerSensor : AgentSensor
{

    public AgentState idle, attack, move;

    public float attackRange = 1f;
    public float alertRange = 3f;

    protected override bool LoopBehavior => true;

    public override IEnumerator RunTask(AgentController agent)
    {
        if (agent.target == null)
            agent.target = AgentManager.Instance.Player.transform;
        Vector3 delta = agent.target.position - transform.position;
        if (delta.magnitude < attackRange) {
            agent.SetState(attack);
        }
        else if (delta.magnitude < alertRange) {
            agent.SetState(move);
        }
        else
        {
            agent.SetState(idle);
        }
        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
