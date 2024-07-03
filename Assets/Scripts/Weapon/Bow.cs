using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public void Attack()
    {
        Debug.Log("Bow Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
