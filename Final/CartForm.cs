using System;
using System.Windows.Forms;

namespace Final
{
    public partial class CartForm : Form
    {
        public CartForm()
        {
            InitializeComponent();
            LoadCart();
        }

        void LoadCart()
        {
            listView1.Items.Clear();
            foreach (var p in CartData.Items)
                listView1.Items.Add(new ListViewItem(new[] { p.Name, p.Price.ToString("C") }));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CartData.Items.Clear();
            LoadCart();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            using (var dlg = new CheckoutForm())
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    CartData.Items.Clear();
                    LoadCart();
                }
        }

        private void CartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
