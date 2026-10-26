namespace SMTPing;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (!File.Exists("app.ico") || args.Contains("--gen-icon"))
        {
            IconBuilder.GenerateAppIcon("app.ico");
            if (args.Contains("--gen-icon")) return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}