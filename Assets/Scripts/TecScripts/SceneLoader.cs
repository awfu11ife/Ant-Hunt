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
        _currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
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

        if (SceneManager.sceneCountInBuildSettings > _currentSceneNumber && SceneManager.GetActiveScene().buildIndex != _currentSceneNumber)
            SceneManager.LoadScene(_currentSceneNumber);
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

        if (SceneManager.sceneCountInBuildSettings >= _currentSceneNumber + 1)
            saveSceneIndex = _currentSceneNumber + 1;
        else
            saveSceneIndex = _currentSceneNumber;

        var data = new SaveData.SceneData()
        {
            CurrentScene = saveSceneIndex
        };

        return data;
    }
}
