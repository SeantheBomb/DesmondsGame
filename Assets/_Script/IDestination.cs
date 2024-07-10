using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestination
{
    void SetDestination(Vector2 destination);

    void ClearDestination();

    bool IsDestinationReached();
}
