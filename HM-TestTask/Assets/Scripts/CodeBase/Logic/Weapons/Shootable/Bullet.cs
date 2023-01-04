using CodeBase.Logic.Characters;
using UnityEngine;

namespace CodeBase.Logic.Weapons.Shootable
{
    public class Bullet : MonoBehaviour
    {
        private const string Glass = "Glass";
        private const string Side = "Side";
        private const string AI = "AI";
        private const string HeroPlayer = "Hero_Player";
        
        [SerializeField] private GameObject _hitBlood;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private int _damage = 50;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
       
            Vector2 dir = GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
            if (collision.CompareTag(AI) || collision.CompareTag(HeroPlayer))
            {
                GameObject effect = Instantiate(_hitBlood, transform.position, Quaternion.Euler(0, 0, angle));
            }
            else if (collision.CompareTag(Side))
            {
                GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.Euler(0, 0, angle));           
            }

            Unit unit = collision.gameObject.GetComponent<Unit>();

            if (unit)
            {
                unit.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
