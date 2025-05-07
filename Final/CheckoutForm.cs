using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Final
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm() => InitializeComponent();

        private void btnSend_Click(object sender, EventArgs e)
        {
            string to = txtEmail.Text.Trim();
            string name = txtname.Text.Trim();    // ← 你的新 TextBox
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("請先輸入買家姓名和 Email");
                return;
            }

            decimal total = CartData.Items.Sum(p => p.Price);
            var bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine($"{name:C}您好，以下是您在育成衣館的訂單，預計於3天後送達");
            foreach (var p in CartData.Items)
                bodyBuilder.AppendLine($"{p.Name}: {p.Price:C}");
            bodyBuilder.AppendLine("---------------------");
            bodyBuilder.AppendLine($"            Total: {total:C}");

            // 3. 準備 CSV 資料夾與檔案路徑
            //    專案執行檔同層會自動建立 FinalOrders 資料夾
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folder = Path.Combine(baseDir, "FinalOrders");
            Directory.CreateDirectory(folder);

            string safeName = string.Concat(
              name.Select(c => char.IsLetterOrDigit(c) ? c : '_')
          );
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = $"{safeName}_{timestamp}.csv";
            string csvFile = Path.Combine(folder, fileName);

            // 4. 組出要寫入 CSV 的文字
            //    欄位：Email,OrderTime,ProductName,Price,Total
            var sb = new StringBuilder();
            foreach (var p in CartData.Items)
            {
                sb.AppendLine(
                    $"{to}," +
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}," +
                    $"{p.Name}," +
                    $"{p.Price}" 
                );
            }
            sb.Append("-----------------------");
            sb.AppendLine($"總共：{total}");

            // 5. 寫入（若不存在自動建立檔案，若存在就追加）
            File.AppendAllText(csvFile, sb.ToString(), Encoding.UTF8);
            var Body = bodyBuilder.ToString();
            var mail = new MailMessage("kevin1015.lu@gmail.com", to) { Subject = "您的收據", Body = Body };
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("kevin1015.lu@gmail.com", "dmlv rbhp fxmt mrok"),
                EnableSsl = true
            };
            client.Send(mail);
            MessageBox.Show("已寄出！");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
