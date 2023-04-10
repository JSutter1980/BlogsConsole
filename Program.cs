using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");



try
{ 
    var db = new BloggingContext();
    string choice;
    do
    {
        Console.WriteLine("What would you like to do?:\n1. Display All Blogs\n2. Add a Blog\n3. Create A Post\n4. Display Posts");
        choice = Console.ReadLine();

        if (choice == "1")
        {
            // Display all Blogs from the database
            var query = db.Blogs.OrderBy(b => b.BlogId);

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.BlogId}.){item.Name}");
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

            Console.WriteLine("Please select number of Blog you would like to add to:");

            var query = db.Blogs.OrderBy(b => b.BlogId);
            foreach (var item in query)
            {
                Console.WriteLine($"{item.BlogId}.){item.Name}");
            }
            
            var post = new Post();
            post.BlogId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter your post title:");
            post.Title = Console.ReadLine();    

            Console.WriteLine("Enter your post:");
            post.Content = Console.ReadLine();

            db.AddPost(post);
            logger.Info("Post added - {name}", post.Title);
        }

        else if (choice == "4")

        {

            Console.WriteLine("Which Blog would you like to view the posts from?:\n");

            var query = db.Blogs.OrderBy(b => b.BlogId);

            foreach (var item in query)
            {
                Console.WriteLine($"{item.BlogId}.) {item.Name}");
            }

            var blogChoice = Convert.ToInt32(Console.ReadLine());

            var postQuery = db.Posts.Where(p => p.BlogId == blogChoice).OrderBy(p => p.Title);

            foreach ( var postItem in postQuery)
            {
                Console.WriteLine($" Blog: {postItem.Blog.Name}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"#{postItem.BlogId} {postItem.Title}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{postItem.Content}\n");
                Console.ForegroundColor = ConsoleColor.White;
                
            }

        }

    }while (choice == "1" || choice == "2" || choice == "3" || choice == "4");

}
catch (Exception ex)
{
    logger.Error(ex.Message);
}
// }while (choice == "1" || choice == "2" || choice == "3" || choice == "4");

logger.Info("Program ended");

