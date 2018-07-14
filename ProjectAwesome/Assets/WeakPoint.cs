using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IWeakPoint
{
    public float percentage = 5;

    public float Percentage
    {
        get
        {
            return percentage;  
        }

    }
}


public interface IWeakPoint
{
    float Percentage { get;}
}