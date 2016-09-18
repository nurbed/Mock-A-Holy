using UnityEngine;
using System.Collections;
using FMODUnity;

public class MusicManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string MUS_MainMenuEvent;
    FMOD.Studio.EventInstance MUS_MainMenu;
    public string p_MenuState;

    // Use this for initialization
    void Start()
    {
        MUS_MainMenu = FMODUnity.RuntimeManager.CreateInstance(MUS_MainMenuEvent);
        MUS_MainMenu.start();
    }

    // Update is called once per frame
    void Update()
    {

    //    MUS_MainMenu.setParameterValue(p_MenuState, value);
    }
}