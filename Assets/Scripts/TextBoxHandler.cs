using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// To make sure we have a text component on the object
[RequireComponent(typeof(Text))]
public class TextBoxHandler : MonoBehaviour
{

    // Our text component that is on the same gameObject as this script
    private Text _textComponent;

    // Here we can add text to the dialogue, each string element is a page of textbox/dialogue
    private string[] _textBoxStrings;

    // Text object for the "speaker" of the text box
    public Text nameText;

    // How much time between the print of each character of text
    [Range(0.01f, 0.1f, order = 0)]
    [Header("How long between each character print")]
    public float characterPrintInterval = 0.1f;

    [Range(0.01f, 1f, order = 0)]
    [Header("Modifies the value of characterPrintInterval when we hold down 'A'.")]
    [Header("The lower the number, the faster.")]
    public float printSpeedup = 0.1f;


    public KeyCode continueInput = KeyCode.Return;
    public KeyCode closeBoxInput = KeyCode.Return;

    // If we are currently revealing a string
    private bool _stringIsBeingRevealed = false;
    // If the textbox is playing
    private bool _textIsPlaying = false;
    // Checks if we're playing the last page of text
    private bool _isEndOfText = false;

    private bool finishedText = false;

    // Icons for the textbox
    public Image ContinueIcon;
    public Image StopIcon;
    public Image Border;
    public Image Background;

    private AudioSource _audioSource;

    // The sound when playing text
    public AudioClip textClip;
    // An audio clip for going to the next page
    public AudioClip nextPageClip;

    // How many character prints between each sound played
    [Header("How often you want textSound to play.")]
    [Header("The lower, the more often it plays.")]
    [Range(1, 10)]
    public int soundInterval = 2;

    // Used for counting when to play the sound
    private int _soundCounter = 0;

    private GameObject _methodCaller;
    private string _methodToInvoke = "";

    void Start()
    {
        // Gets our textcomponent and clears it from all text
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";

        nameText.text = "";

        _audioSource = GetComponent<AudioSource>();

        // Hides start/continue buttons
        ContinueIcon.enabled = false;
        StopIcon.enabled = false;

        // Hides textbox
        Border.enabled = false;
        Background.enabled = false;
    }

    public void StartMessage(string[] textPages, string messagerName, GameObject methodHolder, string invokeMethod)
    {
     

        // Checks if we're not already playing some text
        if (!_textIsPlaying)
        {
            PlayerSingleton.instance.canMove = false;

            _methodCaller = methodHolder;
            _methodToInvoke = invokeMethod;

            // Makes the textbox visible
            Border.enabled = true;
            Background.enabled = true;

            _textBoxStrings = textPages;

            // Gets our textcomponent and clears it from all text
            _textComponent = GetComponent<Text>();
            _textComponent.text = "";

            nameText.text = "";

            // Sets the name of the "speaker" of the text box
            nameText.text = messagerName;

            _textIsPlaying = true;

            // Starts a textbox
            StartCoroutine(WaitForTextFinish());
        }
    }

    private IEnumerator WaitForTextFinish()
    {
        StartCoroutine(StartTextBox());

        while (!finishedText)
        {          
            yield return null;
        }

        PlayerSingleton.instance.canMove = true;

        if (_methodCaller != null)
            if (_methodToInvoke != null)
                _methodCaller.SendMessage(_methodToInvoke);

        _methodCaller = null;
        _methodToInvoke = null;
    }

    private IEnumerator StartTextBox()
    {
        // Sets how many pages of text there are
        int textBoxLength = _textBoxStrings.Length;
        // Which page we're currently on
        int currentTextPage = 0;

        // When we're not revealing a string or we're not on the last page of text
        while (currentTextPage < textBoxLength || !_stringIsBeingRevealed)
        {
            // When a string is not being revealed/played
            if (!_stringIsBeingRevealed)
            {
                // If we're on the last page of text
                if (currentTextPage + 1 >= textBoxLength)
                {
                    _isEndOfText = true;
                }

                // We will start revealing string again
                _stringIsBeingRevealed = true;
                StartCoroutine(DisplayString(_textBoxStrings[currentTextPage++]));          
            }

            yield return 0;
        }

    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        // Current character in the string to write next
        int currentCharIndex = 0;

        // Clears textcomponent
        _textComponent.text = "";

        // Loops through all characters of the string 
        while (currentCharIndex < stringLength)
        {
            // The whole string to add, if we want special formatting to add 
            //whole words or formatting such as <b>word</b> all at once
            string stringToAdd = "";

            // Adds the two next chars to a string to see if we're doing some kind of formatting
            string nextTwoChars = ""; 
            nextTwoChars += stringToDisplay[currentCharIndex];
            if(currentCharIndex + 1 != stringLength)
                nextTwoChars += stringToDisplay[currentCharIndex + 1];

            // If we find the start of text formatting...
            if (nextTwoChars == "<<")
            {
                // We first add the formatting start
                stringToAdd += '<';

                // We then skip ahead two chars so we don't get our <words> like that, with breackets
                currentCharIndex += 2;

                while (true)
                {
                    // Gets the two next characters again
                    nextTwoChars = "";
                    nextTwoChars += stringToDisplay[currentCharIndex];
                    nextTwoChars += stringToDisplay[currentCharIndex + 1];

                    // Checks if we're at the end of our formatting
                    if (nextTwoChars == ">>")
                    {
                        // Skips ahead two characters
                        currentCharIndex += 2;
                        // Adds the end of the text formatting
                        stringToAdd += '>';
                        break;
                    }
                    // Adds all of the characters we will display later to a string
                    stringToAdd += stringToDisplay[currentCharIndex];

                    // Goes to the next character
                    currentCharIndex++;
                }
            }
            else
                stringToAdd += stringToDisplay[currentCharIndex];

            // Adds our current character from the string, to our textcomponent
            _textComponent.text += stringToAdd;

            // Plays a sound when revealing a character
            if (_soundCounter >= soundInterval)
            {
                _soundCounter = 0;

                _audioSource.pitch = Random.Range(0.9f, 1f);
                _audioSource.PlayOneShot(textClip, 1f);
            }
            else
                _soundCounter++;
            // Increments the character index to get the next character in the string
            currentCharIndex++;

            // When we still have characters in the string left to display
            if(currentCharIndex < stringLength)
            {
                if (Input.GetKey(continueInput))
                {
                    // Prints at a higher speed
                    yield return new WaitForSeconds(characterPrintInterval * printSpeedup);
                }
                else
                {
                    // Prints at normal speed
                    yield return new WaitForSeconds(characterPrintInterval);
                }
            }
            else
            {
                break;
            }   
        }

        ShowIcons();

        // When the whole string has been displayed...
        while (true)
        {
            // We go to the next page when we press the textBoxInput
            if (Input.GetKeyDown(continueInput))
            {          
                    
                break;
            }

            // ...the string will be paused so the player can read it completely
            yield return 0;
        }
        HideIcons();

        // We are currently not revealing text
        _stringIsBeingRevealed = false;

        // Clears textcomponent
        if (!_isEndOfText)
        {
            _textComponent.text = "";
            finishedText = true;
        }
        else
        {
            // Makes the textbox invisible
            _textComponent.text = "";
            Border.enabled = false;
            Background.enabled = false;
            nameText.text = "";

            _isEndOfText = false;
            _textIsPlaying = false;
        }
    }

    // Hides the continue/stop icons
    private void HideIcons()
    {
        _audioSource.PlayOneShot(nextPageClip, 1f);

        ContinueIcon.enabled = false;
        StopIcon.enabled = false;
    }

    private void ShowIcons()
    {
        // Shows the red stop icon if we're on the last page of text
        if(_isEndOfText)
        {
            StopIcon.enabled = true;
            return;
        }
        // Shows the green continue icon for showing the next page
        ContinueIcon.enabled = true;
    }
}

