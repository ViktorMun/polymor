using System;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = Screen.PrimaryScreen.Bounds.Width;
            form.Height = Screen.PrimaryScreen.Bounds.Height;

            Game.Init(form);
            Game.Load();
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
