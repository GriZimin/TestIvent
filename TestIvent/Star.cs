using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIvent
{
    internal class Star : Character
    {
        PointF aim;      
        public Star(GameManager gm, float x, float y) : base(gm)
        {
            pos = new PointF(x, y);
            init(gm);
            radius = 50;           
            speed = 1.5F;
            Rerouting();
            Spawn();
        }
        public override void Render(Graphics g) => g.DrawImage(Resources.Star,GetRectangle());
        public override void Step()
        {
            pos.X = (pos.X + gm.fieldWidth) % gm.fieldWidth;
            pos.Y = (pos.Y + gm.fieldHeight) % gm.fieldHeight;
            pos += direction * speed;
            radius = 50;
            foreach (var c in gm.characters)
            {
                if (c != this) Merger(c);
            }
        }        
        public override void Spawn()
        {
            pos = new PointF(gm.rand.Next(0, gm.fieldWidth), gm.rand.Next(0, gm.fieldHeight));
        }
        public override void Merger(GameObject go)
        {
            if (go.radius >= 60 && new Vector(pos, go.pos).getLength() <= go.radius)
            {
                go.Split(pos);
            }
        }
        public void Rerouting()
        {            
            aim =new(gm.rand.Next(0, gm.fieldWidth), gm.rand.Next(0, gm.fieldHeight));           
            direction = new Vector(pos, aim).ToUnitVector();
        }
    }

}
