using System.Drawing;
interface ICollision
{
    bool Collision(ICollision obj);
    Rectangle Rect { get; }
}
