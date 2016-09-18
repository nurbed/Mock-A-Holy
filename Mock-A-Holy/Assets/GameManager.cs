using UnityEngine;
using System.Collections;

public enum GameState { INTRO, MAIN_MENU, GAME_ADEPTO, GAME_SCIAMANO }

public delegate void OnStateChangeHandler();

public class GameManager : Object
{
    protected GameManager() : base() { }
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState GameState { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Object.DontDestroyOnLoad(GameManager.instance);
                instance = new GameManager();
            }
            return instance;
        }

    }

    public void SetGameState(GameState state)
    {
        this.GameState = state;
        if(OnStateChange != null) OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }
}