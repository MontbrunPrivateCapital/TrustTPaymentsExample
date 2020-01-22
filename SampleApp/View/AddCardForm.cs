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

            var card = new CardInfo
            {
                Email = textBoxEmail.Text,
                Payload = textCardNumber.Text
            };

            try
            {
                var add = _api.CardAttach(card);

                MessageBox.Show(
                    $"Current card ID is: {add.CardId}",
                    "card succesfully add",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message, "error adding card",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } // envent


    } // class
}
