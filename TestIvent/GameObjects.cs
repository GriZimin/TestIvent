using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIvent
{
    public abstract class GameObject
    {
        public PointF pos, renderPos;
        
        public abstract void Step();
        public abstract void Render(Graphics g);
        public abstract void Spawn();
        public abstract void ProcIntersection();
        public static Boolean GetIntersection(GameObject g0, GameObject g1) => Math.Max(g1.radius,g0.radius) > new Vector(g0.pos, g1.pos).getLength();
        public float radius;
        public int foodScale = 1;
        public Vector direction = new Vector(0, 0);
        public List<GameObject> intersections = new List<GameObject>();


        protected GameManager gm;

        public void init(GameManager gm)
        {
            this.gm = gm;
            gm.Render += Render;
            gm.Step += Step;
        }
        public void SetDirection(float x, float y) => direction = new(x, y);
        public void SetDirection(float x, float y,float length) => direction = new Vector(x, y).ToLength(length);
        public virtual void Split(PointF StarPos)
        {

        }
       
    }


    public class Enemy : Character 
    {              
        int aim;
        public Enemy(GameManager gm, string name) : base(gm)
        {
            init(gm);
            Spawn();           
            radius = 50;
            direction = new(0,0);
            this.name = name;
            gm.table.Add(name, 0);
            Rerouting();
        }               
        public override void Render(Graphics g) => g.FillEllipse(new SolidBrush(Color.Green), GetRectangle());
        public override void Step()
        {    
            base.Step();
            pos += direction * speed;            
        }
        public void Rerouting()
        {
            gm.foodList[aim].EventWasEtten -= Rerouting;
            aim = gm.rand.Next(0, 150);
            gm.foodList[aim].EventWasEtten += Rerouting;
            direction = new Vector(pos, gm.foodList[aim].pos).ToUnitVector();
        }
    }
    
}
