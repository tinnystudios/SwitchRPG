using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchManager : MonoBehaviour
{
    private static CharacterSwitchManager mInstance;

    public static CharacterSwitchManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<CharacterSwitchManager>();
            }

            return mInstance;
        }
    }

    public Dictionary<GameObject, GameObject> mLookUp = new Dictionary<GameObject, GameObject>();
    public CharacterSwitchSettings mSwitchSettings;
    public int currentIndex = 0;

    private void Awake()
    {
        var character = FindObjectOfType<PlayerController>();
        Destroy(character.gameObject);

        for (int i = 0; i < mSwitchSettings.mCharacters.Length; i++)
        {
            var element = mSwitchSettings.mCharacters[i];
            element.SetActive(false);

            var instance = Instantiate(element, character.transform.position, character.transform.rotation, character.transform.parent);
            mLookUp.Add(element, instance);

            element.SetActive(true);

            if (i == 0)
                instance.SetActive(true);
        }

        

    }

    public GameObject GetNewCharacter(GameObject currentCharacter)
    {
        var mCharacters = mSwitchSettings.mCharacters;
        currentIndex++;

        if (currentIndex >= mCharacters.Length) currentIndex = 0;

        var character = mLookUp[mCharacters[currentIndex]];

        if (currentCharacter == character)
        {
            currentIndex++;
            if (currentIndex >= mCharacters.Length) currentIndex = 0;
        }

        return mLookUp[mCharacters[currentIndex]];
    }

}
