using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIvent
{

    public class Player : Character
    {        
        public Player(GameManager gm, float x, float y) : base(gm)
        {
            pos = new PointF(x, y);
            init(gm);
            radius = 50;
            this.name = "You"; 
            gm.table.Add(name,foodScale);
        }
        public override void Step()
        {
            base.Step();
            pos += direction * speed / 100;                       
            gm.Camera = pos;                       
        }
        public override void Render(Graphics g) => g.DrawEllipse(new Pen(Color.Red), GetRectangle());
    }
}   

