using System;
using System.Drawing;

namespace MyGame
{
    public delegate void Message();
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;

            //* Создать собственное исключение GameObjectException, 
            //которое появляется при попытке создать объект с неправильными характеристиками
            if (pos.X<0 || pos.Y <0) throw new GameObjectException("Нельзя отрицательные значения");
            if (Convert.ToInt32(dir.X) > 100 || Convert.ToInt32(dir.Y) > 100) throw new GameObjectException("Слишком большая скорость!");
        }

       


        public abstract void Draw();
      //  2.	Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
        public abstract void Update();
      

       
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);

    }

    class Ship : BaseObject
    {
        private Image _image;
        public static event Message MessageDie;
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public void EnergyAdd(int n)
        {
            _energy += n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _image = Image.FromFile("ship.png");
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y, 30,30);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }

    }

    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
        /// <summary>
        /// 3.	Сделать так, чтобы при столкновении пули с астероидом они регенерировались в разных концах экрана.
        /// </summary>
        public void UpdatePlace() //добавил UpdatePlace и астероид переносится рандомно
        {
            var rnd = new Random();
            Pos.X = rnd.Next(0, Game.Height);
            Pos.Y = rnd.Next(0, Game.Height);
        }
    }
    /// <summary>
    /// 3.	Aптечки, которые добавляют энергию
    /// </summary>
    class Medicine : BaseObject
    {
        public int Power { get; set; }
        public Medicine(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 4;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Green, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 4;
        }
    }



    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;

        }
    }

    
}
