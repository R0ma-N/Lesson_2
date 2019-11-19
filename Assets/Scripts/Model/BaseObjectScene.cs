using UnityEngine;

namespace Geekbrains
{
	public abstract class BaseObjectScene : MonoBehaviour
	{
		//у всех объектов есть
        //слой
        private int _layer;
        //цвет
		private Color _color;
        //видим/нет
		private bool _isVisible;
        //физ.тело
		[HideInInspector] public Rigidbody Rigidbody;

		#region UnityFunction
        //ссылка на физ.тело
		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
		}
		#endregion

		#region Property

		/// <summary>
		/// Имя объекта
		/// </summary>
		public string Name
		{
			get => gameObject.name;
			set => gameObject.name = value;
		}

		/// <summary>
		/// Слой объекта
		/// </summary>
        //пробегаемся асклэйером по всем вложенным объектам и назначаем слой
		public int Layer
		{
			get => _layer;

			set
			{
				_layer = value;
				AskLayer(transform, value);
			}
		}

		/// <summary>
		/// Цвет материала объекта
		/// </summary>
        //пробегаемся аскалором по вложенным объектам и меняем им цвет
		public Color Color
		{
			get => _color;
			set
			{
				_color = value;
				AskColor(transform, _color);
			}
		}

        //для скрытия объекта через компонент "рендерер"
		public bool IsVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				var tempRenderer = GetComponent<Renderer>();
				if (tempRenderer)
					tempRenderer.enabled = _isVisible;
				if (transform.childCount <= 0) return;
				foreach (Transform d in transform)
				{
					tempRenderer = d.gameObject.GetComponent<Renderer>();
					if (tempRenderer)
						tempRenderer.enabled = _isVisible;
				}
			}
		}

		#endregion

		#region PrivateFunction
		/// <summary>
		/// Выставляет слой себе и всем вложенным объектам в независимости от уровня вложенности
		/// </summary>
		/// <param name="obj">Объект</param>
		/// <param name="lvl">Слой</param>
		private void AskLayer(Transform obj, int lvl)
		{
			obj.gameObject.layer = lvl;
			if (obj.childCount <= 0) return;
			foreach (Transform d in obj)
			{
				AskLayer(d, lvl);
			}
		}

		private void AskColor(Transform obj, Color color)
		{
			foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
			{
				curMaterial.color = color;
			}
			if (obj.childCount <= 0) return;
			foreach (Transform d in obj)
			{
				AskColor(d, color);
			}
		}
		#endregion
		
		/// <summary>
		/// Выключает физику у объекта и его детей
		/// </summary>
        // 26:55
		public void DisableRigidBody()
		{
			var rigidbodies = GetComponentsInChildren<Rigidbody>();
			foreach (var rb in rigidbodies)
			{
				rb.isKinematic = true;
			}
		}

		/// <summary>
		/// Включает физику у объекта и его детей
		/// </summary>
        //включает в физику с сообщением энергии. типа выбросить предмет
		public void EnableRigidBody(float force)
		{
			EnableRigidBody();
			Rigidbody.AddForce(transform.forward * force);
		}

		/// <summary>
		/// Включает физику у объекта и его детей
		/// </summary>
		public void EnableRigidBody()
		{
			var rigidbodies = GetComponentsInChildren<Rigidbody>();
			foreach (var rb in rigidbodies)
			{
				rb.isKinematic = false;
			}
		}

		/// <summary>
		/// Замораживает или размораживает физическую трансформацию объекта
		/// </summary>
		/// <param name="rigidbodyConstraints">Трансформацию которую нужно заморозить</param>
		public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
		{
			var rigidbodies = GetComponentsInChildren<Rigidbody>();
			foreach (var rb in rigidbodies)
			{
				rb.constraints = rigidbodyConstraints;
			}
		}

        //чтобы что-то выключить. видимость, коллайдер и пр.
		public void SetActive(bool value)
		{
			IsVisible = value;

			var tempCollider = GetComponent<Collider>();
			if (tempCollider)
			{
				tempCollider.enabled = value;
			}
		}

	}
}