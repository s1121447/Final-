using System;
using System.Linq;
using System.Windows.Forms;

namespace Final
{
    public partial class CartForm : Form
    {
        public CartForm()
        {
            InitializeComponent();

            // 一開表單就載入清單並更新總金額
            LoadCart();
            UpdateSum();
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            // 如果你想要把 UpdateSum 放在 Load 事件，也可以取消註解
            // UpdateSum();
        }

        /// <summary>
        /// 把目前購物車裡的商品列到 ListView
        /// </summary>
        void LoadCart()
        {
            listView1.Items.Clear();

            foreach (var p in CartData.Items)
            {
                var item = new ListViewItem(new[]
                {
                    p.Name,
                    p.Price.ToString("C")
                });
                listView1.Items.Add(item);
            }
        }

        /// <summary>
        /// 計算並顯示總金額到 Sum 這個 Label
        /// </summary>
        void UpdateSum()
        {
            decimal sum = CartData.Items.Sum(p => p.Price);
            Sum.Text = $"總金額：{sum:C}";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CartData.Items.Clear();
            LoadCart();
            UpdateSum();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            using (var dlg = new CheckoutForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    CartData.Items.Clear();
                    LoadCart();
                    UpdateSum();
                }
            }
        }
    }
}
