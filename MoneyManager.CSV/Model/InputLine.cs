namespace MoneyManager.Integrations.CSV.Model
{
	public class InputLine
	{
		public int LineNumber { get; private set; }
		public string Text { get; private set; }

		public InputLine(int lineNumber, string text)
		{
			LineNumber = lineNumber;
			Text = text;
		}
	}
}
