namespace CoffeeMachine.Client
{
	using System;
    using System.Text;
    using System.Windows.Forms;

	public partial class frmMain : Form
	{
		private void btnAddCoffee_Click(object sender, EventArgs e)
		{
			var size = CoffeeOrder.GetCoffeeSize(this.txtSize.Text.ToUpperInvariant());
			var sugars = this.GetValidIntegerValueFromInput(this.txtSugar.Text, ErrorMessageNumberOfSugars);
			var creams = this.GetValidIntegerValueFromInput(this.txtCream.Text, ErrorMessageNumberOfCreams);

			if (this.ValidateCoffeeSize(size) &&
				this.ValidateNumberOfAddOns(sugars, ErrorMessageNumberOfSugars) &&
				this.ValidateNumberOfAddOns(creams, ErrorMessageNumberOfCreams))
			{
				this.CoffeeVendingMachine.AddCoffeeToOder(size, sugars, creams);
				this.UpdateDisplay();
				ClearAddCoffeeBoxes();

			}
		}

		private void btnAddPayment_Click(object sender, EventArgs e)
		{
			double amount = this.GetValidDoubleValueFromInput(txtPayment.Text, ErrorMessageInvalidPaymentAmount);

			if (this.ValidatePayment(amount))
            {
				this.CoffeeVendingMachine.AddPayment(amount);
				this.UpdateDisplay();
				this.ClearAmountBox();
			}
		}

		private void btnVend_Click(object sender, EventArgs e)
		{
			if (this.CoffeeVendingMachine.IsAdequatePaymentProvided())
            {
				StringBuilder message = new StringBuilder();

				message.Append(this.GetCoffeeOrders());
				message.Append($"\r\n");
				message.Append($"Order Total: {this.CoffeeVendingMachine.GetOrderTotal():C}\r\n");
				message.Append($"Customer Amount: {this.CoffeeVendingMachine.GetPaymentTotal():C}\r\n");
				message.Append($"\r\n");
				message.Append($"Change: {this.CoffeeVendingMachine.CalculateChange():C}\r\n");

				MessageBox.Show(message.ToString());

				this.CoffeeVendingMachine.Clear();
				this.Clear();
				this.UpdateDisplay();
            }
			else
            {
				DisplayError(ErrorMessageInadquatePaymentAmount);
            }
		}

		private void UpdateDisplay()
		{
			this.txtCurrentOrder.Text = this.GetCoffeeOrders();
			this.lblOrderTotal.Text = this.CoffeeVendingMachine.GetOrderTotal().ToString("C");
			this.lblCurrentPayment.Text = this.CoffeeVendingMachine.GetPaymentTotal().ToString("C");
        }

		private string GetCoffeeOrders()
        {
			StringBuilder display = new StringBuilder();

			foreach (var order in this.CoffeeVendingMachine.GetCoffeeOrders())
			{
				display.Append($"Coffee Size: {order.Size}, Sugars: {order.NumberOfSugars}, Creams: {order.NumberOfCreams} \r\n");
			}

			return display.ToString();
		}

		private int GetValidIntegerValueFromInput(string input, string errorMessage)
		{
			int value = ErrorResult;

			try
			{
				value = int.Parse(input);
			}
			catch (Exception)
			{
				MessageBox.Show(errorMessage);
			}

			return value;
		}

		private double GetValidDoubleValueFromInput(string input, string errorMessage)
		{
			double value = ErrorResult;

			try
			{
				value = double.Parse(input);
			}
			catch (Exception)
			{
				MessageBox.Show(errorMessage);
			}

			return value;
		}

		private bool ValidateCoffeeSize(CoffeeSize size)
        {
			if (!CoffeeVendingMachine.ValidateCoffeeSize(size))
			{
				this.DisplayError(ErrorMessageCoffeeSize);

				return false;
			}

			return true;
        }

		private bool ValidateNumberOfAddOns(int value, string errorMessage)
		{
			if (value == ErrorResult)
			{
				return false;
			}
			else if (!CoffeeVendingMachine.ValidateAddOnNumber(value))
            {
				DisplayError(errorMessage);
				return false;
            }

			return true;
		}

		private bool ValidatePayment(double amount)
		{
			if (amount <= 0)
            {
				this.DisplayError(ErrorMessageNegativePaymentAmount);
				
				return false;
            }

			if (!this.CoffeeVendingMachine.ValidatePayment(amount))
            {
				this.DisplayError(ErrorMessageWrongIncrementsPaymentAmount);

				return false;
			}

			return true;
		}

		private void DisplayError(string errorMessage)
        {
			MessageBox.Show(errorMessage);
		}

		private void ClearAddCoffeeBoxes()
		{
			txtSize.Clear();
			txtSugar.Clear();
			txtCream.Clear();
		}

		private void ClearAmountBox()
        {
			txtPayment.Clear();
        }

		private void Clear()
        {
			txtCream.Clear();
			txtCurrentOrder.Clear();
			txtPayment.Clear();
			txtSize.Clear();
			txtSugar.Clear();

			lblCurrentPayment.Text = "--";
			lblOrderTotal.Text = "--";
		}

		public frmMain()
		{
			InitializeComponent();
			this.CoffeeVendingMachine = new CoffeeVendingMachine();
		}

		private CoffeeVendingMachine CoffeeVendingMachine;

		private const int ErrorResult = -999;

		private const String ErrorMessageCoffeeSize = "Please enter a valid value for coffee size (S, M, L).";
		private const String ErrorMessageNumberOfSugars = "Please enter a valid value for number of sugars (0 to 3).";
		private const String ErrorMessageNumberOfCreams = "Please enter a valid value for number of creams (0 to 3).";
		private const String ErrorMessageInvalidPaymentAmount = "Please enter a valid payment amount.";
		private const String ErrorMessageNegativePaymentAmount = "Please provide an amount that non negative and greater than zero.";
		private const String ErrorMessageWrongIncrementsPaymentAmount = "Please provide an amount in proper increments (0.05 to 20.00).";
		private const String ErrorMessageInadquatePaymentAmount = "Please provide an adequate payment amount before checking out.";
	}
}
