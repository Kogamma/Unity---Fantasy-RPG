using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private int nrOfNotes = 4;              // The amount of notes that will appear
    private int missDist = 32;              // The minimum distance between notes and targets that gives a miss 
    private int goodDist = 18;              // The minimum distance between notes and targets that gives a 'good' 
    private int greatDist = 4;              // The minimum distance between notes and targets that gives a 'great' 
    private int excellentDist = 0;          // The minimum distance between notes and targets that gives a 'excellent'
    private int notesInPlay = 0;            // The amounts of notes active in the combo
    private int notesFinished = 0;          // Number of notes that is either hit or missed
    private int critChance = 10;            // The probability of a crit note appears
    private int rndPath = -1;               // The randomized path that a nothe will follow
    private int rndHolder = 0;              // Will hold the randomized number before it is set to the 'rndPath'
    private int rndResetCount = 0;          // Number of times that the randomization will be reset
    private float noteSpeed = 0.3f;         // Speed of the notes
    private float baseInterval = 0.5f;      // The base value of the amount of time that the notes will spawn between eachother
    private float interval = 0.5f;          // The actual amount of time that the notes will spawn between eachother
    private float goodHit = 0;              // Number of notes hit by the player with an 'good' score
    private float greatHit = 0;             // Number of notes hit by the player with a 'great' score
    private float excellentHit = 0;         // Number of notes hit by the player with a 'excellent' score
    private float critHit = 0;              // Number of crit-notes hit by the player
    private float hitAccuracy = 0f;         // The percentage of how many notes the player hit and how well they were hit
    private float timer = 0;                // A timer that counts the time for spawning the notes
    private float removeDelay = 2.1f;       // The delay time before removing the combo system and adding accuracy to the player
    private float removeTimer = 0f;         // A timer that counts time for when to remove the combo system
    private float leftBorderPos;            // The left side of the note background
    private bool shouldSpawn = true;        // Should more notes be spawned

    private GameObject noteInstance;                // The note gameobject that will be instantiated
    [SerializeField] private GameObject note;       // A normal note that the player must hit
    [SerializeField] private GameObject critNote;   // Rare notes that gives extra damage
    [SerializeField] private GameObject redCross;   // A cross that will cover missed notes
    [SerializeField] private GameObject player;     // The player
    [SerializeField] private Transform leftBorder;  // A transform that holds the position of the left border of the combo background

    private List<GameObject> notesY = new List<GameObject>();               // All the notes in the Y-row
    private List<GameObject> notesX = new List<GameObject>();               // All the notes in the X-row
    private List<GameObject> notesB = new List<GameObject>();               // All the notes in the B-row
    private List<GameObject> notesA = new List<GameObject>();               // All the notes in the A-row
    private List<List<GameObject>> noteRows = new List<List<GameObject>>(); // A list containing all above rows for notes
    [SerializeField] private GameObject[] targets = new GameObject[4];      // The positions that the notes must be near when the player hits the corresponding buttons

    // All button images for Xbox controlls
    [SerializeField] private Sprite buttonA;
    [SerializeField] private Sprite buttonA_pressed;
    [SerializeField] private Sprite buttonB;
    [SerializeField] private Sprite buttonB_pressed;
    [SerializeField] private Sprite buttonX;
    [SerializeField] private Sprite buttonX_pressed;
    [SerializeField] private Sprite buttonY;
    [SerializeField] private Sprite buttonY_pressed;

    // All texts displaying how well the player hit a note
    [SerializeField] private GameObject missText;
    [SerializeField] private GameObject goodText;
    [SerializeField] private GameObject greatText;
    [SerializeField] private GameObject excellentText;
    [SerializeField] private GameObject critText;

    // All sound effects
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip critHitSound;
    [SerializeField] private AudioClip missSound;
    private AudioSource audioSource;


    void Start ()
    {
        // Adds the note rows in the list
        noteRows.Add(notesY);
        noteRows.Add(notesX);
        noteRows.Add(notesB);
        noteRows.Add(notesA);

        audioSource = GetComponent<AudioSource>();
    }
	

	void Update ()
    {
        // If the timer has reached the interval delay and there is less nothes in play than the amount requested
        if (timer > interval && shouldSpawn)
        {
            // Resets interval to it's base value
            interval = baseInterval;

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
            
            // Randomizes if a crit-note should spawn based on the crit-chance
            if (Random.Range(0, 100 / critChance) == 0)
                noteInstance = critNote;
            else
                noteInstance = note;

            // Instantiates a note and adds it to the randomized row (list) in the row-list
            noteRows[rndPath].Add(Instantiate(noteInstance, transform.GetChild(rndPath).transform.position, transform.rotation, transform.GetChild(rndPath + 4).transform));
            notesInPlay++;

            // Notes will no longer spawn if the amount of active notes is equal to the requested amount
            if (notesInPlay == nrOfNotes)
                shouldSpawn = false;

            // Makes the next interval less or more for variation 
            interval *= Random.Range(0.7f, 1.3f);

            // Resets randomization reset-counter and the spawn timer
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
                noteRows[i][j].transform.Translate(GetComponent<RectTransform>().sizeDelta.x * -noteSpeed * Time.deltaTime, 0f, 0f);

                // If a note goes past the left border of the combo background
                if (noteRows[i][j].transform.position.x <= leftBorder.position.x)
                    StartCoroutine(RemoveNote(i));
            }
        }

        // When a button is pressed, the distance of the corresponding target and first note in its corresponding row will be checked 
        if (Input.GetButtonDown("Y/Up"))
            CheckNoteDist(0);
        else if (Input.GetButtonDown("X/Left"))
            CheckNoteDist(1);
        else if (Input.GetButtonDown("B/Right"))
            CheckNoteDist(2);
        else if (Input.GetButtonDown("A/Down"))
            CheckNoteDist(3);

        // Changes the button images depending on the player presses them or not
        if (Input.GetButton("Y/Up"))
            targets[0].GetComponent<Image>().sprite = buttonY_pressed;
        else
            targets[0].GetComponent<Image>().sprite = buttonY;

        if (Input.GetButton("X/Left"))
            targets[1].GetComponent<Image>().sprite = buttonX_pressed;
        else
            targets[1].GetComponent<Image>().sprite = buttonX;

        if (Input.GetButton("B/Right"))
            targets[2].GetComponent<Image>().sprite = buttonB_pressed;
        else
            targets[2].GetComponent<Image>().sprite = buttonB;

        if (Input.GetButton("A/Down"))
            targets[3].GetComponent<Image>().sprite = buttonA_pressed;
        else
            targets[3].GetComponent<Image>().sprite = buttonA;

        // If all notes are out of play
        if (notesFinished == nrOfNotes)
        {
            // If the timer has reached its delay
            if (removeTimer >= removeDelay)
            {
                // Resets timer
                removeTimer = 0;

                // Calculates the procentual hit accuracy
                hitAccuracy += (critHit / nrOfNotes) * 1.5f;
                hitAccuracy += excellentHit / nrOfNotes;
                hitAccuracy += (greatHit / nrOfNotes) / 1.5f;
                hitAccuracy += (goodHit / nrOfNotes) / 2;

                /*print("Hit Accuracy: " + hitAccuracy);
                print("Crit hit: " + critHit);
                print("Excellent hit: " + excellentHit);
                print("Great hit: " + greatHit);
                print("Good hit: " + goodHit);*/

                // Sets the calculated hit accuracy to the players hit accuracy
                player.GetComponent<PlayerCombatLogic>().hitAccuracy = hitAccuracy;

                // Tells the player that the combo is now finished
                player.GetComponent<PlayerCombatLogic>().comboIsDone = true;

                // Deactivates the combo system
                gameObject.SetActive(false);
            }
            else
                removeTimer += Time.deltaTime;
        }
        
    }
    
    
    public void ActivateCombo (int _nrOfNotes, float _noteSpeed, float _interval, int _critChance)
    {
        // Resets variables
        shouldSpawn = true;
        hitAccuracy = 0;
        excellentHit = 0;
        greatHit = 0;
        goodHit = 0;
        notesFinished = 0;
        notesInPlay = 0;

        // Assigns the variables that will difference the attacks from eachother
        nrOfNotes = _nrOfNotes;
        noteSpeed = _noteSpeed;
        baseInterval = _interval;
        critChance = _critChance;
    }


    void CheckNoteDist (int targetIndex)
    {
        // Gets the position of the note target
        Vector3 targetPos = targets[targetIndex].transform.position;
        Vector3 targetAnchorPos = targets[targetIndex].GetComponent<RectTransform>().anchoredPosition;


        try {
            // If a note exits on the first index of the target row
            if (noteRows[targetIndex][0] != null)
            {
                // Gets the position of the first note in the row
                Vector3 notePos = noteRows[targetIndex][0].transform.position;
                Vector3 noteAnchorPos = noteRows[targetIndex][0].GetComponent<RectTransform>().anchoredPosition;

                // If the distance between the target and the note is greater than the minimum distance that gives a miss
                if (Vector3.Distance(targetAnchorPos, noteAnchorPos) >= missDist)
                {
                    // Instantiates a miss-text to the left of the target
                    Instantiate(missText, new Vector3(leftBorder.position.x - 40f, targetPos.y), transform.rotation, transform);

                    // Instantiates a cross on the left note showing it was missed
                    Instantiate(redCross, notePos, transform.rotation, noteRows[targetIndex][0].transform);

                    audioSource.PlayOneShot(missSound, 1f);

                    // Removes the note
                    StartCoroutine(RemoveNote(targetIndex));
                }

                // Distance check for normal notes
                if (noteRows[targetIndex][0].tag != "Crit Note")
                {
                    // If the distance between the target and the note is greater than the minimum distance that gives a good hit and is less than the minimum miss distance
                    if (Vector3.Distance(targetAnchorPos, noteAnchorPos) >= goodDist && Vector3.Distance(targetAnchorPos, noteAnchorPos) <= missDist)
                    {
                        // Instantiates a good-text to the left of the target
                        Instantiate(goodText, new Vector3(leftBorder.position.x - 40f, targetPos.y), transform.rotation, transform);

                        audioSource.PlayOneShot(hitSound, 1f);

                        // REmoves note
                        StartCoroutine(RemoveNote(targetIndex));

                        goodHit++;
                    }
                    // If the distance between the target and the note is greater than the minimum distance that gives a great hit and is less than the minimum good distance
                    else if (Vector3.Distance(targetAnchorPos, noteAnchorPos) >= greatDist && Vector3.Distance(targetAnchorPos, noteAnchorPos) <= goodDist)
                    {
                        // Instantiates a great-text to the left of the target
                        Instantiate(greatText, new Vector3(leftBorder.position.x - 40f, targetPos.y), transform.rotation, transform);

                        audioSource.PlayOneShot(hitSound, 1f);

                        // Removes note
                        StartCoroutine(RemoveNote(targetIndex));

                        greatHit++;
                    }
                    // If the distance between the target and the note is greater than the minimum distance that gives an excellent hit and is less than the minimum great distance
                    else if (Vector3.Distance(targetAnchorPos, noteAnchorPos) >= excellentDist && Vector3.Distance(targetAnchorPos, noteAnchorPos) <= greatDist)
                    {
                        // Instantiates an excellent-text to the left of the target
                        Instantiate(excellentText, new Vector3(leftBorder.position.x - 40f, targetPos.y), transform.rotation, transform);

                        audioSource.PlayOneShot(hitSound, 1f);

                        // Removes note
                        StartCoroutine(RemoveNote(targetIndex));

                        excellentHit++;
                    }
                }
                // Distance check for crit-notes
                else
                {
                    // If the crit-note is within the target
                    if (Vector3.Distance(targetAnchorPos, noteAnchorPos) >= 0 && Vector3.Distance(targetAnchorPos, noteAnchorPos) <= missDist)
                    {
                        // Instantiates an excellent-text to the left of the target
                        Instantiate(critText, new Vector3(leftBorder.position.x - 40f, targetPos.y), transform.rotation, transform);

                        audioSource.PlayOneShot(critHitSound, 1f);

                        // Removes note
                        StartCoroutine(RemoveNote(targetIndex));

                        critHit++;
                    }
                }
            }
        } catch (System.ArgumentOutOfRangeException)
        {
        }
    }


    private IEnumerator RemoveNote(int rowIndex)
    {
        notesFinished++;

        // Plays fade animation
        noteRows[rowIndex][0].GetComponent<Animator>().Play("TextFade");

        // Creates a reference holding the note to destroy
        GameObject noteToDestroy = noteRows[rowIndex][0];

        // Removes the note from the list
        noteRows[rowIndex].RemoveAt(0);

        // Waits two seconds, the time for the animation to reach its end, then destroys the note
        yield return new WaitForSeconds(2f);
        Destroy(noteToDestroy);
    }
}
