using System;
using UnityEngine;

namespace Geekbrains
{
	//МИШЕНЬ. 
    public class Aim3 : MonoBehaviour, ISetDamage, ISelectObj
	{
		//Событие для опевещения об уничтожении мишени
        public event Action OnPointChange;
		
        //здоровье
		public float Hp = 101;
        //флаг смерти
		private bool _isDead;
		//todo дописать поглащение урона

            //получение урона
		public void SetDamage(InfoCollision info)
		{
			//если мертвы, то ничего
            if (_isDead) return;

            //если здоровье > 0 берем инфу об уроне, которую снаряд передал в InfoCollision и отнимаем
            //опускаем мишень при попадании
            if (Hp > 0)
			{
				Hp -= info.Damage;
                transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            }

            //если здоровья меньше 0
			if (Hp <= 0)
			{
				//если не с РБ
                if (!GetComponent<Rigidbody>())
				{
					//пусть будет с РБ
                    gameObject.AddComponent<Rigidbody>();
				}
                //удаляем мишень через 10 сек
				Destroy(gameObject, 10);
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