using System;
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
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }


        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }


        public static void Load()
        {
            _objs = new BaseObject[100];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(10, 10));
            _asteroids = new Asteroid[50];
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

         

        }




        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                { //System.Media.SystemSounds.Hand.Play(); }
                  // У меня не играл звук, внизу я поставил чтобы проверить проходит или нет
                  // MessageBox.Show("Столкновение");
                    a.UpdatePlace();


                }
            }
            _bullet.Update();
        }





    }
}
