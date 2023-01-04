using UnityEngine;

public class FightObject : MonoBehaviour
{
    [SerializeField] private GameObject _hands;
    [SerializeField] private WeaponManager _weaponManager;

    private void Update() => 
        SetCurrentObject();

    private void SetCurrentObject()
    {
        if (_weaponManager.currentSelectedWeaponID == -1)
            _hands.SetActive(true);
        else
            _hands.SetActive(false);
    }
}