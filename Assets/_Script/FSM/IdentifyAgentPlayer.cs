using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyAgentPlayer : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (AgentManager.Instance == null)
            return;
        if (target == null) target = gameObject;
        AgentManager.Instance.Player = target;
    }

    
}
