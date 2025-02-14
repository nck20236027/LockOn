using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraType
{
    NormalCamera,
    BoostCamera,

}

public class CameraController : MonoBehaviour, ICameraContollorable
{

    [SerializeField]
    InputAction action;
    [SerializeField, Header("U“®‚ÌÅ‘å‚Ì‹­‚³")]
    private float shaikhPowor;
    [SerializeField, Header("U“®‚Ì—h‚ê‚ÌŠÔŠu")]
    private float shaikhInterval;

     public CinemachineInputProvider inputProvider;


    [SerializeField] 
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private CinemachineImpulseSource _impulseSource;
    public void CameraSheikh()
    {
        _impulseSource.GenerateImpulse();
    }

    public void CameraChange()
    {
        _virtualCamera.enabled = !_virtualCamera.enabled;
    }

    private void Start()
    {

        for(int i = 0; action.bindings.Count > i; i++)
        {
            Debug.Log(action.bindings[i].processors);
        }
            
        ;
        _impulseSource.m_ImpulseDefinition.m_AmplitudeGain = shaikhPowor;
        _impulseSource.m_ImpulseDefinition.m_FrequencyGain = shaikhInterval;

    }
#if a
    public Transform Target { get => target; set => target = value; }
    private Transform target;
    Vector3 position = Vector3.zero;
    [SerializeField]
    Vector2 cameraRotationPoint = Vector2.zero;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        position = 
    }

    public void CameraChange()
    {
        throw new System.NotImplementedException();
    }

    public void CameraSheikh()
    {
        throw new System.NotImplementedException();
    }
#endif
}
