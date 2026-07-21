using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScaleEntry 
{
    public string scaleName;
    public List<ScriptableNote> notes;
    public bool isMinor;
}


public class ScaleRegistery : MonoBehaviour
{
    static public ScaleRegistery instance;

    [SerializeField] private List<ScriptableNote> notes;

    void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    public ScriptableNote GetNote(int index) { return notes[index]; }

    public ScaleEntry CreateRandomChord() 
    {
        ScaleEntry newEntry = new ScaleEntry();
        newEntry.notes = new List<ScriptableNote>();

        newEntry.isMinor = Random.value < 0.5f;

        int n = Random.Range(0, 3);
        int rC = 2 * n + 1;
        

        ScriptableNote lastNote = notes[Random.Range(0, notes.Count)];
        newEntry.notes.Add(lastNote);

        while(newEntry.notes.Count < rC) 
        {
            ScriptableNote nNote = notes[Random.Range(0, notes.Count)];

            bool errorFound = false;

            if (newEntry.isMinor) 
            {
                if (lastNote.ID == 0 && nNote.ID == 1 || lastNote.ID == 3 && nNote.ID == 4)
                    errorFound = true;
            }
            else 
            {
                if (lastNote.ID == 1 && nNote.ID == 2)
                    errorFound = true;
            }

            if (!errorFound) 
            {
                newEntry.notes.Add(nNote);
                lastNote = nNote;
            }
        }

        return newEntry;
    }

}
