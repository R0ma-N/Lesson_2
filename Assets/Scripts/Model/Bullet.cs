using UnityEngine;

namespace Geekbrains
{
	//ПУЛЯ. ЧАСТНОСТЬ ОТ ТИПОВ СНАРЯДА
    public sealed class Bullet : Ammunition
	{
		//
        private void OnCollisionEnter(Collision collision)
		{
            // дописать доп урон

            //проверка на столкновение с объектом, реализующим интерфейс ISetDamage(попытка получить компонент)
            //интерфейс отвечает за обработку урона
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();


            if (tempObj != null)
			{
				//сталкивается с объектом и передает инфу об уроне и направлении
                tempObj.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
			}

            DestroyAmmunition();
		}
	}
}