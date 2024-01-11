using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    public Animator animator;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera selectCamera;
    public CinemachineVirtualCamera enterCamera;
    
    
    public ButtonManagerWithIcon startButton;
    public ButtonManagerWithIcon creditsButton;
    public ButtonManagerWithIcon exitButton;
    
    
    public GameObject book;
    public bool canEnter = false;
    public AsyncOperation loadScene;
    
    SceneCameras currentSceneCameras;
    
    void Start()
    {
        instance = this;
        MainMenuController.instance.currentSceneCameras = currentSceneCameras;
    }

    void Update()
    {
        
    }
    
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        book.GetComponent<Animator>().Play("OpenBook");
        mainCamera.Priority = 0;
        selectCamera.Priority = 1;
        enterCamera.Priority = 0;
        StartCoroutine(LoadSceneAsync());
    }

    public void EnterGame()
    {

        if (loadScene.progress >= 0.9f)
        {
            loadScene.allowSceneActivation = true;
            mainCamera.Priority = 0;
            selectCamera.Priority = 10;
        }


    }

    public void OnCameraEntered()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("OutdoorsScene", LoadSceneMode.Additive);
    }
    IEnumerator LoadSceneAsync()
    {
        loadScene = SceneManager.LoadSceneAsync("OutdoorsScene", LoadSceneMode.Additive);
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            Debug.Log(loadScene.progress);
            yield return null;

        }
        Debug.Log("Scene Loaded");
        canEnter = true;




    }
}
