namespace TestIvent
{
    public class Character : GameObject
    {
        protected float speed = 4;
        public string name;
        public Character(GameManager gm)
        {
            gm.characters.Add(this);
            
        }        
        public override void Step()
        {
            pos.X = (pos.X + gm.fieldWidth) % gm.fieldWidth;
            pos.Y = (pos.Y + gm.fieldHeight) % gm.fieldHeight;
            radius = (foodScale / 15) + 15;
            speed = 3.5F + 2 / foodScale;
            foreach (Food food in gm.foodList)
            {
                Merger(food);
            }
            foreach (var c in gm.characters)
            {
                if (c != this) Merger(c);
            }
        }
        public override void Render(Graphics g)
        {

        }
        public override void Spawn()
        {
            int a, b;
            a = gm.rand.Next(0, gm.fieldWidth);
            b = gm.rand.Next(0, gm.fieldWidth);
            if (gm.rand.Next(0, 2) == 0)
            {
                pos = new PointF(a, b * gm.rand.Next(0, 2));
            }
            else pos = new PointF(b * gm.rand.Next(0, 2), a);
            foodScale = 1;
        }
        public override void ProcIntersection()
        {

        }
        public virtual void Merger(GameObject go)
        {
            if (go.radius * 1.1 <= radius && new Vector(pos, go.pos).getLength() <= radius)
            {
                foodScale += go.foodScale;                
                gm.table[this.name] = foodScale;
                go.Spawn();
            }
        }
        public override void Split(PointF StarPos)
        {
            for (int i = 0; i <= 5; i++)
            {
                int fs = foodScale / (7 - i);
                foodScale -= fs;
                gm.table[this.name] = foodScale;
                Vector dir = new Vector((float)Math.Sin(i * 60 * Math.PI / 180),(float)Math.Cos(i * 60 * Math.PI / 180)) * 50;
                var f = new Food(gm,fs,(StarPos + dir).X,(StarPos + dir).Y);
                f.SetDirection(dir.x,dir.y,40);               
            }
            
        }
        
        protected RectangleF GetRectangle()
        {
            renderPos = gm.toFrameCoordinats(pos);
            return new(renderPos + new Vector(-radius, -radius) / gm.cameraScale, new SizeF(radius * 2, radius * 2) / gm.cameraScale);
        }
    }
}
