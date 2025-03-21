using UnityEngine;

public interface IAttack
{
    public void EquipOnEnemy();
    public void ExecuteAttack(Transform muzzle, int amount);
}
