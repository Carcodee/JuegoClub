using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    public Animator animator;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera selectCamera;
    public CinemachineVirtualCamera enterCamera;
    public CinemachineVirtualCamera optionsCamera;
    
    
    public ButtonManagerWithIcon startButton;
    public ButtonManagerWithIcon creditsButton;
    public ButtonManagerWithIcon exitButton;
    public ButtonManagerWithIcon options;
    public Image hoverImage;

    public SliderManager brightSlider;
    public SliderManager soundSlider;


    public GameObject book;
    public bool canEnter = false;
    public AsyncOperation loadScene;
    public Animator doorAnimator;
    public Animator canvasBookAnimator;
    
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
        options.gameObject.SetActive(false);
        book.GetComponent<Animator>().Play("OpenBook");
        mainCamera.Priority = 0;
        selectCamera.Priority = 1;
        enterCamera.Priority = 0;
        doorAnimator.Play("OpenDoor");

        StartCoroutine(LoadSceneAsync());
    }
    public void OnCharHover() {
        canvasBookAnimator.Play("OnHoverAnim");
    }
    public void OnCharHoverEnd() {
        canvasBookAnimator.speed=-1;
        canvasBookAnimator.Play("OnHoverAnim");
    }
    public void Options() {
        startButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        mainCamera.Priority = 0;
        selectCamera.Priority = 0;
        enterCamera.Priority = 0;
        optionsCamera.Priority = 1;

    }
    public void ExitOptions() {
        startButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        mainCamera.Priority = 1;
        selectCamera.Priority = 0;
        enterCamera.Priority = 0;
        optionsCamera.Priority = 0;
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
    public void OnSliderSoundChange() {

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
