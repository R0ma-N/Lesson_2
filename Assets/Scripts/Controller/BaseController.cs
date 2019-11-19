
namespace Geekbrains
{
	//БАЗОВЫЙ ДЛЯ ВСЕХ КОНТРОЛЛЕРОВ
    public abstract class BaseController
	{
		//поскольку только контроллеры взаимодействуют с интерфейсом, создается класс, где хранятся ссылки на эти интерфейсы
        protected UiInterface UiInterface;
        
        //в конструкторе в UiInterface создается объекта класса UiInterface
        protected BaseController()
		{
			UiInterface = new UiInterface();
		}

        //флаг активности контроллера
		public bool IsActive { get; private set; }

        //
		public virtual void On()
		{
			On(null);
		}

		public virtual void On(BaseObjectScene obj)
		{
			IsActive = true;
		}

		public virtual void Off()
		{
			IsActive = false;
		}

		public void Switch()
		{
			if (IsActive)
			{
				Off();
			}
			else
			{
				On();
			}
		}
	}
}