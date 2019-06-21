namespace SampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//ImplementationSample example = new ImplementationSample();
			//example.SyncMemberData();

			BasicSample example2 = new BasicSample();
			example2.GetMemberInbox();

			System.Console.WriteLine("Process complete. Press any key to continue.");
			System.Console.ReadLine();
		}
	}
}
