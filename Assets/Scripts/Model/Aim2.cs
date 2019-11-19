using System;
using UnityEngine;

namespace Geekbrains
{
	//МИШЕНЬ. 
    public class Aim2 : MonoBehaviour, ISetDamage, ISelectObj
	{
		//Событие для опевещения об уничтожении мишени
        public event Action OnPointChange;
		
        //здоровье
		public float Hp = 101;
        private Vector3 _curPosition;
        //флаг смерти
		private bool _isDead;
		//todo дописать поглащение урона

            //получение урона
		public void SetDamage(InfoCollision info)
		{
			//если мертвы, то ничего
            if (_isDead) return;

            //если здоровье > 0 берем инфу об уроне, которую снаряд передал в InfoCollision и отнимаем
            //проигрываем звук попадания
            if (Hp > 0)
			{
				Hp -= info.Damage;
                GetComponent<AudioSource>().Play();
			}

            //если здоровья меньше 0
			if (Hp <= 0)
			{
				Destroy(gameObject);
                //опевещение об уничтожении мишени
                OnPointChange?.Invoke();
                //поднимаем флаг смерти
				_isDead = true;
			}
		}

		public string GetMessage()
		{
			return gameObject.name;
		}
	}
}