using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private int nrOfNotes = 3;
    private int notesHit = 0;
    private int notesFinished = 0;
    private int noteSpeed = 10;
    private int critNoteChance = 1;
    private int rndPath = 0;
    private float interval = 0.5f;
    private float hitAccuracy = 0f;
    private float timer = 0;
    [SerializeField] GameObject note;
    private List<GameObject> notes = new List<GameObject>();
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    float ActivateCombo (int _nrOfNotes, int _noteSpeed, float _interval, int _critNoteChance)
    {
        nrOfNotes = _nrOfNotes;
        noteSpeed = _noteSpeed;
        interval = _interval;
        critNoteChance = _critNoteChance;

        while (notesFinished < nrOfNotes)
        {
            if (timer > interval)
            {
                rndPath = Random.Range(1, 5);

                notes.Add(Instantiate(note, new Vector2(transform.position.x + 100f, 30 * rndPath), transform.rotation, transform));
            }
            else
                timer += Time.deltaTime;
        }

        return hitAccuracy;
    }
}
