using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Final
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm() => InitializeComponent();

        private void btnSend_Click(object sender, EventArgs e)
        {
            string to = txtEmail.Text.Trim();
            var body = string.Join("\r\n", CartData.Items.Select(p => $"{p.Name}: {p.Price:C}"));
            var mail = new MailMessage("kevin1015.lu@gmail.com", to) { Subject = "您的收據", Body = body };
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
