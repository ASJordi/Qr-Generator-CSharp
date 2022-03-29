using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;


namespace Qr_Generator_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            if (txtData.Text != string.Empty)
            {
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode qrCode = new QrCode();
                qrEncoder.TryEncode(txtData.Text, out qrCode);

                GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

                MemoryStream ms = new MemoryStream();
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                var imageTemporal = new Bitmap(ms);

                var imagen = new Bitmap(imageTemporal, new Size(new Point(200, 200)));
                panelImage.BackgroundImage = imagen;

                // save image in ./
                imagen.Save("image.png", ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("No data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Image imageFinal = (Image)panelImage.BackgroundImage.Clone();

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.AddExtension = true;
            saveFile.Filter = "Image PNG (*.png)|*.png";
            saveFile.ShowDialog();
            if (!string.IsNullOrEmpty(saveFile.FileName))
            {
                imageFinal.Save(saveFile.FileName, ImageFormat.Png);
            }

            imageFinal.Dispose();
        }
    }
}
