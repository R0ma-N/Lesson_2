using System;
using UnityEngine;

namespace Geekbrains
{
	//ДЛЯ ВЫДЕЛЕНИЯ ОБЪЕКТОВ
    public sealed class SelectionController : BaseController, IOnUpdate
	{
		//камера
        private readonly Camera _mainCamera;
        //центр экрана
		private readonly Vector2 _center;
        //дистанция, на которой чекается объект
		private readonly float _dedicateDistance = 20;
        //выделенный объект
		private GameObject _dedicatedObj;
        //закешированный выделенный объект, чтобы повторно не выделять объекты
		private ISelectObj _selectedObj;
        //вместо проверки на нул, бул дешевле
		private bool _nullString;
		private bool _isSelectedObj;

        //конструктор.
		public SelectionController()
		{
			//находим камеру
            _mainCamera = Camera.main;
            //находим центр экрана
			_center = new Vector2(Screen.width / 2, Screen.height / 2);
		}

        //тот, что потом будет запучкать в центролизованном апдейте
		public void OnUpdate()
		{
            //выпускается луч из центра экрана на _dedicateDistance
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center), out var hit, _dedicateDistance))
			{
                //передаем объект,кот. хотим выделить --> метод SelectObject
                SelectObject(hit.collider.gameObject);
				_nullString = false;
			}
            //повтороно строки не назаначаются
			else if(!_nullString)
			{
				UiInterface.SelectionObjMessageUi.Text = String.Empty;
				_nullString = true;
				_dedicatedObj = null;
				_isSelectedObj = false;
			}

            //если получилось выделить объект
			if (_isSelectedObj)
			{
				// Действие над объектом
                //например здесь. пробегаемся по свитчу. по типам объектов
				switch (_selectedObj)
				{
					//если тип - оружие
                    case Weapon aim:

                        // в инвентарь
                        //!!!!!!!!!!!!!!!ДЗ

                        //Inventory.AddWeapon(aim);
                        break;
                        //если тип - стена....
					case Wall wall:
						break;
				}
			}
		}

        //--> 
		private void SelectObject(GameObject obj)
		{
            //если объект уже был выделен, то его повторно не выделяем
            if (obj == _dedicatedObj) return;
            //пытыемся взять у объекта интерфейс ISelectObj,а он есть, если объект выделяемый
            _selectedObj = obj.GetComponent<ISelectObj>();
            //если получилось
			if (_selectedObj != null)
			{
                //берем у объекта сообщение, которое тот передает (_selectedObj.GetMessage()) и
                //выводим на экран через UiInterface.SelectionObjMessageUi.Text
                UiInterface.SelectionObjMessageUi.Text = _selectedObj.GetMessage();
                //говорим, что данный объект выделен
				_isSelectedObj = true;
			}
            //если не нашелся интерфейс ISelectObj
            else
            {
				//передаем пустую строку
                UiInterface.SelectionObjMessageUi.Text = String.Empty;
                //говорим, что объект не выделен
				_isSelectedObj = false;
			}
            //кэшиируем объект вне зависимостиот того, может он передать сообщение или нет
			_dedicatedObj = obj;
		}
	}
}