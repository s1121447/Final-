using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Final
{

    public partial class Form1 : Form
    {
        // 控制項陣列
        private PictureBox[] productBoxes;
        private Dictionary<PictureBox, Product> mapping;

        public Form1()
        {
            InitializeComponent();
            productBoxes = new[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };
            LoadProducts();
            // 購物車圖示和數量
            pictureBox5.Image = Properties.Resources.shopcar;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Click += Cart_Click;
            UpdateCartLabel();
        }

        private void LoadProducts()
        {
            var products = new List<Product>
            {
                new Product { Name = "純白T-shirt", Description = "100% 純棉，非常舒適", Price = 399, Image = Properties.Resources.WhiteTshirt },
                new Product { Name = "黑色上衣", Description = "黑色純棉 T 恤", Price = 449, Image = Properties.Resources.blackTshirt },
                new Product { Name = "布料圍巾", Description = "柔軟圍巾", Price = 299, Image = Properties.Resources.blackcloth },
                new Product { Name = "韓系短版上衣", Description = "時尚短版女裝上衣", Price = 499, Image = Properties.Resources.girl }
            };
            mapping = new Dictionary<PictureBox, Product>();
            for (int i = 0; i < 4; i++)
            {
                var pb = productBoxes[i];
                var p = products[i];
                mapping[pb] = p;
                pb.Image = p.Image;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Cursor = Cursors.Hand;
                pb.Click += Product_Click;
            }
        }

        private void UpdateCartLabel()
        {
            Number.Text = CartData.Items.Count.ToString();
        }

        private void Product_Click(object sender, EventArgs e)
        {
            ShowProductDetail(mapping[(PictureBox)sender]);
        }

        private void ShowProductDetail(Product p)
        {
            using (var dlg = new Form())
            {
                dlg.Text = p.Name;
                dlg.Size = new Size(300, 350);
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;

                var pic = new PictureBox
                {
                    Image = p.Image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Top,
                    Height = 180
                };
                dlg.Controls.Add(pic);

                var lbl = new Label
                {
                    Text = p.Description + "\n" + p.Price.ToString("C"),
                    Dock = DockStyle.Top,
                    Height = 60,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                dlg.Controls.Add(lbl);

                var btnCancel = new Button
                {
                    Text = "放棄",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(80, 30),
                    Location = new Point(20, dlg.ClientSize.Height - 50)
                };
                dlg.Controls.Add(btnCancel);

                var btnAdd = new Button
                {
                    Text = "加入購物車",
                    Size = new Size(100, 30),
                    Location = new Point(dlg.ClientSize.Width - 120, dlg.ClientSize.Height - 50)
                };
                btnAdd.Click += (_, __) =>
                {
                    CartData.Items.Add(p);
                    UpdateCartLabel();
                    dlg.Close();
                };
                dlg.Controls.Add(btnAdd);

                dlg.AcceptButton = btnAdd;
                dlg.CancelButton = btnCancel;
                dlg.ShowDialog();
            }
        }

        private void Cart_Click(object sender, EventArgs e)
        {
            var cf = new CartForm();
            cf.ShowDialog();
            UpdateCartLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
