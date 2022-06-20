using System;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public enum GravityMode
    {
        Normal,
        Reversed,
    }

    public static event Action<GravityMode> OnGravityChanged;
    public static event Action<bool> OnSimulationToggle;

    private GravityMode currentGravityMode = GravityMode.Normal;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.gravity = -Physics.gravity;
            currentGravityMode = currentGravityMode == GravityMode.Normal ? GravityMode.Reversed : GravityMode.Normal;
            OnGravityChanged?.Invoke(currentGravityMode);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Physics.autoSimulation = !Physics.autoSimulation;
            OnSimulationToggle?.Invoke(Physics.autoSimulation);
        }
    }
}