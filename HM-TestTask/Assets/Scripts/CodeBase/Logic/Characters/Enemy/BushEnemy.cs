using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BushEnemy : MonoBehaviour
    {
        private const string AI = "AI";

        [SerializeField] private LayerMask _bushMask = 8;
        [SerializeField] private LayerMask _floorMask = 15;

        private Rigidbody2D _rigidbody2D;
        private bool _inFloor = false; 

        private void Start()
        {
            _inFloor = false;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody2D.velocity.magnitude < 1) 
                _inFloor = true;

            if (_inFloor)
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                gameObject.layer = Utils.Utils.LayerMaskToLayer(_floorMask);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            gameObject.layer = Utils.Utils.LayerMaskToLayer(_bushMask);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (collision.gameObject.CompareTag(AI))
            {
                if (!_inFloor)
                {
                    if (enemy) 
                        enemy.Bush();
                
                    _inFloor = true;
                }
            }

        }
    }
}