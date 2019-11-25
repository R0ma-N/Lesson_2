using UnityEngine.AI;
using UnityEngine;

namespace Geekbrains
{
    public class Patrol : MonoBehaviour
    {
        public Transform[] Cells;
        public bool isAgry;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _startToShot;
        private Renderer _rend;
        private NavMeshAgent _agent;
        private Transform _player;
        private Transform _distPoint;
        private int _randomPoint;
        private float _pauseBtwShots;

        void Start()
        {
            _rend = GetComponent<Renderer>();
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            isAgry = false;
            MoveNextPoint();
        }

        void Update()
        {
            if (Vector3.Distance(_distPoint.position, transform.position) <= 1)
            {
                MoveNextPoint();
            }

            if (isAgry)
            {
                _rend.material.SetColor("_Color", Color.red);
                _agent.destination = _player.position;
                if (_pauseBtwShots <= 0)
                {
                    Instantiate(_bullet, _barrel.position, _barrel.rotation);
                    _pauseBtwShots = _startToShot;
                }
                else
                {
                    _pauseBtwShots -= Time.deltaTime;
                }
            }
            
            if (!isAgry)
            {
                _rend.material.SetColor("_Color", Color.green);
            }
        }

        public void MoveNextPoint()
        {
            _randomPoint = Random.Range(0, Cells.Length);
            _distPoint = Cells[_randomPoint];
            _agent.destination = Cells[_randomPoint].position;
        }
    }
}

