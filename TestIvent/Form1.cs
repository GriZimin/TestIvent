namespace TestIvent
{
    public partial class Form1 : Form
    {
        Bitmap frame;
        Graphics g, gf;
        GameManager gm = new GameManager();
        public delegate void framer(Graphics g);
        public event framer? oneFrame;
        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            frame = new Bitmap(800, 800);
            gf = Graphics.FromImage(frame);
            gm.character = new Player(gm, 100, 100);
            
            for (int i = 0; i <= 3750; i++)
            {
                new Food(gm);
            }
            for (int i = 0;i < 15;i++)
            {
                new Enemy(gm,$"Enemy{i}");
            }
            for (int i = 0;i <= 5; i++)
            {
                new Star(gm, 200 * (i + 1), 200 * (i + 1));
            }
            oneFrame += gm.renderFrame;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gm.keys.Add(e.KeyCode);
            if (e.KeyCode == Keys.W) gm.character.pos.Y -= 3;
            if (e.KeyCode == Keys.A) gm.character.pos.X -= 3;
            if (e.KeyCode == Keys.S) gm.character.pos.Y += 3;
            if (e.KeyCode == Keys.D) gm.character.pos.X += 3;
            if (e.KeyCode == Keys.PageUp) gm.cameraScale += 0.1F;
            if (e.KeyCode == Keys.PageDown) gm.cameraScale -= 0.1F;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Vector mousePos = new(e.Location - new Vector(Width / 2, Height / 2));
            if (mousePos.getLength() <= 100) gm.character.direction = mousePos;
            else gm.character.direction = mousePos.ToLength(100);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            oneFrame?.Invoke(gf);
            g.DrawImage(frame, 0, 0);
        }
        

    }





}