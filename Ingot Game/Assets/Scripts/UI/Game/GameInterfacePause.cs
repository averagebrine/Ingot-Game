using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInterfacePause : MonoBehaviour
{
    [SerializeField] private Pause parent;
    [SerializeField] private Button resumeButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resumeButton.onClick.Invoke();
        }
    }

    public void Exit()
    {
        FindObjectOfType<GameManager>().SaveGame();
        SceneManager.LoadScene("Home");
    }
}
