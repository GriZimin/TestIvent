namespace TestIvent
{
    public class Food : GameObject
    {
        Color myColor;
        Random random = new Random();

        public delegate void WasEtten();
        public event WasEtten? EventWasEtten;


        public Food(GameManager gm,int foodScale = 1, float x = 0, float y = 0)
        {
            this.foodScale = foodScale;
            init(gm);
            myColor = Color.FromArgb(random.Next(0, 250), random.Next(0, 250), random.Next(0, 250));
            if (foodScale == 1) radius = 3;
            else radius = (foodScale / 15) + 15;           
            gm.foodList.Add(this);
            if (x != 0 || y != 0) pos = new(x, y);
            else Spawn();
        }
        public override void Step()
        {
            if (direction.getLength() > 0)
            {
                pos += direction;
                direction = direction.ToLength((int)direction.getLength() - 1);

            }
        }
        public override void Render(Graphics g)
        {
            renderPos = gm.toFrameCoordinats(pos);
            if (renderPos.X < 800 + radius && renderPos.Y < 800 + radius && renderPos.X > -radius && renderPos.Y > -radius)
            {
                RectangleF scope = new(renderPos + new Vector(-radius, -radius) / gm.cameraScale, new SizeF(radius * 2, radius * 2) / gm.cameraScale);
                g.FillEllipse(new SolidBrush(myColor), scope);
            }
        }
        public override void Spawn()
        {                        
            PointF npos;
            do
            {

                npos = new PointF(gm.rand.Next(0, gm.fieldWidth), gm.rand.Next(0, gm.fieldHeight));

            }
            while (CheckPos(npos));
            pos = npos;
            EventWasEtten?.Invoke();
        }
        private bool CheckPos(PointF pos)
        {
            foreach (var i in gm.characters) if (i.radius >= new Vector(i.pos,pos).getLength()) return true;
            return false;
        }
        public override void ProcIntersection()
        {

        }
    }
}
