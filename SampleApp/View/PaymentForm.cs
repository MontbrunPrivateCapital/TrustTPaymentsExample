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
            textCardHash.Text = _faker.Random.Hash();
            textPinHash.Text = _faker.Random.Hash();
        }


        private void RequestPayment_Click(object sender, EventArgs e)
        {
            // populate some random hash data there
            var payment = new Payment
            {
                CardHash = textCardHash.Text,
                PinHash = textPinHash.Text
            };

            // close dialog
            Close();

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
