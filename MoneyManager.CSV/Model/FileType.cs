using MoneyManager.Integrations.CSV.Reader;

namespace MoneyManager.Integrations.CSV.Model
{
	public class FileType
	{
		public static FileType MbankCSV = new FileType("Mbank", new ReaderSettings(';') { DetectFirstLine = text => text.StartsWith("#Data operacji") }, new MbankReader());
		public static FileType PkoCSV = new FileType("PKO BP", new ReaderSettings(',') { FirstLine = 2 } , new PkoReader());

		public string Description { get; private set; }
		public ReaderSettings Settings { get; private set; }
		public IReaderImplmentation ReaderImplementation { get; private set; }

		private FileType(string description, ReaderSettings settings, IReaderImplmentation readerImplementation)
		{
			Description = description;
			Settings = settings;
			ReaderImplementation = readerImplementation;
		}
	}
}
