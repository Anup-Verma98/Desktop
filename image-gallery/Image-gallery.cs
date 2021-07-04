using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Tile;
namespace image_gallery
{
    public partial class Form1 : Form
    {
        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            
                Rectangle r = _searchBox.Bounds;
                r.Inflate(3, 3);
                Pen p = new Pen(Color.LightGray);
                e.Graphics.DrawRectangle(p, r);
            
        }

        private void _searchBox_TextChanged(object sender, EventArgs e)
        {
        }

        private async void _search_Click(object sender, EventArgs e)
        {
            
                statusStrip1.Visible = true;
                imagesList = await datafetch.GetImageData(_searchBox.Text);
                AddTiles(imagesList);
                statusStrip1.Visible = false;
           
        }

        private void _exportImage_Click(object sender, EventArgs e)
        {
            
                List<Image> images = new List<Image>();
                foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
                {
                    if (tile.Checked)
                    {
                        images.Add(tile.Image);
                    }
                }
                ConvertToPdf(images);
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "pdf";
                saveFile.Filter = "PDF files (*.pdf)|*.pdf*";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    imagePdfDocument.Save(saveFile.FileName);
                }
            
        }
        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }

        }
        private void OnExportImagePaint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(_exportImage.Location.X, _exportImage.Location.Y, _exportImage.Width, _exportImage.Height)
r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(this.Width, 43));
        }
        private void OnTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }
    }
}
