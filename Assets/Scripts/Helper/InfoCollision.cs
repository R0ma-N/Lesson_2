using UnityEngine;

namespace Geekbrains
{
	//ИНКАПСУЛЯЦИЯ МЕССЕДЖЕЙ СНАРЯДОВ ДЛЯ ОБЪЕКТОВ СТОЛКНОВЕНИЙ
    public struct InfoCollision
	{
        private readonly Vector3 _dir;
		private readonly float _damage;

        //передаем, какой будет нести урон и направление в какую сторону летит, чтобы предать ускорение
		public InfoCollision(float damage, Vector3 dir = default)
		{
			_damage = damage;
			_dir = dir;
		}

		public Vector3 Dir => _dir;

		public float Damage => _damage;
	}
}