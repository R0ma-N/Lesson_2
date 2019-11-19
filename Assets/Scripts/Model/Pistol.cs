
namespace Geekbrains
{
    public class Pistol : Weapon
    {
        //реализация стрельбы
        public override void Fire()
        {
            //если не можем стрелять - не стреляем
            if (!_isReady) return;
            //если нет пуль - не стреляем
            if (Clip.CountAmmunition <= 0) return;
            //создаем экземпляр пули в позиции дула
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            //придаем импульс пуле
            temAmmunition.AddForce(_barrel.forward * _force);
            //вычитаем пульку из обоймы
            Clip.CountAmmunition--;
            //не готовы сделать сразу следующий выстрел
            _isReady = false;
            //вызываем метод через _rechergeTime, который переключит флаг готовности
            //для выстрела
            Invoke(nameof(ReadyShoot), _rechergeTime);

            //лирическое отступление на реализацию класса Timer из Helper
            //_timer.Start(_rechergeTime);
        }
    }
}
