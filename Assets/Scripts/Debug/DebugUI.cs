using UnityEngine;

using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private bool show = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            show = !show;
    }

    void OnGUI()
    {
        if (!show) return;

        GUILayout.BeginArea(new Rect(10, 10, 280, 200), GUI.skin.box);
        GUILayout.Label("<b>Character Controller Debug</b>", new GUIStyle(GUI.skin.label) { richText = true });

        GUILayout.Label($"Position: {controller.transform.position:F2}");
        GUILayout.Label($"Grounded: {controller._isGrounded:F2}");
        GUILayout.Label($"VelX: {controller.GetAnimationVelocityX():F2}");
        GUILayout.Label($"VelY: {controller.GetAnimationVelocityY():F2}");

        GUILayout.EndArea();
    }
}