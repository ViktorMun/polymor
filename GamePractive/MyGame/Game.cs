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
            _objs = new BaseObject[50];
            for (int i = 0; i < _objs.Length / 3; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
      
            for (int i = _objs.Length / 3; i < 21; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(3, 3));

            //1.	Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полет в звездном пространстве.
            for (int i = 21; i < 31 ; i++)
                _objs[i] = new Square(new Point(600, i * 20), new Point(-i, 0), new Size(6, 6));
            for (int i = 31; i < 41; i++)
                _objs[i] = new Curve(new Point(600, i * 20), new Point(-i, 0), new Size(6, 6));
            //2.	* Заменить кружочки картинками, используя метод DrawImage.
            for (int i =41; i < _objs.Length ; i++)
                _objs[i] = new Image(new Point(600, i * 20), new Point(-i, -i), new Size(30, 30));
        }


        public static void Draw()
        { Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));

            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

      

    }
}
