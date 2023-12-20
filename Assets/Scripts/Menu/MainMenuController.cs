using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator animator;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera selectCamera;
    public CinemachineVirtualCamera enterCamera;
    
    
    public ButtonManagerWithIcon startButton;
    public ButtonManagerWithIcon creditsButton;
    public ButtonManagerWithIcon exitButton;
    
    
    public GameObject book;
    void Start()
    {
        
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
    }

    public void EnterGame()
    {
        mainCamera.Priority = 0;
        selectCamera.Priority = 0;
        enterCamera.Priority = 1;
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
}
