using System;
using System.Windows.Forms;
using Bogus;
using TrustTPaymentSDK;
using TrustTPaymentSDK.Models;

namespace SampleApp.View
{
    public partial class PaymentForm : Form
    {
        private readonly Faker _faker;
        private readonly TrusttAPI _api;

        public PaymentForm(Faker faker, TrusttAPI api)
        {
            InitializeComponent();
            _faker = faker;
            _api = api;
        }


        private void RequestPayment_Click(object sender, EventArgs e)
        {
            // close dialog
            Close();

            if (numericAmount.Value < 1)
            {
                MessageBox.Show(
                    "Please, specify a valid money amount.",
                    "invalid amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // populate some random hash data there
            var payment = new Payment
            {
                Payload = textPayload.Text,
                Currency = "USD",
                Amount = Convert.ToInt64(numericAmount.Value * 100)
            };


            try
            {
                var transaction = _api.Payment(payment);

                MessageBox.Show(
                    $"Transcation complete, ID ${transaction.TransactionId}",
                    "Operation Successfully",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var yesno =
                    MessageBox.Show(
                        "Not such card! Does you want to add it now?",
                        ex.Message,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (yesno == DialogResult.Yes)
                    new AddCardForm(_faker,_api).ShowDialog();

            }

        } // event

    } // class
}
