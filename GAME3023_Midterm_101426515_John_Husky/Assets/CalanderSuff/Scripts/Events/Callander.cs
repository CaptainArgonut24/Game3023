using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public abstract class Callander : ScriptableObject
{
    //public string eventName;
    public Calender.DateTime eventDate;

    public abstract void TriggerEvent(MainCalanderSystem);
}
