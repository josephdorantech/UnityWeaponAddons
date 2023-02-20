using UnityEngine;

/// <summary>
/// This script adds a sway to the weapon based on mouse movement (aim).
/// It is designed for FPS games.
/// Attach it directly to a GameObject in the hierarchy ABOVE the weapon. 
/// It is recommended to half the values of the sway when aiming down sights.
/// The pivot point for the sway should be where the supporting hand would be:
/// e.g. If the shooter was right handed and carrying an assault rifle, the supporting hand
/// would be holding just in front of the magazine. 
/// This is where the sway pivot should be placed.
/// </summary>


public class WeaponSway : MonoBehaviour
{
    [Header("Sway movement settings")]
    [Tooltip("This is how hard the intial response is to a turn. Turn this up for heavier weapons.")]
    [SerializeField]
    private float initialSwingSpeed = 10;
    public float InitialSwingSpeed
    {
        get { return initialSwingSpeed; }
        set { initialSwingSpeed = value; }
    }


    [Tooltip("This is the up and down tilt amount.")]
    [SerializeField]
    private float linearSwayAmount = 20;
    public float LinearSwayAmount
    {
        get { return linearSwayAmount; }
        set { linearSwayAmount = value; }
    }


    [Tooltip("This is the rotational sway alon the forward axis that matches left and right movement.")]
    [SerializeField]
    private float rotationSwayAmount = 30;
    public float RotationSwayAmount
    {
        get { return rotationSwayAmount; }
        set { rotationSwayAmount = value; }
    }


    [Tooltip("How fast the gun returns to zero point after being swung.")]
    [SerializeField]
    private float returnSwingSpeed = 5f;
    public float ReturnSwingSpeed
    {
        get { return returnSwingSpeed; }
        set { returnSwingSpeed = value; }
    }


    private Quaternion targetRotation; //Frame to frame rotation save


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * LinearSwayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * LinearSwayAmount;
        float rotationMouseX = Input.GetAxis("Mouse X") * RotationSwayAmount;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion rotationZ = Quaternion.AngleAxis(-rotationMouseX, Vector3.forward);

        Quaternion frameTargetRot = rotationX * rotationY * rotationZ;

        targetRotation = Quaternion.Slerp(targetRotation, frameTargetRot, Time.deltaTime * ReturnSwingSpeed);


        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, InitialSwingSpeed * Time.deltaTime);
    }
}
