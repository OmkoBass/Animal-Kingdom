using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Maybe i shouldn't have done all of this in one prefab
public class GenerateAnimalName : MonoBehaviour
{
    VisualElement Root;
    VisualElement LetterContainer;
    Label LabelScore;
    Label LabelTimer;

    VisualElement ElementGameOver;
    Label LabelGameOverScore;
    Button ButtonMainMenu;
    Button ButtonRestart;

    [SerializeField]
    SpriteRenderer SpriteRenderer;

    [SerializeField]
    Sprite[] animals = new Sprite[28];

    [SerializeField]
    float TypeTimerInitial = 10;

    [SerializeField]
    float TypeTimerDecrement = 0.1f;

    [SerializeField]
    float MinimumTimerTreshhold = 5f;
    int lastAnimalIndex = 0;
    string animalName = null;
    float TypeTimer = 0;
    List<Button> animalLetterButtons = new List<Button>();

    Slider Slider;
    void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        LetterContainer = Root.Q<VisualElement>("LetterContainer");
        LabelTimer = Root.Q<Label>("LabelTimer");
        LabelScore = Root.Q<Label>("LabelScore");

        ElementGameOver = Root.Q<VisualElement>("ElementGameOver");
        LabelGameOverScore = Root.Q<Label>("LabelGameOverScore");
        Button ButtonMainMenu = Root.Q<Button>("ButtonMainMenu");
        Button ButtonRestart = Root.Q<Button>("ButtonRestart");

        ButtonRestart.clicked += RestartGame;
        ButtonMainMenu.clicked += MainMenu;

        TypeTimer = TypeTimerInitial;

        Clear();
        GenerateAnimal();
        GameManager.Score = 0;
        GameManager.GameOver = false;
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    private void RestartGame()
    {
        Clear();
        GenerateAnimal();
        GameManager.Score = 0;
        TypeTimer = TypeTimerInitial;
        ElementGameOver.style.display = DisplayStyle.None;

        GameManager.GameOver = false;
    }

    private void Update()
    {
        if (GameManager.GameOver)
        {
            return;
        }

        if (TypeTimer <= 0)
        {
            GameManager.GameOver = true;

            ElementGameOver.style.display = DisplayStyle.Flex;
            LabelGameOverScore.text = LabelScore.text;
        }

        // If it's the last letter we typed
        // Then we reset the variables
        // Generate new animal
        // Increment new score
        // And decrease the time allowed to type again
        if (lastAnimalIndex == animalName.Length)
        {
            Clear();
            GenerateAnimal();
            GameManager.Score++;

            float typeTimerDecrementer = TypeTimerDecrement * GameManager.Score;

            if (typeTimerDecrementer <= 5)
            {
                TypeTimer = TypeTimerInitial - typeTimerDecrementer;
            }
            else
            {
                TypeTimer = MinimumTimerTreshhold;
            }

            return;
        }

        // T I G E R <- Press T then I then G then E then R
        // Checks the first letter that needs to be typed
        // When it's typed we move to the next letter
        // And blur out the typed one
        string inputString = Input.inputString;
        if (inputString == animalName[lastAnimalIndex].ToString())
        {
            // Check if the button is read from a mistake, if it is remove that class
            if (animalLetterButtons[lastAnimalIndex].ClassListContains("typed-error"))
            {
                animalLetterButtons[lastAnimalIndex].RemoveFromClassList("typed-error");
                animalLetterButtons[lastAnimalIndex].AddToClassList("to-be-typed");
            }

            animalLetterButtons[lastAnimalIndex].AddToClassList("typed");
            lastAnimalIndex++;
        }
        else if (inputString != "")
        {
            animalLetterButtons[lastAnimalIndex].RemoveFromClassList("to-be-typed");
            animalLetterButtons[lastAnimalIndex].AddToClassList("typed-error");
        }

        // Timer goes down
        // Update the score
        // Update the timer
        TypeTimer -= Time.deltaTime;
        LabelScore.text = $"Score: {GameManager.Score}";
        LabelTimer.text = TypeTimer.ToString("#.##");
    }

    // I should rename this to ClearState
    private void Clear()
    {
        lastAnimalIndex = 0;
        LetterContainer.Clear();
        animalLetterButtons.Clear();
    }

    void GenerateAnimal()
    {
        // Generates a random animal from the animal sprites
        int randomAnimal = Random.Range(0, 28);

        // Replace the middle sprite with the chosen one
        SpriteRenderer.sprite = animals[randomAnimal];

        // Gets the selected animal name
        string animalName = animals[randomAnimal].name;

        // And changes the global var animalName
        this.animalName = animalName;

        // Foreach letter of the animal add a button
        foreach (char animalLetter in animalName)
        {
            Button buttonAnimalLetter = new Button();
            buttonAnimalLetter.text = animalLetter.ToString();
            buttonAnimalLetter.AddToClassList("button");
            buttonAnimalLetter.AddToClassList("to-be-typed");

            animalLetterButtons.Add(buttonAnimalLetter);
            LetterContainer.Add(buttonAnimalLetter);
        }
    }
}
