using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class ShakeMachine : MonoBehaviour
{
    public UnityEvent shake;
    public CinemachineImpulseSource source;

    public void Shake()
    {
        source.GenerateImpulse();
    }
}
