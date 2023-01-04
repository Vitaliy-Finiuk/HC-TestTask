#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private const string TriggerByShoot = "Trigger By Shoot";
    private Enemy _enemy;
    public override void OnInspectorGUI()
    {
        _enemy = (Enemy)target;

        DrawDefaultInspector();
        GUILayout.Space(10);

        if (Application.isPlaying) {
            if (GUILayout.Button(TriggerByShoot)) {
                _enemy.TriggerByShoot();
            }
        }

    }
}
#endif