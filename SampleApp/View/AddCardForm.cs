using System;
using System.Windows.Forms;
using Bogus;
using Bogus.DataSets;
using TrustTPaymentSDK;
using TrustTPaymentSDK.Models;

namespace SampleApp.View
{
    public partial class AddCardForm : Form
    {
        private readonly Faker _faker;
        private readonly TrusttAPI _api;

        public AddCardForm(Faker faker, TrusttAPI api)
        {
            InitializeComponent();
            _faker = faker;
            _api = api;

            textCardNumber.Text = faker.Finance.CreditCardNumber(CardType.Mastercard).Replace("-", string.Empty);
            textMonth.Text = "09";
            textYear.Text = DateTime.Now.Year.ToString();
            textCVV.Text = faker.Finance.CreditCardCvv();
        }


        private void btnAddCard_Click(object sender, EventArgs e)
        {

            Close();

            var card = new Card
            {
                CVV = textCVV.Text,
                ExpirationMonth = textMonth.Text,
                ExpirationYear = textYear.Text,
                Number = textCardNumber.Text
            };

            try
            {
                var add = _api.CardAttach(card);

                MessageBox.Show(
                    $"Card Hash Data: {add.CardHash}",
                    "card succesfully add",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "This not looks like a valid card.", "invalid card data",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } // envent


    } // class
}
