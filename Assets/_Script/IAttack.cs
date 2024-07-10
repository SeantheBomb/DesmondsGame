using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{

    Action<string> OnAttack { get; set; }

    void StartAttack(bool displayOnly = false);

}
