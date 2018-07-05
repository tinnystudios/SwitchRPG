using UnityEngine;

[CreateAssetMenu(menuName = "SwitchRPG/SwitchSettings")]
public class CharacterSwitchSettings : ScriptableObject
{
    public GameObject[] mCharacters;

    private int currentIndex = 0;

    public GameObject GetNewCharacter(GameObject currentCharacter)
    {
        currentIndex++;
        if (currentIndex >= mCharacters.Length) currentIndex = 0;

        if (currentCharacter == mCharacters[currentIndex])
        {
            currentIndex++;
            if (currentIndex >= mCharacters.Length) currentIndex = 0;
        }

        return mCharacters[currentIndex];
    }
}
