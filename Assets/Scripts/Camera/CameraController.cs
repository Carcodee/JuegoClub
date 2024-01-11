using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public SceneCameras mainMenuCams;
    public SceneCameras inGameCams;
    
    public SceneCameras currentSceneCameras;
    
    public static CameraController instance;
    void Start()
    {

        
    }

    public void SetMainMenuCam()
    {
        mainMenuCams.SetPriority(10,CamType.MainMenu);
    }
    void Update()
    {


        if ((Input.GetKeyDown(KeyCode.Tab)))
        {
            LookStats();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LookDroppeableZone();
        }
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.D))
        {
            LookPacient();
        }

    }

    public void OnSceneCameraChange(SceneCameras newSceneCameras)
    {
        currentSceneCameras = newSceneCameras;
    }
    
    public void EnterGame()
    {

    }
    
    public void LookPacient()
    {
        inGameCams.SetPriority(10,CamType.LookPacient);
    }
    public void LookDroppeableZone()
    {
        inGameCams.SetPriority(10,CamType.LookDroppeableZone);
    }
    public void LookStats()
    {
        inGameCams.SetPriority(10,CamType.LookStats);
    }



}


    
[System.Serializable]
public class SceneCameras
{
    public Camera myCam;
    public CinemachineBrain cinemachineBrain;
    public CameraConfig[] vcams;
        
    public void SetPriority(int priority, CamType index)
    {
        for (int i = 0; i < vcams.Length; i++)
        {
            if (vcams[i].camType==index)
            {
                vcams[i].vcam.Priority = priority;
            }
            else
            {
                vcams[i].vcam.Priority = 0;
            }
        }
    }
}


        
[System.Serializable]
public struct CameraConfig
{
    public CinemachineVirtualCamera vcam;
    public CamType camType;
}

public enum CamType
{
    MainMenu,
    PlayerSection,
    LookPacient,
    LookDroppeableZone, 
    LookStats,
}