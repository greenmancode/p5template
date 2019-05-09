using System; //no explanation needed. essential stuff there
using System.IO; //nice file IO methods that I can use

//p5template
//Created by Greenman 2019

namespace p5template
{
    class Program
    {
        //variables acting as config for the final template
        static int w;
        static int h;
        static bool dlib;
        static bool slib;
        //future update?
        //static string outputpath;

        static void Main(string[] args)
        {
            //standard colors that don't look gay
            Console.Title = "p5 Template | Created by Greenman";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            start();
        }

        static void start()
        {
            Console.Clear();
            Console.WriteLine("p5 Template - Created By Greenman");
            Console.WriteLine("------------");
            Console.WriteLine(Environment.CurrentDirectory); //displays current directory without using cmd :)
            Console.WriteLine("Is this the directory you want to create your sketch in?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");

            //OLD user input code
            //char op1 = Console.ReadKey().KeyChar; 
            /*if (op1 == '2')
                error();
            setsize();*/

            uninput(2, new Action[]{setsize, error}); //user input that handles exceptions and semantic errors :)
        }

        static void setsize()
        {
            Console.Clear();
            Console.WriteLine("Canvas Width (Pixels)");
            Console.Write("Width: ");
            w = Convert.ToInt32(Console.ReadLine()); //I've never seen anyone make the canvas size a floating point number
            Console.WriteLine("Canvas Height (Pixels)");
            Console.Write("Height: ");
            h = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Width: " + w + " Height: " + h);
            Console.WriteLine("Is this size okay?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            uninput(2, new Action[] { setlibs, setsize });
        }

        static void setlibs()
        {
            Console.Clear();
            Console.WriteLine("Do you want to include the p5 DOM library?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            //OLD user input code
            /*
            char op3 = Console.ReadKey().KeyChar;
            if (op3 == '1')
            {
                dlib = true;
            } else if (op3 == '2')
            {
                dlib = false;
            }
            */

            //mini c# lesson: delegate creates anonymous function
            uninput(2, new Action[] { delegate () { dlib = true; }, delegate () { dlib = false; } }); //I don't want to make extra functions

            Console.Clear();
            Console.WriteLine("Do you want to include the p5 Sound library?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            char op4 = Console.ReadKey().KeyChar;
            uninput(2, new Action[] { delegate () { slib = true; }, delegate () { slib = false; } }); //I still don't want to make extra functions
            writeT(); //not a mistake. the above choice does not redirect
        }

        static void writeT() //time to write the files
        {
            Console.Clear();
            Console.WriteLine("Creating files...");

            //these strings look like a mess but it works so I don't care
            String htmlfile;
            htmlfile = "<html>\n    <head>\n"; 
            if (dlib)
            {
                htmlfile += "        <script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.8.0/p5.js\"></script>\n"; 
            }
            if (slib)
            {
                htmlfile += "        <script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.8.0/addons/p5.sound.js\"></script>\n";
            }
            htmlfile += "        <script src=\"sketch.js\"></script>\n    </head>\n</html>";
            File.WriteAllText("index.html",htmlfile);

            Console.WriteLine("Finished HTML file!");

            String sketchfile;
            sketchfile = $"function setup(){{\n    createCanvas({w},{h});\n}}\n\nfunction draw(){{\n    background(0);\n}}"; //template literal strings are nice
            File.WriteAllText("sketch.js", sketchfile);

            Console.WriteLine("Finished JS file!");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void uninput(int opts, Action[] callbacks) //used to handle user input without throwing exceptions and deals with semantic errors
        {
            //check to match options to callbacks
            if (callbacks.Length == opts)
            {
                bool flag = false;//looping flag
                while (flag == false)
                {
                    int i; //global so we can use after exception handling
                    try
                    {
                         i = Convert.ToInt32(Convert.ToString(Console.ReadKey().KeyChar)); //char -> string -> int because char -> int results in ASCII stuff
                    }
                    catch (FormatException e)
                    {
                        continue; //skip next check because it requires an int and a string will break it
                    }
                    
                    if (i <= callbacks.Length)
                    {
                        flag = true; //break the loop
                        callbacks[i - 1](); //run the callback (-1 because arrays start at idx 0)
                    }
                }
            }
        }

        static void error() //this will probably be removed once I add directory support
        {
            Console.Clear();
            Console.WriteLine("Place this program into the directory you want your files to be created in.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
