#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    Enemy enemy;
    public override void OnInspectorGUI()
    {
        enemy = (Enemy)target;

        DrawDefaultInspector();
        GUILayout.Space(10);

        if (Application.isPlaying) {
            if (GUILayout.Button("Trigger By Shoot")) {
                enemy.TriggerByShoot();
            }
        }

    }
}
#endif