using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Draw_a_triangle
{
    public partial class Form1 : Form
    {
        Microsoft.DirectX.Direct3D.Device device;

        public Form1()
        {
            InitializeComponent();
            InitDevice();
        }

        private void InitDevice()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            device = new Device(0, DeviceType.Hardware, this,
            CreateFlags.HardwareVertexProcessing, pp);
        }

        private void Render()
        {
            // Create vertices for the triangle
            CustomVertex.TransformedColored[] vertexes = new CustomVertex.TransformedColored[3];

            // First point (green)
            vertexes[0].Position = new Vector4(240, 110, 0, 1.0f);
            vertexes[0].Color = System.Drawing.Color.FromArgb(0, 255, 0).ToArgb();

            // Second point (blue)
            vertexes[1].Position = new Vector4(380, 420, 0, 1.0f);
            vertexes[1].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();

            // Third point (red)
            vertexes[2].Position = new Vector4(110, 420, 0, 1.0f);
            vertexes[2].Color = System.Drawing.Color.FromArgb(255, 0, 0).ToArgb();

            // Clear screen with CornflowerBlue
            device.Clear(ClearFlags.Target, Color.CornflowerBlue, 1.0f, 0);

            // Begin drawing the scene
            device.BeginScene();
            device.VertexFormat = CustomVertex.TransformedColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, vertexes);
            device.EndScene();

            // Present the rendered scene
            device.Present();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Any initialization on form load can go here
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }
    }
}
