using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

try
{
    Console.WriteLine("What would you like to do?:\n1. Display All Blogs\n2. Add a Blog\n3. Create A Post\n4. Display Posts\n");
    var choice = Console.ReadLine();

    var db = new BloggingContext();

    if (choice == "1")
    {
        // Display all Blogs from the database
        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine("All blogs in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }

    }

    else if (choice == "2")
    {

        // Create and save a new Blog
        Console.Write("Enter a name for a new Blog: ");
        var name = Console.ReadLine();

        var blog = new Blog { Name = name };

        db.AddBlog(blog);
        logger.Info("Blog added - {name}", name);

    }

    else if (choice == "3")
    {

        Console.WriteLine("Please select which Blog you would like to add to");
        var blogChoice = Console.ReadLine();

    }

    else if (choice == "4")
    {

        Console.WriteLine("Which post would you like to view?");
        var postChoice = Console.ReadLine();


    }

}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");

