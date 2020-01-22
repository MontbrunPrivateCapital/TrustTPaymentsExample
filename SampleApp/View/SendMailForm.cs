using System;
using System.Windows.Forms;
using Bogus;
using TrustTPaymentSDK;
using TrustTPaymentSDK.Models;

namespace SampleApp.View
{
    public partial class SendMailForm : Form
    {
        private readonly Faker _faker;
        private readonly TrusttAPI _api;

        public SendMailForm(Faker faker, TrusttAPI api)
        {
            InitializeComponent();
            _faker = faker;
            _api = api;

            textEmail.Text = _faker.Internet.Email(provider: "gmail.com").ToLowerInvariant();
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            Close();

            var mailv = new Verification { Email = textEmail.Text };

            try
            {
                _api.SendVerification(mailv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "There is not user with such email.", "mail sending error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show(
                "Verification email sent!", "email sent",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        } // event

    } // class
}
