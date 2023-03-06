using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpringBehaviour
{
    #region Constructors

    public SpringBehaviour(float currentPoint, float targetPoint, float springStrengthCoefficient, float overshootCoefficient)
    {
        this.currentPoint = currentPoint;
        this.targetPoint = targetPoint;
        currentVelocity = 0;
        this.springStrengthCoefficient = springStrengthCoefficient;
        this.overshootCoefficient = overshootCoefficient;
    }

    public SpringBehaviour(float currentPoint, float targetPoint, float springStrengthCoefficient, float overshootCoefficient, AnimationCurve responseCurve)
    {
        this.currentPoint = currentPoint;
        this.targetPoint = targetPoint;
        currentVelocity = 0;
        this.springStrengthCoefficient = springStrengthCoefficient;
        this.overshootCoefficient = overshootCoefficient;
        this.responseCurve = responseCurve;
    }

    #endregion

    [SerializeField]
    private AnimationCurve responseCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    /// <summary>
    /// Sets the curve blend. With a value of 1,1 on 2 keyframes it will produce a sharp spring effect. With a default value of 0,0 and 1,1, it will produce a soft spring.
    /// </summary>
    public AnimationCurve ResponseCurve
    {
        get { return responseCurve; }
        set { responseCurve = value; }
    }

    private float currentPoint;
    /// <summary>
    /// The center point of the object to spring. Needs to be initialised with transform.localPosition variables!
    /// </summary>
    public float CurrentPoint
    {
        get { return currentPoint; }
        set { currentPoint = value; }
    }

    private float targetPoint;
    /// <summary>
    /// The target point of the object. It will move toward and oscillate around this point. Remember to use transform.localPosition variables!
    /// </summary>
    public float TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value; }
    }

    private float currentVelocity;
    /// <summary>
    /// The current "velocity" of the object. This will need to be updated every frame. The SpringPositionDeterminer function Outs a new velocity, so make sure to cache it!
    /// </summary>
    public float CurrentVelocity
    {
        get { return currentVelocity; }
        set { currentVelocity = value; }
    }

    [SerializeField]
    [Range(0.1f, 1f)]
    private float springStrengthCoefficient = 0.12f;
    /// <summary>
    /// A value between 0 and 1. Zero produces no spring, and 1 is the loosest spring possible. 
    /// </summary>
    public float SpringStrengthCoefficient
    {
        get { return springStrengthCoefficient; }
        set { springStrengthCoefficient = Mathf.Clamp(value, 0, 1); }
    }

    [SerializeField]
    [Range(1, 20)]
    private float overshootCoefficient = 2.14f;
    /// <summary>
    /// A value between 1 and 20. Higher numbers produce more rigid results, e.g. 1.1 is like a loose spring, and 20 is like a tough car suspension.
    /// This value increases the Spring Strength Coefficient when the object passes the target point and starts going in the wrong direction.
    /// It will revert back to 1 (no effect) when it starts to return back towards to the target point.
    /// A value of 1 will have no effect on the overshoot.
    /// </summary>
    public float OvershootCoefficient
    {
        get { return overshootCoefficient; }
        set { overshootCoefficient = Mathf.Clamp(value, 1, 20); }
    }

    /// <summary>
    /// Uses this objects values to return a new position, when given a target.
    /// Simply apply the output to your use case. 
    /// This sets the new target position on the local object, so you don't have to worry about updating anything.
    /// </summary>
    /// <returns>Returns the new position value</returns>
    public float GetNewFloatPosition(float target)
    {
        float newVelocity = 0;
        this.TargetPoint = target;

        float newPosition = SpringDampener.SpringPositionDeterminer(
            this.CurrentPoint,
            this.TargetPoint,
            this.currentVelocity,
            out newVelocity,
            this.springStrengthCoefficient,
            this.overshootCoefficient,
            this.responseCurve
            );

        this.currentVelocity = newVelocity;
        this.CurrentPoint = newPosition;

        return newPosition;
    }
    
}
