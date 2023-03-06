using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDampener : MonoBehaviour
{
    /// <summary>
    /// Determines the output value of a float when it is fed into a spring type system, given the springs strength, proximity response and the objects velocity.
    /// </summary>
    /// <param name="currentPoint">Current float value</param>
    /// <param name="targetPoint">Target float value</param>
    /// <param name="currentVelocity">Current linear velocity</param>
    /// <param name="newVelocity">New linear velocity</param>
    /// <param name="springStrengthCoefficient">Spring strength coefficient- how springy the spring is.</param>
    /// <param name="overshootCoefficient">How hard the response coefficient changes when travelling AWAY from the target</param>
    /// <param name="springResponse">The response of the spring when the target is in close proximity</param>
    /// <returns>Returns the new position of a float along a line.</returns>
    public static float SpringPositionDeterminer(float currentPoint, float targetPoint, float currentVelocity, out float newVelocity, float springStrengthCoefficient, float overshootCoefficient, AnimationCurve springResponse)
    {
        springStrengthCoefficient = Mathf.Clamp(springStrengthCoefficient, 0, 1);
        overshootCoefficient = Mathf.Clamp(overshootCoefficient, 1, 20);

        float targetDistance = targetPoint - currentPoint;

        if (currentPoint > targetPoint)
        {
            if (currentVelocity > 0)
            {
                newVelocity = currentVelocity - CoefficientResponse(springStrengthCoefficient, overshootCoefficient, targetDistance, springResponse) * Time.deltaTime;
                return currentPoint + newVelocity;
            }
            else
            {
                newVelocity = currentVelocity - CoefficientResponse(springStrengthCoefficient, 1, targetDistance, springResponse) * Time.deltaTime;
                return currentPoint + newVelocity;
            }
        }
        else
        {
            if (currentVelocity < 0)
            {
                newVelocity = currentVelocity + CoefficientResponse(springStrengthCoefficient, overshootCoefficient, targetDistance, springResponse) * Time.deltaTime;
                return currentPoint + newVelocity;
            }
            else
            {
                newVelocity = currentVelocity + CoefficientResponse(springStrengthCoefficient, 1, targetDistance, springResponse) * Time.deltaTime;
                return currentPoint + newVelocity;
            }
        }
    }

    /// <summary>
    /// Works out the coefficient response of the spring, in relation to proximity to the target point.
    /// </summary>
    /// <param name="springStrengthCoefficient">The spring strength coefficient value</param>
    /// <param name="overshootCoefficient">The overshoot coefficient value</param>
    /// <param name="currentDistance">The current distance to the target</param>
    /// <param name="response">The spring response curve</param>
    /// <returns>Returns the total value of the spring response based on coefficients and proximity to target</returns>
    private static float CoefficientResponse(float springStrengthCoefficient, float overshootCoefficient, float currentDistance, AnimationCurve response)
    {
        return springStrengthCoefficient * overshootCoefficient * EvaluateResponse(currentDistance, response);
    }

    /// <summary>
    /// Returns response of the spring when there is close proximity to the target point.
    /// </summary>
    /// <param name="value">Distance to the target point</param>
    /// <param name="response">Spring response curve</param>
    /// <returns>Returns the value the coefficient should be reduced by as a value between 0 and 1</returns>
    private static float EvaluateResponse(float value, AnimationCurve response)
    {
        value = Mathf.Abs(value);

        if (value > 1)
        {
            return 1;
        }
        else
        {
            return response.Evaluate(value);
        }
    }
}
