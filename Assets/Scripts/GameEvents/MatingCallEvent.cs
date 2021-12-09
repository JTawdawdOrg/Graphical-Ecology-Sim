using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mating game event", menuName = "Mating event")]

public class MatingCallEvent : ScriptableObject
{
    private HashSet<Reproduce> listeners = new HashSet<Reproduce>();

    public void MatingCall(Vector3 pos, bool isMale)
    {
        foreach(var GameObjectListener in listeners)
        {
            GameObjectListener.Response(pos, isMale);
        }
    }

    public void Register(Reproduce gameEventListener)
    {
        listeners.Add(gameEventListener);
    }

    public void Deregister(Reproduce gameEventListener)
    {
        listeners.Remove(gameEventListener);
    }

}
