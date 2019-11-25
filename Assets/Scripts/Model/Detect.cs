using UnityEngine;

namespace Geekbrains
{
    public class Detect : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                _enemy.GetComponent<Patrol>().isAgry = true;
            }
        }
    }
}

