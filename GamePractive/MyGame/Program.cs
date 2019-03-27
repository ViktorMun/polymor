using System;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form
            {
                Width = 999,
                Height = 999
            };
            //    4.Сделать проверку на задание размера экрана в классе Game. Если высота или ширина(Width, Height) больше 1000 или принимает отрицательное значение, выбросить исключение ArgumentOutOfRangeException().
            if (form.Height > 1000 || form.Width > 1000) throw new ArgumentOutOfRangeException("Form size", "Size need to be lower");
            if (form.Height < 0 || form.Width < 0) throw new ArgumentOutOfRangeException("Form size", "Size need to be bigger");





            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);

        }

    }
}
