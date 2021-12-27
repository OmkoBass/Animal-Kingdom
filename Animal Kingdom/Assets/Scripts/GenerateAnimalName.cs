using UnityEngine;

public class GenerateAnimalName : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SpriteRenderer;

    [SerializeField]
    SpriteRenderer AnimalLetter;

    [SerializeField]
    Sprite[] letters = new Sprite[0];

    [SerializeField]
    Sprite[] animals = new Sprite[28];

    string animalName = "";
    void Start()
    {
        GameManager.Score = 0;
        GameManager.Timer = 10f;
        GameManager.LastAnimalIndex = 0;
        GenerateAnimal();
        GameManager.GameOver = false;
    }

    private void Update()
    {
        if (GameManager.GameOver)
        {
            SpriteRenderer.gameObject.SetActive(false);
            AnimalLetter.gameObject.SetActive(false);
            return;
        }
        else
        {
            SpriteRenderer.gameObject.SetActive(true);
            AnimalLetter.gameObject.SetActive(true);
        }

        if (GameManager.LastAnimalIndex == animalName.Length || GameManager.LastAnimalIndex == -1)
        {
            GameManager.Score++;
            if (GameManager.Score * 0.1 < 5)
            {
                GameManager.Timer = 10f - (GameManager.Score * 0.1f);
            }
            else
            {
                GameManager.Timer = 5f;
            }

            GameManager.LastAnimalIndex = 0;
            Clear();
            GenerateAnimal();
            return;
        }

        string inputString = Input.inputString;
        if (inputString == animalName[GameManager.LastAnimalIndex].ToString())
        {
            Color currentLetterColor = GameManager.AddedLetters[GameManager.LastAnimalIndex].color;
            currentLetterColor.a = 0.3f;
            GameManager.AddedLetters[GameManager.LastAnimalIndex].color = currentLetterColor;

            GameManager.LastAnimalIndex++;
        }
    }

    void GenerateAnimal()
    {
        // Generates a random animal from the animal sprites
        int randomAnimal = Random.Range(0, 28);

        // Replace the middle sprite with the chosen one
        SpriteRenderer.sprite = animals[randomAnimal];

        // Gets the selected animal name
        animalName = animals[randomAnimal].name;

        // Foreach letter of the animal add a button
        for (int i = 0; i < animalName.Length; i++)
        {
            for (int j = 0; j < letters.Length; j++)
            {
                if (letters[j].name == $"letter_{animalName[i].ToString().ToUpper()}")
                {
                    var spriteAnimalLetter = Instantiate(AnimalLetter);

                    if (animalName.Length % 2 == 0)
                    {
                        spriteAnimalLetter.transform.position = new Vector2((i - animalName.Length / 2.5f) * 1.4f, -2);
                    }
                    else
                    {
                        spriteAnimalLetter.transform.position = new Vector2((i - animalName.Length / 2) * 1.4f, -2);
                    }
                    spriteAnimalLetter.sprite = letters[j];

                    GameManager.AddedLetters.Add(spriteAnimalLetter);
                    break;
                }
            }
        }
    }

    public static void Clear()
    {
        foreach (var item in GameManager.AddedLetters)
        {
            Destroy(item.gameObject);
        }
        GameManager.AddedLetters.Clear();
    }
}
