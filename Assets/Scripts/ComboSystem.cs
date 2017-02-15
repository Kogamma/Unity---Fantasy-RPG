using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private int nrOfNotes = 4;              // The amount of notes that will appear
    private int missDist = 32;              // The minimum distance between notes and goals that gives a miss 
    private int goodDist = 18;              // The minimum distance between notes and goals that gives a 'good' 
    private int greatDist = 4;             // The minimum distance between notes and goals that gives a 'great' 
    private int excellentDist = 0;          // The minimum distance between notes and goals that gives a 'excellent'
    private int notesInPlay = 0;
    private int notesFinished = 0;          // Number of notes that is either hit or missed
    private int noteSpeed = 150;            // Speed of the notes
    private int critChance = 1;             // The probability of a crit note appears
    private int rndPath = -1;               // The randomized path that a nothe will follow
    private int rndHolder = 0;              // Will hold the randomized number before it is set to the 'rndPath'
    private int rndResetCount = 0;          // Number of times that the randomization will be reset
    private float interval = 0.5f;          // The amount of time that the notes will spawn between eachother
    private float goodHit = 0;              // Number of notes hit by the player with an 'good' score
    private float greatHit = 0;             // Number of notes hit by the player with a 'great' score
    private float excellentHit = 0;         // Number of notes hit by the player with a 'excellent' score
    private float hitAccuracy = 0f;         // The percentage of how many notes the player hit and how well they were hit
    private float timer = 0;                // A timer that counts the time for spawning the notes
    private float removeDelay = 2.1f;
    private float removeTimer = 0f;
    private float leftBorderPos;            // The left side of the note background
    private bool shouldSpawn = true;        // Should more notes be spawned
   
    [SerializeField] private GameObject note;                       // The note gameobject that will be instantiated
    [SerializeField] private GameObject redCross;                   // A cross that will cover missed notes
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] goals = new GameObject[4];// The positions that the notes must be near when the player hits the corresponding buttons
    [SerializeField] private Transform noteKillPos;                 // A transform that holds which x-position the notes will be destroyed at
    private List<GameObject> notesY = new List<GameObject>();        // All the notes in the Y-row
    private List<GameObject> notesX = new List<GameObject>();        // All the notes in the X-row
    private List<GameObject> notesB = new List<GameObject>();        // All the notes in B-row
    private List<GameObject> notesA = new List<GameObject>();        // All the notes in A-row
    private List<List<GameObject>> noteRows = new List<List<GameObject>>();

    [SerializeField] private Sprite buttonA;
    [SerializeField] private Sprite buttonA_pressed;
    [SerializeField] private Sprite buttonB;
    [SerializeField] private Sprite buttonB_pressed;
    [SerializeField] private Sprite buttonX;
    [SerializeField] private Sprite buttonX_pressed;
    [SerializeField] private Sprite buttonY;
    [SerializeField] private Sprite buttonY_pressed;

    [SerializeField] private GameObject missText;
    [SerializeField] private GameObject goodText;
    [SerializeField] private GameObject greatText;
    [SerializeField] private GameObject excellentText;


    void Start ()
    {
        noteRows.Add(notesY);
        noteRows.Add(notesX);
        noteRows.Add(notesB);
        noteRows.Add(notesA);
    }
	

	void Update ()
    {
        // If the timer has reached the interval delay and there is less nothes in play than the amount requested
        if (timer > interval && shouldSpawn)
        {
            // While the randomization hasn't been reset 3 times
            while (rndResetCount < 3)
            {
                // Randomizes a number between 0 and 3
                rndHolder = Random.Range(0, 4);

                // Resets randomization if the randomized number is equal to the last randomized path
                // This lowers the chance of notes appearing on the same path
                if (rndHolder == rndPath)
                {
                    rndResetCount++;
                    continue;
                }
                else
                {
                    rndPath = rndHolder;
                    break;
                }
            }

            // Instantiates a note and adds it to the randomized row (list) in the row-list
            noteRows[rndPath].Add(Instantiate(note, transform.GetChild(rndPath).transform.position, transform.rotation, transform.GetChild(rndPath + 4).transform));

            notesInPlay++;

            if (notesInPlay == nrOfNotes)
                shouldSpawn = false;

            // Resets randomization reset counter and the spawn timer
            rndResetCount = 0;
            timer = 0;
        }
        else
            timer += Time.deltaTime;

        // Updates all the notes
        for (int i = 0; i < noteRows.Count; i++)
        {
            for (int j = 0; j < noteRows[i].Count; j++)
            {
                // Moves notes to the left
                noteRows[i][j].transform.Translate(-noteSpeed * Time.deltaTime, 0f, 0f);

                // If a note goes past the left border of the combo background
                if (noteRows[i][j].transform.position.x <= noteKillPos.position.x)
                {
                    StartCoroutine(RemoveNote(i));
                }
            }
        }

        if (Input.GetButtonDown("Y/Up"))
            CheckNoteDist(0);
        else if (Input.GetButtonDown("X/Left"))
            CheckNoteDist(1);
        else if (Input.GetButtonDown("B/Right"))
            CheckNoteDist(2);
        else if (Input.GetButtonDown("A/Down"))
            CheckNoteDist(3);

        if (Input.GetButton("Y/Up"))
            goals[0].GetComponent<Image>().sprite = buttonY_pressed;
        else
            goals[0].GetComponent<Image>().sprite = buttonY;

        if (Input.GetButton("X/Left"))
            goals[1].GetComponent<Image>().sprite = buttonX_pressed;
        else
            goals[1].GetComponent<Image>().sprite = buttonX;

        if (Input.GetButton("B/Right"))
            goals[2].GetComponent<Image>().sprite = buttonB_pressed;
        else
            goals[2].GetComponent<Image>().sprite = buttonB;

        if (Input.GetButton("A/Down"))
            goals[3].GetComponent<Image>().sprite = buttonA_pressed;
        else
            goals[3].GetComponent<Image>().sprite = buttonA;

        if (notesFinished == nrOfNotes)
        {
            if (removeTimer >= removeDelay)
            {
                removeTimer = 0;

                hitAccuracy += excellentHit / nrOfNotes;
                hitAccuracy += (greatHit / nrOfNotes) / 1.5f;
                hitAccuracy += (goodHit / nrOfNotes) / 2;
                print("Hit Accuracy: " + hitAccuracy);
                print("Excellent hit: " + excellentHit);
                print("Great hit: " + greatHit);
                print("Good hit: " + goodHit);

                player.GetComponent<PlayerCombatLogic>().hitAccuracy = hitAccuracy;
                player.GetComponent<PlayerCombatLogic>().comboIsDone = true;
                gameObject.SetActive(false);
            }
            else
                removeTimer += Time.deltaTime;
        }
        
    }
    

    public void ActivateCombo (int _nrOfNotes, int _noteSpeed, float _interval, int _critChance)
    {
        shouldSpawn = true;
        hitAccuracy = 0;
        excellentHit = 0;
        greatHit = 0;
        goodHit = 0;
        notesFinished = 0;
        notesInPlay = 0;

        // Assigns the variables that will differense the attacks from eachother
        nrOfNotes = _nrOfNotes;
        noteSpeed = _noteSpeed;
        interval = _interval;
        critChance = _critChance;
         
    }

    void CheckNoteDist (int goalIndex)
    {
        Vector3 goalPos = goals[goalIndex].transform.position;

        if (noteRows[goalIndex][0] != null)
        {
            Vector3 notePos = noteRows[goalIndex][0].transform.position;

            if (Vector3.Distance(goalPos, notePos) >= missDist)
            {
                Instantiate(missText, new Vector3(goalPos.x - 70f, goalPos.y), transform.rotation, transform);
                Instantiate(redCross, notePos, transform.rotation, noteRows[goalIndex][0].transform);
                StartCoroutine(RemoveNote(goalIndex));
            }
            else if (Vector3.Distance(goalPos, notePos) >= goodDist && Vector3.Distance(goalPos, notePos) <= missDist)
            {
                Instantiate(goodText, new Vector3(goalPos.x - 70f, goalPos.y), transform.rotation, transform);
                StartCoroutine(RemoveNote(goalIndex));
                goodHit++;
            }
            else if (Vector3.Distance(goalPos, notePos) >= greatDist && Vector3.Distance(goalPos, notePos) <= goodDist)
            {
                Instantiate(greatText, new Vector3(goalPos.x - 70f, goalPos.y), transform.rotation, transform);
                StartCoroutine(RemoveNote(goalIndex));
                greatHit++;
            }
            else if (Vector3.Distance(goalPos, notePos) >= excellentDist && Vector3.Distance(goalPos, notePos) <= greatDist)
            {
                Instantiate(excellentText, new Vector3(goalPos.x - 70f, goalPos.y), transform.rotation, transform);
                StartCoroutine(RemoveNote(goalIndex));
                excellentHit++;
            }
        }
    }

    private IEnumerator RemoveNote(int rowIndex)
    {
        notesFinished++;
        noteRows[rowIndex][0].GetComponent<Animator>().Play("TextFade");
        GameObject noteToDestroy = noteRows[rowIndex][0];
        noteRows[rowIndex].RemoveAt(0);
        yield return new WaitForSeconds(2f);
        Destroy(noteToDestroy);
    }
}
