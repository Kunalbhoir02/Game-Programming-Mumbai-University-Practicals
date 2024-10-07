using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace model_rendering5
{
    public partial class Form1 : Form
    {
        private Microsoft.DirectX.Direct3D.Device device;
        private Microsoft.DirectX.Direct3D.Texture texture;
        private Microsoft.DirectX.Direct3D.Font font;

        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load); // Hook up the Load event
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDirectX();
        }

        private void InitializeDirectX()
        {
            InitDevice();
            InitFont();
            LoadTexture();
        }

        private void InitFont()
        {
            System.Drawing.Font f = new System.Drawing.Font("Arial", 16f, FontStyle.Regular);
            font = new Microsoft.DirectX.Direct3D.Font(device, f);
        }

        private void LoadTexture()
        {
            try
            {
                texture = TextureLoader.FromFile(device, "E:\\TYCS\\images\\img1.jpg", 400, 400, 1, 0, Format.A8B8G8R8, Pool.Managed, Filter.Point, Filter.Point, Color.Transparent.ToArgb());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading texture: " + ex.Message);
            }
        }

        private void InitDevice()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);
        }

        private void Render()
        {
            device.Clear(ClearFlags.Target, Color.CornflowerBlue, 0, 1);
            device.BeginScene();
            using (Sprite s = new Sprite(device))
            {
                s.Begin(SpriteFlags.AlphaBlend);
                s.Draw2D(texture, new Rectangle(0, 0, 0, 0), new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height), new Point(0, 0), 0f, new Point(0, 0), Color.White);
                font.DrawText(s, "KMA College", new Point(10, 10), Color.Black);
                s.End();
            }
            device.EndScene();
            device.Present();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (texture != null) texture.Dispose();
            if (font != null) font.Dispose();
            if (device != null) device.Dispose();
            base.OnClosed(e);
        }
    }
}
