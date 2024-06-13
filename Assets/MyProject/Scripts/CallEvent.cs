using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEvent : MonoBehaviour
{
    [SerializeField] UnityEvent action;

    public void Action()
    {
        action.Invoke();
    }

}
