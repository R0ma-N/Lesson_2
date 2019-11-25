using UnityEngine;

namespace Geekbrains
{
	//ПУЛЯ. ЧАСТНОСТЬ ОТ ТИПОВ СНАРЯДА
    public sealed class BulletEnemy : Ammunition
	{
		
        private void OnCollisionEnter(Collision collision)
		{

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