using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// To make sure we have a text component on the object
[RequireComponent(typeof(Text))]
public class CombatTextBoxHandler : MonoBehaviour
{

    // Our text component that is on the same gameObject as this script
    private Text _textComponent;

    // Here we can add text to the dialogue, each string element is a page of textbox/dialogue
    [TextArea]
    private List<string> _textBoxStrings;

    // How much time between the print of each character of text
    [Range(0.01f, 0.1f, order = 0)]
    [Header("How long between each character print")]
    public float characterPrintInterval = 0.01f;
    
    // How much time to take before closing/going to the next page
    [Range(0.05f, 1.5f, order = 0)]
    [Header("How long between each new page")]
    public float newPageInterval = 0.75f;

    // If we are currently revealing a string
    private bool _stringIsBeingRevealed = false;
    // If the textbox is playing
    private bool _textIsPlaying = false;
    // Checks if we're playing the last page of text
    private bool _isEndOfText = false;

    // The text box images
    public Image Border;
    public Image Background;


    private AudioSource _audioSource;
    // The sound when playing text
    public AudioClip textSound;

    bool playSound = true;

    bool finishedText = false;

    private GameObject _methodCaller;
    private string _methodToInvoke = "";

    // How many character prints between each sound played
    [Header("How often you want textSound to play.")]
    [Header("The lower, the more often it plays.")]
    [Range(0, 10)]
    public int soundInterval = 2;

    // Used for counting when to play the sound
    private int _soundCounter = 0;

    void Start()
    {
        // Gets our textcomponent and clears it from all text
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";

        _audioSource = GetComponent<AudioSource>();

        // Hides textbox
        Border.enabled = false;
        Background.enabled = false;
    }

    // Call on this function to print a message of your choice
    public void PrintMessage(List<string> textPages, GameObject methodHolder, string invokeMethod)
    {
        // Checks if we're not already playing text
        if (!_textIsPlaying)
        {
            PlayerSingleton.instance.canMove = false;

            _methodCaller = methodHolder;
            _methodToInvoke = invokeMethod;

            _textBoxStrings = textPages;

            // Makes the textbox visible
            Border.enabled = true;
            Background.enabled = true;

            _textIsPlaying = true;
            finishedText = false;
            // Starts a textbox
            StartCoroutine(WaitForTextFinish());
        }
    }

    private IEnumerator WaitForTextFinish()
    {
        StartCoroutine(StartTextBox());

        while (!finishedText)
            yield return 0;

        PlayerSingleton.instance.canMove = true;

        if (_methodCaller != null)
            if (_methodToInvoke != null)
                _methodCaller.SendMessage(_methodToInvoke);

        _methodCaller = null;
        _methodToInvoke = null;

        finishedText = false;
    }

    private IEnumerator StartTextBox()
    {
        // Sets how many pages of text there are
        int textBoxLength = _textBoxStrings.Count;
        // Which page we're currently on
        int currentTextPage = 0;

        // When we're not revealing a string or we're not on the last page of text
        while (currentTextPage < textBoxLength || !_stringIsBeingRevealed)
        {
            // When a string is not being revealed/played
            if (!_stringIsBeingRevealed)
            {
                // We will start revealing string again
                _stringIsBeingRevealed = true;
                // Starts displaying the text of the page we are currently on
                StartCoroutine(DisplayString(_textBoxStrings[currentTextPage++]));

                // If we're going to be on the last page of text this iteration
                if (currentTextPage >= textBoxLength)
                {
                    _isEndOfText = true;
                }
            }

            yield return 0;
        }

        // This is when we're finished with the last page of text
        while (true)
        {
            // We close the last page of text
            if (!_stringIsBeingRevealed)
            {
                yield return new WaitForSeconds(newPageInterval * 2f);
                finishedText = true;

                break;
            }

            yield return 0;
        }

        // Makes the textbox invisible
        _textComponent.text = "";
        Border.enabled = false;
        Background.enabled = false;

        _isEndOfText = false;
        _textIsPlaying = false;
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
            if (currentCharIndex + 1 != stringLength)
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
            if (_soundCounter >= soundInterval && stringToDisplay[currentCharIndex] != ' ')
            {
                _soundCounter = 0;

                _audioSource.pitch = Random.Range(0.9f, 1f);
                _audioSource.PlayOneShot(textSound, 1f);
            }
            else
                _soundCounter++;
            // Increments the character index to get the next character in the string
            currentCharIndex++;

            // When we still have characters in the string left to display
            if (currentCharIndex < stringLength)
            {
                // Prints at normal speed
                yield return new WaitForSeconds(characterPrintInterval);
            }
            else
            {
                break;
            }
        }

        // When the whole string has been displayed...
        while (true)
        {        
            // We go to the next page after a while
            yield return new WaitForSeconds(newPageInterval);

            break;
        }
        // We are currently not revealing text
        _stringIsBeingRevealed = false;
    }
}
