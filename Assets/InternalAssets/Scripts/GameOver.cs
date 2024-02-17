using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    public void OnTrigger(UnityEvent action)
    {
        action.Invoke();
    }
}
