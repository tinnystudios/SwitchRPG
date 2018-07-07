using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public void Switch()
    {
        var newCharacter = CharacterSwitchManager.Instance.GetNewCharacter(gameObject);
        newCharacter.transform.SetPositionAndRotation(transform.position, transform.rotation);
        newCharacter.SetActive(true);
        gameObject.SetActive(false);
    }
}

