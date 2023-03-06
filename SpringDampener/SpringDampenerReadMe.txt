/////////////////////////////////////////////////////////
//      SPRING DAMPENER READ ME
/////////////////////////////////////////////////////////

This collection of scripts works to create a spring-like effect using floats. 
This can be expanded to work with positions and Quaternions (see my Hand Sway script for example usage).



/////////////////////////////////////////////////////////
///     SpringDampener.CS       ///
/////////////////////////////////////////////////////////

This script is completely static- in other words, just have it inside your Unity project.
This is the main maths and conditionals that run the spring system.
You can engage with it directly, but for easier access, I recommend using:



/////////////////////////////////////////////////////////
///     SpringBehaviour.CS      ///
/////////////////////////////////////////////////////////

This script is a class that you can add to your scripts to make creating spring effects easier.
For example:

    [SerializeField]
    private SpringBehaviour springBehaviour;

This will allow you to edit the settings of the script:

    ResponseCurve: This is how the spring acts when it's oscillations get closer to the target point.
                    If you give it a linear value (0,0 to 1,1), it will produce a softening effect.
                    Curving the response curve can produce softer responses or more extreme responses.
                    A flat line at 1 across to 1 produces a hard spring.

    SpringStrengthCoefficient:  This value is equivalent to the springyness of the spring. Lower values produce
                                a softer spring, higher values produce a harder spring with quicker oscillations.

    OvershootCoefficient: This can be thought of like the drag experienced by the spring; higher values will slow
                            the spring down faster. At the bottom of the range (1f), there will be no drag and the
                            spring will oscillate for wayyyyy too long to calculate.


/////////////////////////////////////////////////////////                  
///     Creating a Runtime SpringBehaviour ///
/////////////////////////////////////////////////////////

When initiating a SpringBehaviour at runtime, you will need to use the constructors:

    SpringBehaviour behaviour = new SpringBehaviour(behaviour details here);

Make sure to use a transform.localPosition if you are trying to add this to a child of a gameobject.


/////////////////////////////////////////////////////////
///     Quick Start     ///
/////////////////////////////////////////////////////////

There is a quick use function to set and get an answer from SpringBehaviour:

float floatToChange = SpringBehaviour.GetNewFloatPosition(SpringBehaviour.TargetPoint);

This function updates everything within the class before returning the value so you only have to worry
about applying the value returned to the value you want to change.

If you want to make the oscillations bigger but have the spring working ideally, do the following:

float floatToChange = SpringBehaviour.GetNewFloatPosition(SpringBehaviour.TargetPoint) * amountToScaleBy;

This will increase the oscillation scale, without affecting the spring settings.