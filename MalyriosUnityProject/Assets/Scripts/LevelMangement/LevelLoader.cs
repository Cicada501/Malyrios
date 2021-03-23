using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=CE9VOZivb3I&t=184s
public class LevelLoader : MonoBehaviour
{   
    public static string levelToLoad;
    [SerializeField] Animator transition = null;
    float transtionTime = 1f;
    public static bool loadLevel;


    private void Update() {
        
        if(loadLevel){
            LoadNextLevel(levelToLoad);
            loadLevel = false;
        }
    }
    public void LoadNextLevel(string levelName){
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string _levelName){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);
        SceneManager.LoadScene(_levelName);
    }
}
