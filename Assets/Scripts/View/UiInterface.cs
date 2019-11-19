using UnityEngine;

namespace Geekbrains
{
	//ДЛЯ ХРАНЕНИЯ ССЫЛОК НА ЛЮБОЙ ИНТЕРЕСУЮЩИЙ ИНТЕРФЕЙС
    public class UiInterface
	{
		private FlashLightUiText _flashLightUiText;

        //при попытке получить ссылку на элемент интерфейса делаем проверку...


		public FlashLightUiText LightUiText => _flashLightUiText ? _flashLightUiText :
			(_flashLightUiText = Object.FindObjectOfType<FlashLightUiText>());

		private FlashLightUiBar _flashLightUiBar;

		public FlashLightUiBar FlashLightUiBar
		{
			get
			{
				//...если элемента нет
                if (!_flashLightUiBar)
                    //...пробуем найти на сцене
					_flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                //если находим, то именно его и возвращаем
				return _flashLightUiBar;
			}
		}

		private WeaponUiText _weaponUiText;

		public WeaponUiText WeaponUiText
		{
			get
			{
				if (!_weaponUiText)
					_weaponUiText = Object.FindObjectOfType<WeaponUiText>();
				return _weaponUiText;
			}
		}

		private SelectionObjMessageUi _selectionObjMessageUi;

		public SelectionObjMessageUi SelectionObjMessageUi
		{
			get
			{
				if (!_selectionObjMessageUi)
					_selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
				return _selectionObjMessageUi;
			}
		}
	}
}