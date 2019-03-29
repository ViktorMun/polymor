﻿using System;
using System.Windows.Forms;
using System.Drawing;
namespace MyGame
{
    static class Game
    {

       

        static BaseObject[] _objs;
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        private static Medicine[] _medicine;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        private static Timer _timer = new Timer();
        public static Random Rnd = new Random();


        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
             Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
             Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
           Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
          
            _timer.Start();
            _timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
                if (e.KeyCode == Keys.Up) _ship.Up();
                if (e.KeyCode == Keys.Down) _ship.Down();
     

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        public static void Load()
        {
            _objs = new BaseObject[100];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(10, 10));
            _asteroids = new Asteroid[50];
            _medicine = new Medicine[10];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(2, 2));
            }
           for (var i = 0; i < _asteroids.Length; i++)
           {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
            for (var i = 0; i < _medicine.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _medicine[i] = new Medicine(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(8, 8));
            }


        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Medicine m in _medicine)
            {
                m?.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();

                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();


                if (!_ship.Collision(_medicine[i])) continue;

                _ship?.EnergyAdd(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
            }
            for (var i = 0; i < _medicine.Length; i++)
            {
                if (_medicine[i] == null) continue;
                _medicine[i].Update();

                if (!_ship.Collision(_medicine[i])) continue;
                var rnd = new Random();
                _ship?.EnergyAdd(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();

            }

        }



    }
}
