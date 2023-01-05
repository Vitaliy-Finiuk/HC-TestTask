 using System;
 using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Characters.Hero
{
    public class HealthHandler : MonoBehaviour
    {       
        public int Health;

        [SerializeField] private Image[] _hearts;

        private void Start()
        {
            UpdateHealth();
        }

        public void UpdateHealth()
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < Health)
                {
                    _hearts[i].color = Color.red;
                }
                else
                {
                    _hearts[i].color = Color.gray;
                }
            }
        }
    }
}