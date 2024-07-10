using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(AgentController))]
public abstract class AgentSensor : CorruptedBehaviour<AgentController, AgentSensor>, IAgentRunTask
{
    [SerializeField] protected FloatVariable updateTime;
    [SerializeField] protected AgentState[] states;

    protected abstract bool LoopBehavior { get; }

    protected AgentController agent;

    protected virtual void OnEnable()
    {
        if (agent == null) agent = GetComponent<AgentController>();
        agent.OnEnterState += OnAgentEnterState;
        agent.OnExitState += OnAgentExitState;
    }

    protected virtual void OnDisable()
    {
        agent.OnEnterState -= OnAgentEnterState;
        agent.OnExitState -= OnAgentExitState;
    }

    private void OnAgentEnterState(AgentState currentState, AgentState newState)
    {
        if (states.Contains(currentState))//We are already in this state, no need to restart
        {
            return;
        }
        if (states.Contains(newState))//We are entering a valid state
        {
            StartCoroutine(RunTaskLoop());
            Debug.Log($"Agent: Start Sensor {GetType().Name} - {name}", gameObject);
        }
    }

    private void OnAgentExitState(AgentState currentState, AgentState newState)
    {
        if (states.Contains(newState))//We are entering a valid state, so no need to restart
        {
            return;
        }
        if (states.Contains(currentState))//We are exiting a valid state, so we must stop our task
        {
            StopAllCoroutines();
        }
    }


    protected IEnumerator RunTaskLoop()
    {
        do
        {
            yield return new WaitForSeconds(updateTime);
            yield return RunTask(agent);
        } while (LoopBehavior);
    }

    public abstract IEnumerator RunTask(AgentController agent);

}

