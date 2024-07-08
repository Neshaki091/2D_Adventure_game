using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupspawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin;
    public void DropItems()
    {
        Instantiate(goldCoin, transform.position, Quaternion.identity);
    }
}