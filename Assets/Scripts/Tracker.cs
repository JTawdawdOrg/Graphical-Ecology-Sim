using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] public int VegTracker { get; private set; }
    [SerializeField] public int DeerTracker { get; private set; }
    [SerializeField] public int WolfTracker { get; private set; }

    public void IncrementVegTracker()
    {
        VegTracker++;
    }
    public void IncrementDeerTracker()
    {
        DeerTracker++;
    }
    public void IncrementWolfTracker()
    {
        WolfTracker++;
    }

    public void DecrementVegTracker()
    {
        VegTracker--;
    }
    public void DecrementDeerTracker()
    {
        DeerTracker--;
    }
    public void DecrementWolfTracker()
    {
        WolfTracker--;
    }
}
