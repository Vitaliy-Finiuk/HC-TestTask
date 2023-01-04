using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Characters.NoiseController
{
    public class NoiseUnitManager : MonoBehaviour
    {
        public bool BotsTriggerBots = true;

        public List<Unit> allUnits;
        public float distanceToTrigger = 8;

        public static NoiseUnitManager instance;
        //��������� ѳ��������
        public bool isAvailable => instance != null;

        public void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            instance = this;
        
            Unit[] unitArray = FindObjectsOfType<Unit>();
            allUnits = new List<Unit>(unitArray);
        }

        public void OnDeath(Unit unit)
        {
            allUnits.Remove(unit);
        }

        public void Trigger(Unit unit, float factor = 1)
        {
            for (int i = 0; i < allUnits.Count; i++)
            {
                if (unit.IsEnemy && !BotsTriggerBots) return;

                float distance = Vector3.Distance(unit.transform.position, allUnits[i].transform.position);
           
                if(distance < distanceToTrigger * factor)
                {
                    allUnits[i]?.onTrigger.Invoke();
                }

            }
        }
    }
}
