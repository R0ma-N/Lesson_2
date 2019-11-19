using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	//БАЗОВЫЙ КЛАСС ДЛЯ ВСЕХ ПУЛЬ
    public abstract class Weapon : BaseObjectScene
	{
		//макс. кол-во пуль в обойме (для рандома)
        private int _maxCountAmmunition = 40;
        //мин. кол-во пуль в обойме (для рандома)
        private int _minCountAmmunition = 20;
        //кол-во обойм
		private int _countClip = 5;
        //класс для самой пульки
		public Ammunition Ammunition;
        //структура для обоймы. хранит колличество пулек
		public Clip Clip;

        //массив пулек из собственных типов пулек
        //для трассирующих пуль например
		protected AmmunitionType[] _ammunitionType = {AmmunitionType.BulletAK47};


		//точка вылета пуль
        [SerializeField] protected Transform _barrel;
        //сила полета пуль
		[SerializeField] protected float _force = 999;
        //время перезарядки пульки
		[SerializeField] protected float _rechergeTime = 0.2f;
        //очередь из обойм
		private Queue<Clip> _clips = new Queue<Clip>();

        //готовность к стрельбе
		protected bool _isReady = true;
		//protected Timer _timer = new Timer();

            //инициализация
		protected virtual void Start()
		{
			//добавляем пули в обойму
            for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip { CountAmmunition = Random.Range(_minCountAmmunition, _maxCountAmmunition) });
			}

            //заряжаем первую обойму
			ReloadClip();
		}

        //заготовка под то, каким образом будет происходить стрельба
		public abstract void Fire();

		//protected virtual void Update()
		//{
		//	_timer.Update();
		//	if (_timer.IsEvent())
		//	{
		//		ReadyShoot();
		//	}
		//}

            //готовность к стрельбе
		protected void ReadyShoot()
		{
			_isReady = true;
		}

        //добавляем обойму в список обойм
		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

        //перезаряжаем обойму
		public void ReloadClip()
		{
			//если обойм нет, то ничего
            if (CountClip <= 0) return;
            //
			Clip = _clips.Dequeue();
		}

        //текущее кол-во обойм, чтоб выводить на экран
		public int CountClip => _clips.Count;
	}
}