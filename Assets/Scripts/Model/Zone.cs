using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class Zone : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;

        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                _enemy.GetComponent<Patrol>().isAgry = false;
                _enemy.GetComponent<Patrol>().MoveNextPoint();
            }
        }
    }
}

