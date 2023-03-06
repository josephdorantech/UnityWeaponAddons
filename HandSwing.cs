using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwing : MonoBehaviour
{
    #region Sway input modifiers

    [Tooltip("This is the up and down tilt amount.")]
    [SerializeField]
    private float xRotationalSwayAmount = 5f;
    public float XRotationSwayAmount
    {
        get { return xRotationalSwayAmount; }
        set { xRotationalSwayAmount = value; }
    }

    [Tooltip("This is the left and right tilt amount.")]
    [SerializeField]
    private float yRotationalSwayAmount = 30f;
    public float YRotationSwayAmount
    {
        get { return yRotationalSwayAmount; }
        set { yRotationalSwayAmount = value; }
    }


    [Tooltip("This is the rotational sway along the forward axis that matches left and right movement.")]
    [SerializeField]
    private float forwardRotationSwayAmount = 20f;
    public float ForwardRotationSwayAmount
    {
        get { return forwardRotationSwayAmount; }
        set { forwardRotationSwayAmount = value; }
    }

    [Tooltip("This is the up and down position movement amount.")]
    [SerializeField]
    private float verticalPositionSwayAmount = 0.1f;
    public float VerticalPositionSwayAmount
    {
        get { return verticalPositionSwayAmount; }
        set { verticalPositionSwayAmount = value; }
    }

    [Tooltip("This is the left and right position movement amount.")]
    [SerializeField]
    private float horizontalPositionSwayAmount = 0.1f;
    public float HorizontalPositionSwayAmount
    {
        get { return horizontalPositionSwayAmount; }
        set { horizontalPositionSwayAmount = value; }
    }

    #endregion

    public ThreeDimensionalSway rotationalSway;

    public ThreeDimensionalSway positionalSway;

    #region Private variables

    private float mouseX;
    private float mouseY;

    #endregion

    private void Update()
    {
        GetMouseInput();
        ApplyRotationalSway();
        ApplyPositionalSway();
    }

    private void ApplyRotationalSway()
    {
        positionalSway.xAxis.TargetPoint = -mouseY;
        positionalSway.yAxis.TargetPoint = mouseX;
        positionalSway.zAxis.TargetPoint = -mouseX;

        Quaternion rotationX = Quaternion.AngleAxis(rotationalSway.xAxis.GetNewFloatPosition(positionalSway.xAxis.TargetPoint) * XRotationSwayAmount, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(rotationalSway.yAxis.GetNewFloatPosition(positionalSway.yAxis.TargetPoint) * YRotationSwayAmount, Vector3.up);
        Quaternion rotationZ = Quaternion.AngleAxis(rotationalSway.zAxis.GetNewFloatPosition(positionalSway.zAxis.TargetPoint) * ForwardRotationSwayAmount, Vector3.forward);

        Quaternion frameTargetRot = rotationX * rotationY * rotationZ;

        transform.localRotation = frameTargetRot;
    }

    private void ApplyPositionalSway()
    {
        positionalSway.xAxis.TargetPoint = mouseX;
        positionalSway.yAxis.TargetPoint = mouseY;

        float newHPos = positionalSway.xAxis.GetNewFloatPosition(positionalSway.xAxis.TargetPoint) * HorizontalPositionSwayAmount;
        float newVPos = positionalSway.yAxis.GetNewFloatPosition(positionalSway.yAxis.TargetPoint) * VerticalPositionSwayAmount;

        transform.localPosition = new Vector3(
            newHPos,
            newVPos,
            0
            );
    }

    private void GetMouseInput()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    [System.Serializable]
    public class ThreeDimensionalSway
    {
        public SpringBehaviour xAxis;
        public SpringBehaviour yAxis;
        public SpringBehaviour zAxis;
    }
}
