using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Shockwave : MonoBehaviour
{
    public UnityEvent shockwave;

    void Awake()
    {
        InvokeRepeating("ShockwaveEvent", 3f, 4f);
    }

    private void ShockwaveEvent()
    {
        shockwave.Invoke();
    }
}
