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
    private string[] _textBoxStrings;

    // How much time between the print of each character of text
    public float characterPrintInterval = 0.01f;
    // How much time to take before closing/going to the next page
    public float newPageInterval = 0.75f;

    // If we are currently revealing a string
    private bool _stringIsBeingRevealed = false;
    // If the textbox is playing
    private bool _textIsPlaying = false;
    // Checks if we're playing the last page of text
    private bool _isEndOfText = false;


    private AudioSource _audioSource;
    // The sound when playing text
    public AudioClip textSound;

    bool playSound = true;

    bool finishText = false;

    void Start()
    {
        // Gets our textcomponent and clears it from all text
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";

        _audioSource = GetComponent<AudioSource>();
    }

    // Call on this function to print a message of your choice
    public void PrintMessage(/*string[] textPages*/)
    {
        string[] textPages = new string[] {"You attacked the enemy and it took 73 damage! \n<<b>(89 % accuracy!)</b>>", "The enemy was also burned by your attack!" };
        _textBoxStrings = textPages;

        // Makes the textbox visible
        transform.parent.gameObject.GetComponent<Image>().enabled = true;

        // Checks if we're not already playing text
        if (!_textIsPlaying)
        {
            _textIsPlaying = true;
            
            // Starts a textbox
            StartCoroutine(StartTextBox());
        }
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
                break;
            }

            yield return 0;
        }

        // Makes the textbox invisible
        _textComponent.text = "";
        transform.parent.gameObject.GetComponent<Image>().enabled = false;

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
            if (playSound)
            {
                playSound = false;

                _audioSource.pitch = Random.Range(0.9f, 1f);
                _audioSource.PlayOneShot(textSound, 1f);
            }
            else
                playSound = true;
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
