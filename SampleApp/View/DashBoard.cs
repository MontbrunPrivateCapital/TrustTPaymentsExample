using System;
using System.Windows.Forms;
using Bogus;
using SampleApp.View;
using TrustTPaymentSDK;

namespace SampleApp
{
    public partial class DashBoard : Form
    {
        private readonly Faker _faker;
        private readonly TrusttAPI _api;

        public DashBoard()
        {
            InitializeComponent();
            _faker = new Faker();
            _api = new TrusttAPI(new TrustTSettings { Host = "trustt-payments-api-test.azurewebsites.net" });

            var fees = _api.Fees();

            labelFees.Text = $"Current gold price {fees.GoldPrice}, trustt fees {fees.TrusttFee}";

        }


        private void btnPayment_Click(object sender, EventArgs e) =>
            new PaymentForm(_faker, _api).ShowDialog();

        private void btnAddCard_Click(object sender, EventArgs e) =>
            new AddCardForm(_faker, _api).ShowDialog();

        private void btnMail_Click(object sender, EventArgs e) =>
            new SendMailForm(_faker, _api).ShowDialog();

    } // class
}
