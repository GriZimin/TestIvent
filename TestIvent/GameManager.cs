using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIvent
{
    public class GameManager
    {       
        public List<Character> characters = new List<Character>();
        public List<Food> foodList = new List<Food>();        
        public List<Keys> keys = new List<Keys>();
        public Dictionary<String, int> table = new();
        public Character character;
        public int fieldWidth = 5000;
        public int fieldHeight = 5000;
        public int speed = 4;
        public PointF Camera = new PointF(0, 0);
        public float cameraScale = 1;
        public Random rand = new Random();
        public PointF toFrameCoordinats(float x, float y) => new PointF(400 +( x - Camera.X) / cameraScale, 400 +( y - Camera.Y)/cameraScale);
        public PointF toFrameCoordinats(PointF pos) => new PointF(400 + (pos.X - Camera.X)/ cameraScale , 400 + (pos.Y - Camera.Y)/cameraScale);
        public string GetTable()
        {
            string s = "";
            foreach (var i in table)
            {
                s += $"{i.Key}:{i.Value}\n";
            }
            return s;
        }



        public delegate void RenderFrame(Graphics g);
        public event RenderFrame? Render;

        public delegate void ObjsStep();
        public event ObjsStep? Step;
        
        public void renderFrame(Graphics gf)
        {
            gf.Clear(Color.White);
            Step?.Invoke();
            Render?.Invoke(gf);
            gf.DrawLine(new Pen(Color.Black), toFrameCoordinats(0, 0), toFrameCoordinats(0, fieldHeight));
            gf.DrawLine(new Pen(Color.Black), toFrameCoordinats(fieldWidth, fieldHeight), toFrameCoordinats(fieldWidth, 0));
            gf.DrawLine(new Pen(Color.Black), toFrameCoordinats(0, 0), toFrameCoordinats(fieldWidth, 0));
            gf.DrawLine(new Pen(Color.Black), toFrameCoordinats(0, fieldHeight), toFrameCoordinats(fieldWidth, fieldHeight));
            gf.DrawString(GetTable(), new("Calibri", 15), new SolidBrush(Color.DarkBlue), new PointF(10, 10));
        }
    }
}
