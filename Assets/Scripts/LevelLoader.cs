using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = 1f;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private static LevelLoader _instance;

    public static LevelLoader Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
