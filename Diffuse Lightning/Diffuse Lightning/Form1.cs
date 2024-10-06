using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Diffuse_Lightning
{
    public partial class Form1 : Form
    {
        private Microsoft.DirectX.Direct3D.Device device;
        private CustomVertex.PositionNormalColored[] vertex = new CustomVertex.PositionNormalColored[3];

        public Form1()
        {
            InitializeComponent();
            InitDevice();
        }

        public void InitDevice()
        {
            // Initialize the present parameters for the device
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            // Create the Direct3D device
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);

            // Set the projection matrix (perspective view)
            device.Transform.Projection = Matrix.PerspectiveFovLH(3.14f / 4, device.Viewport.Width / (float)device.Viewport.Height, 1f, 1000f);

            // Set the view matrix (camera)
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, 10), new Vector3(), new Vector3(0, 1, 0));

            // Set up the vertex data
            vertex[0] = new CustomVertex.PositionNormalColored(new Vector3(0, 1, 1), new Vector3(1, 0, 1), Color.Red.ToArgb());
            vertex[1] = new CustomVertex.PositionNormalColored(new Vector3(-1, -1, 1), new Vector3(1, 0, 1), Color.Red.ToArgb());
            vertex[2] = new CustomVertex.PositionNormalColored(new Vector3(1, -1, 1), new Vector3(-1, 0, 1), Color.Red.ToArgb());

            // Enable lighting and configure the light
            device.RenderState.Lighting = true;
            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Diffuse = Color.Plum;
            device.Lights[0].Direction = new Vector3(0.8f, 0, -1);
            device.Lights[0].Enabled = true;
        }

        public void Render()
        {
            // Clear the screen
            device.Clear(ClearFlags.Target, Color.CornflowerBlue, 1, 0);

            // Begin rendering the scene
            device.BeginScene();

            // Draw the vertices as triangles
            device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertex.Length / 3, vertex);

            // End rendering the scene
            device.EndScene();

            // Present the rendered scene to the display
            device.Present();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Any additional load logic can be added here
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Render the scene
            Render();
        }
    }
}
