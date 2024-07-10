using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(AgentController))]
public abstract class AgentMotor : CorruptedBehaviour<AgentController, AgentMotor>, IAgentRunTask
{
    [SerializeField] protected FloatVariable startDelay = -1f;
    [SerializeField] protected FloatVariable updateTime;
    [SerializeField] protected AgentState state;

    protected AgentController agent;


    protected abstract bool ScheduleTask { get; }
    protected abstract bool LoopBehavior { get; }


    protected virtual void OnEnable()
    {
        if (agent == null) agent = GetComponent<AgentController>();
        agent.OnEnterState += OnAgentEnterState;
        agent.OnExitState += OnAgentExitState;
        if (startDelay == -1f) startDelay = updateTime;
    }

    protected virtual void OnDisable()
    {
        agent.OnEnterState -= OnAgentEnterState;
        agent.OnExitState -= OnAgentExitState;
    }

    private void OnAgentEnterState(AgentState currentState, AgentState newState)
    {
        if (state ==currentState)//We are already in this state, no need to restart
        {
            return;
        }
        if (state == newState)//We are entering a valid state
        {
            Debug.Log($"Agent: Start Motor {GetType().Name} - {name}", gameObject);
            StartCoroutine(RunTaskLoop());
        }
    }

    private void OnAgentExitState(AgentState currentState, AgentState newState)
    {
        if (state == newState)//We are entering a valid state, so no need to restart
        {
            return;
        }
        if (state == currentState)//We are exiting a valid state, so we must stop our task
        {
            StopAllCoroutines();
            StopTask(agent);
            if (ScheduleTask)
            {
                AgentManager.Instance.ClearTasksFrom(agent);
            }
        }
    }

    protected IEnumerator RunTaskLoop()
    {
        yield return new WaitForSeconds(startDelay);
        do
        {
            if (ScheduleTask)
                yield return agent.ScheduleTask(RunTask(agent));
            else
                yield return RunTask(agent);
            yield return new WaitForSeconds(updateTime);
        } while (LoopBehavior);
    }


    public abstract IEnumerator RunTask(AgentController agent);

    public abstract void StopTask(AgentController agent);


    

}

