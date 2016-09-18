using UnityEngine;
using System.Collections;
using FMODUnity;

public class MusicManager_Scene01 : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string MUS_Stage_01_Event;
    FMOD.Studio.EventInstance MUS_Stage_01;
    public string p_AdeptStatus;
    public string p_Thunderstruck;

    // float value;


    // Use this for initialization
    void Start()
    {
        MUS_Stage_01 = FMODUnity.RuntimeManager.CreateInstance(MUS_Stage_01_Event);
        MUS_Stage_01.start();
    }

    // Update is called once per frame
    void Update()
    {

        //    MUS_Stage_01.setParameterValue(p_AdeptStatus, value);
        //    MUS_Stage_01.setParameterValue(p_Thunderstruck, value);
    }
}