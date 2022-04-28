using UnityEngine;
using UnityEngine.UI;

public class GameInterfacePauseOptions : MonoBehaviour
{
    [HideInInspector] public int volume;

    [SerializeField] private Button backButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();
        }
    }

    public void ChangeName()
    {
        Debug.Log("Change Name");
    }

    public void ChangeSkin()
    {
        Debug.Log("Change Skin");
    }

    public void WindowMode()
    {
        Debug.Log("Window Mode");
    }

    public void AudioMode()
    {
        Debug.Log("Audio Mode");
    }

    public void Volume()
    {
        Debug.Log("Volume");
    }

    public void Controls()
    {
        Debug.Log("Controls");
    }
}
