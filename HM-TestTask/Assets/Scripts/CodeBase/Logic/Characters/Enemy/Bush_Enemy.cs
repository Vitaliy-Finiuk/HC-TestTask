using UnityEngine;

namespace CodeBase.Logic.Characters.Enemy
{
    public class Bush_Enemy : MonoBehaviour
    {
        private const string AI = "AI";
    
        public LayerMask bushMask = 8;
        public LayerMask floorMask = 15;

        private Rigidbody2D rigidbody2D;
        bool inFloor = false; 

        private void Start()
        {
            inFloor = false;
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (rigidbody2D.velocity.magnitude < 1) 
                inFloor = true;

            if (inFloor)
            {
                rigidbody2D.bodyType = RigidbodyType2D.Static;
                gameObject.layer = Utils.Utils.LayerMaskToLayer(floorMask);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            gameObject.layer = Utils.Utils.LayerMaskToLayer(bushMask);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (collision.gameObject.CompareTag(AI))
            {
                if (!inFloor)
                {
                    if (enemy) 
                        enemy.Bush();
                
                    inFloor = true;
                }
            }

        }
    }
}