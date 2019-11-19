using UnityEngine;

namespace Geekbrains
{
	//БАЗОВЫЙ КЛАСС ДЛЯ ВСЕХ СНАРЯДОВ
    public abstract class Ammunition : BaseObjectScene
	{
		//время через которое уничтожится
        [SerializeField] protected float _timeToDestruct = 10;
        //урон по умолчанию
		[SerializeField] protected float _baseDamage = 10;
        //текущий урон, т.к. урон может уменьшаться в зависимости от дальности, например
		protected float _curDamage;
        //через какоев ремя будет терять урон
		protected float _lossOfDamageAtTime = 0.2f;

        //тип пули
        public AmmunitionType Type = AmmunitionType.BulletAK47;

        //переопредялямый метод инициализации
		protected override void Awake()
		{
            //Awake из BaseObjectScene
            base.Awake();
            //текущий урон изначально равен урону по умолчанию
			_curDamage = _baseDamage;
		}

        //
		private void Start()
		{
            DestroyAmmunition(_timeToDestruct);
            //потеря велечины урона во время полета
            //с периодичностью в 1секунду 0,2 урона
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
		}

        //придает пуле импульс
		public void AddForce(Vector3 dir)
		{
			//проверка на наличие компонента RB
            if (!Rigidbody) return;
            //предание импульса
			Rigidbody.AddForce(dir);
		}

        //потеря силы урона
		protected void LossOfDamage()
		{
			_curDamage -= _lossOfDamageAtTime;
		}

        //
        protected void DestroyAmmunition(float timeToDestruct = 0)
        {
            //унижтожаем пулю через время = timeToDestruct
            Destroy(gameObject, timeToDestruct);
            //выключаем инвок, который запускали для потери урона взависимомости от времени
            CancelInvoke(nameof(LossOfDamage));
            // Вернуть в пул
        }
	}
}