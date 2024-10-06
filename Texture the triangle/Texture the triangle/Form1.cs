using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Texture_the_triangle
{
    public partial class Form1 : Form
    {
        private Microsoft.DirectX.Direct3D.Device device;
        private CustomVertex.PositionTextured[] vertex = new CustomVertex.PositionTextured[3];
        private Texture texture;

        public Form1()
        {
            InitializeComponent();
            InitDevice();
        }

        private void InitDevice()
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
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, 20), new Vector3(), new Vector3(0, 1, 0));

            // Disable lighting
            device.RenderState.Lighting = false;

            // Define the triangle vertices with texture coordinates
            vertex[0] = new CustomVertex.PositionTextured(new Vector3(0, 0, 0), 0, 0);
            vertex[1] = new CustomVertex.PositionTextured(new Vector3(5, 0, 0), 0, 1);
            vertex[2] = new CustomVertex.PositionTextured(new Vector3(0, 5, 0), -1, 1);

            // Load the texture from an image file
            texture = new Texture(device, new Bitmap("D:\\Pictures\\bjp.jpg"), 0, Pool.Managed);
        }

        private void Form1_Load(Object sender, EventArgs e)
        {
            // Any additional load logic can be added here
        }

        private void Form1_Paint(Object sender, PaintEventArgs e)
        {
            // Clear the screen
            device.Clear(ClearFlags.Target, Color.CornflowerBlue, 1, 0);

            // Begin rendering the scene
            device.BeginScene();

            // Set the texture and draw the triangle
            device.SetTexture(0, texture);
            device.VertexFormat = CustomVertex.PositionTextured.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertex.Length / 3, vertex);

            // End rendering the scene
            device.EndScene();

            // Present the rendered scene to the display
            device.Present();
        }
    }
}
