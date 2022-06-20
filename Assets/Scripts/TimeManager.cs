using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField] private float slowTimeScale = 0.1f;

    private float defaultFixedTimeStep;

    private void Start() {
        defaultFixedTimeStep = Time.fixedDeltaTime;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            
            if (Mathf.Approximately(Time.timeScale, 1.0f)) {
                Time.timeScale = slowTimeScale;
                Time.fixedDeltaTime = defaultFixedTimeStep * slowTimeScale;
            } else {
                Time.timeScale = 1;
                Time.fixedDeltaTime = defaultFixedTimeStep;
            }
            
        }
    }
}