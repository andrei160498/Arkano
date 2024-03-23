using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string firstLevelName = "Level 1"; // Название сцены первого уровня

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelName); // Загружаем сцену первого уровня
    }
}
