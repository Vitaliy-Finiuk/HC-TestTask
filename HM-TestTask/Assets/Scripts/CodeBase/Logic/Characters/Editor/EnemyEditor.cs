#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic.Characters.Editor
{
    [CustomEditor(typeof(Enemy.Enemy))]
    public class EnemyEditor : UnityEditor.Editor
    {
        private const string TriggerByShoot = "Trigger By Shoot";
        private Enemy.Enemy _enemy;
        public override void OnInspectorGUI()
        {
            _enemy = (Enemy.Enemy)target;

            DrawDefaultInspector();
            GUILayout.Space(10);

            if (Application.isPlaying) {
                if (GUILayout.Button(TriggerByShoot)) {
                    _enemy.TriggerByShoot();
                }
            }

        }
    }
}
#endif