using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public CharacterSwitchSettings mSwitchSettings;

    public void Switch()
    {
        var newCharacter = mSwitchSettings.GetNewCharacter(gameObject);

        Instantiate(newCharacter, transform.position,transform.rotation,transform.parent);
        Destroy(gameObject);
    }
}

