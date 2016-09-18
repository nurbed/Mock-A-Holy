using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void StartGameAsAdepto()
    {
        GameManager.Instance.SetGameState(GameState.GAME_ADEPTO);
        SceneManager.LoadScene("MainScene");
    }

    public void StartGameAsSciamano()
    {
        GameManager.Instance.SetGameState(GameState.GAME_SCIAMANO);
        SceneManager.LoadScene("MainScene");
    }

}
