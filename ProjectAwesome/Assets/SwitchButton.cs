using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour, IActivatable
{
    public SwitchLink mSwitchLink;

    public void Activate()
    {
        mSwitchLink.Activate();
    }
}

public interface IActivatable
{
    void Activate();
}