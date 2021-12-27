using UnityEngine;
using System.Collections.Generic;

public class GameManager
{
    public static int Score = 0;
    public static bool GameOver = false;
    public static float TimerInitial = 10f;
    public static float Timer = 10f;
    public static int LastAnimalIndex = 0;
    public static List<SpriteRenderer> AddedLetters = new List<SpriteRenderer>();
}
