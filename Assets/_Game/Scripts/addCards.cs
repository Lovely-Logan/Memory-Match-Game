using UnityEngine;

public class addCards : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] Transform puzzleField;
    [SerializeField] GameObject cardbtn;

    private void Awake()
    {
        for (int i = 0; i < 16; i++) // 16 cards in the game
        {
            GameObject cards = Instantiate(cardbtn);  // Spawn cards 
            cards.name = "" + i; // Name the cards
            cards.transform.SetParent(puzzleField, false); // Parent the cards to the puzzle field
        }
    }
}
