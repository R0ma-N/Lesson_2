using UnityEngine;

namespace Geekbrains
{
	//КОНТРОЛЛЕР ВВОДА. РЕАЛИЗУЕТ БАЗОВЫЙ КОНТРОЛЛЕР И ИНТЕРФЕЙС ДЛЯ КОЛЛЕКТИВНОГО АПДЕЙТА
    public class InputController : BaseController, IOnUpdate
	{
        //фонарь по F
        //объект класса кейкод
        private KeyCode _activeFlashLight = KeyCode.F;
        //отмена по esc
		private KeyCode _cancel = KeyCode.Escape;
        //перезарядка по R
		private KeyCode _reloadClip = KeyCode.R;

        private int _curWeapon;

        private Weapon[] _weapons;

        public InputController()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		
        //то, что пойде в апдейт
		public void OnUpdate()
		{
			//переключение состояний фонаря
            if (!IsActive) return;
			if (Input.GetKeyDown(_activeFlashLight))
			{
				Main.Instance.FlashLightController.Switch();
			}

            //ДЗ todo реализовать выбор оружия по колесику мыши
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0 && !Main.Instance.WeaponController.IsActive)
            {
                SelectWeapon(0);
            }
            else if (scroll > 0 && _curWeapon < _weapons.Length - 1)
            {
                SelectWeapon(_curWeapon + 1);
            }
            else if (scroll < 0 && _curWeapon != 0)
            {
                SelectWeapon(_curWeapon - 1);
            }

            //выкл контроллеров оружия и фонаря
            if (Input.GetKeyDown(_cancel))
			{
				Main.Instance.WeaponController.Off();
				Main.Instance.FlashLightController.Off();
			}

            //перезарядка оружия
			if (Input.GetKeyDown(_reloadClip))
			{
				Main.Instance.WeaponController.ReloadClip();
			}



		}


		/// <summary>
		/// Выбор оружия
		/// </summary>
		/// <param name="i">Номер оружия</param>
		private void SelectWeapon(int i)
		{
            _curWeapon = i;
            //выключаем контроллер
            Main.Instance.WeaponController.Off();
            //берем из инветаря конкретный объект
            _weapons = Main.Instance.Inventory.Weapons;
            var tempWeapon = _weapons[i];
            //передаем контроллеру для оружия
			if (tempWeapon != null)
			{
				Main.Instance.WeaponController.On(tempWeapon);
			}
		}
	}
}