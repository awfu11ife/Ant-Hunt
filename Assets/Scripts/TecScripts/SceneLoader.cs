using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Finish _finish;

    private int _currentSceneNumber;

    private void Awake()
    {
        LoadScene();
    }

    private void OnEnable()
    {
        _finish.OnReached += Save;
        _finish.OnReached += LoadScene;
    }

    private void OnDisable()
    {
        _finish.OnReached -= Save;
        _finish.OnReached -= LoadScene;
    }

    private void LoadScene()
    {
        Load();

        if (SceneManager.GetActiveScene().buildIndex != CalculateSceneToLoad())
            SceneManager.LoadScene(CalculateSceneToLoad());

    }

    private int CalculateSceneToLoad()
    {
        int currentLevel = _currentSceneNumber;

        if (SceneManager.sceneCountInBuildSettings <= _currentSceneNumber)
        {
            int exceedingFactor = _currentSceneNumber / SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < exceedingFactor; i++)
            {
                currentLevel -= SceneManager.sceneCountInBuildSettings;
            }

        }

        return currentLevel;
    }

    private void Save()
    {
        SaveLoadData.Save(gameObject.name.ToString(), GetSaveSnapshot());
    }

    private void Load()
    {
        var data = SaveLoadData.Load<SaveData.SceneData>(gameObject.name.ToString());

        if (_currentSceneNumber != data.CurrentScene)
            _currentSceneNumber = data.CurrentScene;
    }

    private SaveData.SceneData GetSaveSnapshot()
    {
        int saveSceneIndex;

        saveSceneIndex = _currentSceneNumber + 1;

        var data = new SaveData.SceneData()
        {
            CurrentScene = saveSceneIndex
        };

        return data;
    }
}
