using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class AgentManager : Singleton<AgentManager>
{

    [SerializeField] FloatVariable actionsPerStep = 1;
    [SerializeField] FloatVariable timePerStep = 1; 


    List<AgentController> agents = new List<AgentController>();

    Queue<AgentAction> actions = new Queue<AgentAction>();

    public GameObject Player;

    //public AgentAction[] debugActions;


    private void OnEnable()
    {
        if (Player == null) Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DoActionStep());
    }

    IEnumerator DoActionStep()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(timePerStep);
            for (int i = 0; i < actionsPerStep; i++)
            {
                DoNextTask();
                //yield return null;
                //debugActions = actions.ToArray();
            }
        }
    }

    public void AddAgent(AgentController agent)
    {
        agents.Add(agent);
    }

    public void RemoveAgent(AgentController agent)
    {
        agents.Remove(agent);
    }

    public IEnumerator ScheduleTask(AgentAction action)
    {
        actions.Enqueue(action);
        //debugActions = actions.ToArray();
        yield return new WaitWhile(() => actions.Contains(action));
    }

    public void RemoveTask(AgentAction action)
    {
        List<AgentAction> list = actions.ToList();
        list.Remove(action);
        actions.Clear();
        actions = new Queue<AgentAction>(list);
    }

    public void ClearTasksFrom(AgentController agent)
    {
        List<AgentAction> list = actions.ToList();
        List<AgentAction> toRemove = new List<AgentAction>();
        foreach(AgentAction aa in list)
        {
            if (aa.agent == agent)
                toRemove.Add(aa);
        }
        foreach(AgentAction aa in toRemove)
        {
            list.Remove(aa);
        }
        toRemove.Clear();
        actions.Clear();
        actions = new Queue<AgentAction>(list);
    }

    public void DoNextTask()
    {
        
        //Debug.Log($"Agent: Start Task");
        AgentAction action = null;
        do
        {
            if (actions.Count <= 0)
                return;
            action = actions.Dequeue();
        } while (action.agent == null || action.agent.enabled == false); //Keep looking until we find a valid agent
        action.agent.StartCoroutine(action.action);
    }
}


[System.Serializable]
public class AgentAction
{
    public AgentController agent;
    public IEnumerator action;
}