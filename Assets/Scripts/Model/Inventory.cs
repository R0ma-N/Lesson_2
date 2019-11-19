using UnityEngine;

namespace Geekbrains
{
	//ИНВЕНТАРЬ. хранит все ссылки, которые будут отображатся пользователю(?)
    //не наследуется ни от какого класса
    public class Inventory : IInitialization
	{
		//массив оружий
        private Weapon[] _weapons = new Weapon[5];

		public Weapon[] Weapons => _weapons;

        //фонарь
		public FlashLightModel FlashLight { get; private set; }

		public void OnStart()
		{
			//находим список всего оружия
            _weapons = Main.Instance.Player.GetComponentsInChildren<Weapon>();

            //пробегаемся по всему оружию и выключаем его
			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

            //находим фонарь на сцене
			FlashLight = Object.FindObjectOfType<FlashLightModel>();
		}

		//todo Добавить функционал для удаления, получения, добавления оружения
        //для вызова из InputController

        public void RemoveWeapon(Weapon weapon)
        {
            
        }
	}
}