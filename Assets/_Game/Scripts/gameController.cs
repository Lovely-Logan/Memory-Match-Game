using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameController : MonoBehaviour
{
    [SerializeField] private Sprite cardBack;

    public Sprite[] cardImages;
    public List<Sprite> cardList = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    public Text correctCountText;
    public string levelName;

    private bool firstTry, secondTry;
    private int tryCount;
    private int correctCount;
    private int gameTry;
    private int firstTryIndex, secondTryIndex;
    private string firstguessPuzzle, sceondguessPuzzle;

    void Awake()
    {
        cardImages = Resources.LoadAll<Sprite>("Sprites/Card Symbol"); // Load the card images using the Resources folder
    }

    private void Start()
    {
        GetButtons(); // Get the buttons
        AddListeners(); // Add the listeners
        AddcardList(); // Add the card list
        Shuffle(cardList); // Shuffle the card list
        gameTry = cardList.Count / 2; // Get the game try
    }

    void GetButtons() // Get the buttons
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card Button"); // Get the buttons
        for (int i = 0; i < objects.Length; i++) // Loop through the buttons
        {
            btns.Add(objects[i].GetComponent<Button>()); // Add the button
            btns[i].image.sprite = cardBack; // Set the button image
        }
    }

    void AddcardList() // Add the card list
    {
        int looping = btns.Count; // Get the number of buttons
        int index = 0; // Get the index

        for (int i = 0; i < looping; i++) // Loop through the buttons
        {
            if (index == looping / 2) // If the index is equal to the number of buttons
            {
                index = 0; // Set the index to 0
            }
            cardList.Add(cardImages[index]); // Add the card image
            index++; // Increase the index
        }
    }

    void AddListeners() // Add the listeners
    {
        foreach (Button btn in btns) // Loop through the buttons
        {
            btn.onClick.AddListener(PickAPuzzle); // Add the listener
        }
    }

    public void PickAPuzzle() // Pick a puzzle
    {
        string name = EventSystem.current.currentSelectedGameObject.name; // Get the name of the button
        if (!firstTry) // If the first try is false
        {
            firstTry = true; // Set the first try to true
            firstTryIndex = int.Parse(name); // Get the index
            firstguessPuzzle = cardList[firstTryIndex].name; // Get the puzzle
            btns[firstTryIndex].image.sprite = cardList[firstTryIndex]; // Set the button image

        }
        else if (!secondTry) // If the second try is false
        {
            secondTry = true; // Set the second try to true
            secondTryIndex = int.Parse(name); // Get the index
            sceondguessPuzzle = cardList[secondTryIndex].name; // Get the puzzle
            btns[secondTryIndex].image.sprite = cardList[secondTryIndex]; // Set the button image

            StartCoroutine(CheckMatch()); // Start the coroutine
        }

    }

    IEnumerator CheckMatch() // Check the match
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        if (firstguessPuzzle == sceondguessPuzzle) // If the puzzles match
        {
            yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
            btns[firstTryIndex].interactable = false; // Set the button interactable to false
            btns[secondTryIndex].interactable = false; // Set the button interactable to false
            btns[firstTryIndex].image.color = new Color(0f, 0f, 0f, 0f); // Set the button image color
            btns[secondTryIndex].image.color = new Color(0f, 0f, 0f, 0f); // Set the button image color
            tryCount++; // Increase the try count

            matching(); // Call the matching function
        }
        else
        {
            yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
            btns[firstTryIndex].image.sprite = cardBack; // Set the button image
            btns[secondTryIndex].image.sprite = cardBack; // Set the button image
        }
        yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
        firstTry = secondTry = false; // Set the first try and second try to false

    }

    void matching() //  Matching
    {
        correctCount++; // Increase the correct count
        if (correctCount == gameTry) // If the correct count is equal to the game try
        {
            Debug.Log("You Win"); // Print the win message
            Debug.Log("You took " + tryCount + " tries"); // Print the try count
            SceneManager.LoadScene(levelName); // Load the level
        }
    }

    void Shuffle(List<Sprite> list) // Shuffle
    {
        for (int i = 0; i < list.Count; i++) // Loop through the list
        {
            Sprite temp = list[i]; // Get the sprite
            int randomIndex = Random.Range(i, list.Count); // Get the random index
            list[i] = list[randomIndex]; // Set the sprite
            list[randomIndex] = temp; // Set the sprite
        }
    }
    void Update()
    {
        correctCountText.text = "Score: " + correctCount.ToString(); // Update the score
    }

}

