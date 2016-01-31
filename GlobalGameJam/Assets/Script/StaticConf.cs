using UnityEngine;
using System.Collections;

public static class StaticConf 
{
	public const int STREAM_BYTE = 4;

    public static float DELTA_TIME = 0.8f;//0.6558f;
    public static float RANGE_TIME = 0.2f;

    public static int SANT_OK = 10;
    public static int SANT_KO = -10;

    public static int PLAY_OK = 0;
    public static int PLAY_KO = 0;

    public static int SCORE = 50;

    public static float ANALOGIC_TRIGGER = 0.6f;

    public static int RIGHT_MOVE = -1;

    public const string IdleState = "Idle";

    public const string AdeptiTag = "Adepti";
}
