using Corrupted;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;


public class AgentController : CorruptedBehaviour<AgentController>
{
    /// <summary>
    /// The state we are moving from, the state we are moving into
    /// </summary>
    public System.Action<AgentState, AgentState> OnEnterState;


    /// <summary>
    /// The state we are in, and the state we are moving to
    /// </summary>
    public System.Action<AgentState, AgentState> OnExitState;


    [Header("States")]
    [SerializeField] AgentState startState;
    [ReadOnly]AgentState currentState;

    [Header("Target")]
    public Transform target;
    public Vector3 destination;

    public AgentState CurrentState => currentState;


    public void OnEnable()
    {
        StartCoroutine(DelayStart());
    }

    private void OnDisable()
    {
        currentState = null;
    }

    IEnumerator DelayStart()
    {
        yield return null;
        SetState(startState);
    }

    public void SetState(AgentState state)
    {
        if (state == null)
        {
            Debug.LogError($"Agent: {name} can not be set to state null", gameObject);
            return;
        }
        if(currentState == state)
        {
            return;
        }

        if (currentState != null)
        {
            OnExitState?.Invoke(currentState, state);
            Debug.Log($"Agent: {name} moving from {currentState.name} to {state.name}");
        }

        OnEnterState?.Invoke(currentState, state);


        currentState = state;


    }

    public void ClearActions()
    {
        AgentManager.Instance.RemoveAgent(this);
    }

    public IEnumerator ScheduleTask(IEnumerator task)
    {
        AgentAction action = new AgentAction();
        action.agent = this;
        action.action = task;
        yield return AgentManager.Instance.ScheduleTask(action);
    }

    public bool IsWithinLookRadius(Transform LookPoint, Transform target, float LookDistance)
    {
        Vector3 delta = target.position - LookPoint.position;
        if (delta.magnitude > LookDistance)
            return false;
        //if (Vector3.Dot(LookPoint.right, delta.normalized) < 0)
        //    return false;
        return true;
    }

    public bool HasLineOfSight(Transform LookPoint, Transform target, float LookDistance)
    {
        if (IsWithinLookRadius(LookPoint, target, LookDistance) == false)
            return false;
        Vector3 delta = target.position - LookPoint.position;
        Ray ray = new Ray(LookPoint.position, delta.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, LookDistance, Physics.AllLayers ,QueryTriggerInteraction.Ignore))
        {
            if (hit.transform == target || hit.transform.IsChildOf(target))
            {
                return true;
            }
        }
        return false;
    }


}

