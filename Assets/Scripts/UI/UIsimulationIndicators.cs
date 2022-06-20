using UnityEngine;
using UnityEngine.UI;

public class UIsimulationIndicators : MonoBehaviour
{
    [SerializeField] private Image timeIndicator;
    [SerializeField] private Image playIndicator;
    [SerializeField] private Image pauseIndicator;
    [SerializeField] private Image gravityNormalIndicator;
    [SerializeField] private Image gravityReverseIndicator;

    [SerializeField] Color disableColor;
    [SerializeField] Color enableColor;

    private void Awake()
    {
        timeIndicator.color = disableColor;
        playIndicator.color = enableColor;
        pauseIndicator.color = disableColor;
        gravityNormalIndicator.color = enableColor;
        gravityReverseIndicator.color = disableColor;
    }

    private void OnEnable()
    {
        PhysicsManager.OnGravityChanged += HandleGravityChanged;
        PhysicsManager.OnSimulationToggle += HandleSimulationToggle;
    }

    private void OnDisable()
    {
        PhysicsManager.OnGravityChanged -= HandleGravityChanged;
        PhysicsManager.OnSimulationToggle -= HandleSimulationToggle;
    }

    private void HandleSimulationToggle(bool enabled)
    {
        if (enabled)
        {
            playIndicator.color = enableColor;
            pauseIndicator.color = disableColor;
        } else
        {
            playIndicator.color = disableColor;
            pauseIndicator.color = enableColor;
        }
    }

    private void HandleGravityChanged(PhysicsManager.GravityMode mode)
    {
        if (mode == PhysicsManager.GravityMode.Normal)
        {
            gravityNormalIndicator.color = enableColor;
            gravityReverseIndicator.color = disableColor;
        } else
        {
            gravityNormalIndicator.color = disableColor;
            gravityReverseIndicator.color = enableColor;
        }
    }
}