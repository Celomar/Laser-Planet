using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cristal))]
public class CrystalEditor : Editor
{
    private Cristal cristal = null;
    private bool right = false;
    private bool up = false;

    void OnEnable()
    {
        cristal = target as Cristal;
        Vector2 reflectionDirections = cristal.reflectionDirections;
        right = reflectionDirections.x > 0.0f;
        up = reflectionDirections.y > 0.0f;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        bool rightOn = EditorGUILayout.Toggle("Looking right", right);
        bool upOn = EditorGUILayout.Toggle("Looking Up", up);

        if(right != rightOn || up != upOn)
        {
            right = rightOn;
            up = upOn;

            cristal.reflectionDirections = new Vector2(
                rightOn? 1.0f : -1.0f,
                upOn? 1.0f : -1.0f
            );
        }
    }
}
