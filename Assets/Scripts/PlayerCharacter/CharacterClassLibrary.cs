using UnityEngine;
using System.Collections.Generic;

public class CharacterClassLibrary : MonoBehaviour
{
    // Singleton class
    private static CharacterClassLibrary instance;
    public static CharacterClassLibrary Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterClassLibrary>();
            }

            return instance;
        }
    }

    // Serialized array for setting global values in the editor
    [SerializeField]
    private CharacterClass[] CharacterClasses;

    // Easily read character class dictionary collection for runtime scripting
    private Dictionary<CharacterClassName, CharacterClass> mCharacterClassDictionary;
    public Dictionary<CharacterClassName, CharacterClass> CharacterClassDictionary
    {
        get
        {
            return mCharacterClassDictionary;
        }
    }

    /// <summary>
    /// Initialize character class dictionary from serialized values
    /// </summary>
    void Start()
    {
        mCharacterClassDictionary = new Dictionary<CharacterClassName, CharacterClass>();

        foreach (CharacterClass chc in CharacterClasses)
        {
            mCharacterClassDictionary.Add(chc.ClassName, chc);
        }
    }
}
