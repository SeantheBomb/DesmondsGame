using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatMeleeAttack))]
public class AttackMotor : AgentMotor
{

    CombatMeleeAttack m_Attack;

    protected override bool ScheduleTask => true;

    protected override bool LoopBehavior => true;

    public override IEnumerator RunTask(AgentController agent)
    {
        m_Attack.StartSwing();
        yield return null;
    }

    public override void StopTask(AgentController agent)
    {
        m_Attack.ResetSword();
    }

    public override void Start()
    {
        base.Start();
        m_Attack = GetComponent<CombatMeleeAttack>();
    }
}
