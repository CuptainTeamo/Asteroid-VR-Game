using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOringin : MonoBehaviour
{
    [SerializeField] private Pose originPose;
    private XRGrabInteractable grabInteractable;
    
    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originPose.position = transform.position;
        originPose.rotation = transform.rotation;
    }

    private void OnEnable() => grabInteractable.selectExited.AddListener(LaserGunReleased);

    private void OnDisable() => grabInteractable.selectExited.RemoveListener(LaserGunReleased);

    private void LaserGunReleased(SelectExitEventArgs arg0)
    {
        transform.position = originPose.position;
        transform.rotation = originPose.rotation;
    }
}
