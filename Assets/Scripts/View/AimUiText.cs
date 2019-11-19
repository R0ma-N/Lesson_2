using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
	//КЛАСС ДЛЯ ИНТЕРФЕЙСА
    public class AimUiText : MonoBehaviour
	{
		//список всех мишеней
        private Aim[] _aims;
        //куда выводить текст
		private Text _text;
        //---для примера с кнопкою
		private Button _button;
        //кол-во очков за сбитые мишени
		private int _countPoint;

        //инициализация
        private void Awake()
		{
            //находим мишени, добавляем в _aims
            _aims = FindObjectsOfType<Aim>();
            //получаем компонент Text
            _text = GetComponent<Text>();
		}

        //включение подписки
		private void OnEnable()
		{
			//
            foreach (var aim in _aims)
			{
				aim.OnPointChange += UpdatePoint;
			}
			
            //---подписка на событие
			_button.onClick.AddListener(Call);
		}

        //выключение подписки
		private void OnDisable()
		{
			foreach (var aim in _aims)
			{
				aim.OnPointChange -= UpdatePoint;
			}
			
            //---отписка от события
			_button.onClick.RemoveListener(Call);
		}

        //метод для подписки сообщений об уничтожении мишени
		private void UpdatePoint()
		{
			var pointTxt = "очков";
			++_countPoint;
			if (_countPoint >= 5) pointTxt = "очков";
			else if (_countPoint == 1) pointTxt = "очко";
			else if (_countPoint < 5) pointTxt = "очка";
			_text.text = $"Вы заработали {_countPoint} {pointTxt}";
		}

        //---событие для примера с кнопкой
		private void Call()
		{
			Debug.Log("Example");
		}
	}
}