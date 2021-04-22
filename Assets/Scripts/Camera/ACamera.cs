using UnityEngine;

public abstract class ACamera : MonoBehaviour
{
    public enum CameraState { Active, Inactive}
    public CameraState cameraState;

    public virtual Vector2 CameraLCorner() { return Vector2.zero; }


    #region PUBLIC METHODS
    public void ActivatedMovement() => cameraState = CameraState.Active;
    public void DeactivatedMovement() => cameraState = CameraState.Inactive;

    #endregion

}
