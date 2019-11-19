using UnityEngine;

namespace Geekbrains
{
	//БАЗОВЫЙ КОНТРОЛЛЕР ДЛЯ СТРЕЛЬБЫ
    public class WeaponController : BaseController, IOnUpdate
	{
		//оружие, которое производит выстрел
        private Weapon _weapon;
        //как-то берем из перечисления MouseButton нажатие в инте?!
        //::туду гуглить enum
        private int _mouseButton = (int)MouseButton.LeftButton;

        //в каждом кадре
		public void OnUpdate()
		{
            //если BaseController возвращает, что не активен, то ничего не делаем
            if (!IsActive) return;
            //если есть нажатие
			if (Input.GetMouseButton(_mouseButton))
			{
                //вызов метода стрельбы у объекта класса Weapon
                _weapon.Fire();
                //выводим на экран кол-во пуль и кол-во обойм
				UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
			}
		}

        //переопределяя из BaseController, делаем включение контроллера
        public override void On(BaseObjectScene weapon)
		{
            //если BaseController возвращает, что активен, то ничего не делаем
            if (IsActive) return;
            //передаем в метод вкыла базового класса объект класса оружия
			base.On(weapon);

            //назначаем оружие ?
            //:: as
			_weapon = weapon as Weapon;
            //если не получилось назначить(?), то ничего не делаем
			if (_weapon == null) return;
            //запускаем
            _weapon.IsVisible = true;
            //запускаем текст для вывода через поле базового класса
			UiInterface.WeaponUiText.SetActive(true);
            //выводим изначальные значения, полученные от объекта реализации данного класса
			UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
		}

        //переопределяя из BaseController, делаем выключение контроллера
        public override void Off()
		{
            //если BaseController возвращает, что не активен, то ничего не делаем
            if (!IsActive) return;
            //не переопределенный метод
			base.Off();
            //выключаем
			_weapon.IsVisible = false;
            //обнуляем ссылку
			_weapon = null;
            //выклучаем UI интерфейс
			UiInterface.WeaponUiText.SetActive(false);
		}

        //перезарядка обоймы
		public void ReloadClip()
		{
			//если ссылка на оружие не обнулена
            if (_weapon == null) return;
            //вызываем метод перезарядки из объекта, реализующего класс
			_weapon.ReloadClip();
            //обновляем инфу на экране
			UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
		}
	}
}