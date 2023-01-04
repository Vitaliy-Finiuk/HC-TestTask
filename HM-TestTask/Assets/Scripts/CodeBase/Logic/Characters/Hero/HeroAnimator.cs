using System;
using CodeBase.Infrastucture;
using CodeBase.Logic.Weapons;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Logic.Characters.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("move");
        private static readonly int Weapon = Animator.StringToHash("weapon");
        private static readonly int Drop = Animator.StringToHash("drop");

        public Animator _animator;
        private IInputService _inputService;
        private WeaponManager _weaponManager;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _weaponManager = GetComponent<WeaponManager>();
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (_inputService.Axis.magnitude > 0)
                _animator.SetBool(MoveHash, true);
            else
                _animator.SetBool(MoveHash, false);
        }

        public void SelectedWeapon()
        {
            if ((_weaponManager.currentSelectedWeaponID != -1) && (_weaponManager.currentSelectedWeaponID != 2) &&
                (_weaponManager.currentSelectedWeaponID != 3))
                _animator.SetBool(Weapon, true);
            else
                _animator.SetBool(Weapon, false);
        }

        public void DropItem() => 
            _animator.SetTrigger(Drop);
    }
}