using System; //no explanation needed. essential stuff there
using System.IO; //nice file IO methods that I can use
using System.Net; //get request
using System.Text.RegularExpressions; //extracting substrings

//p5template 1.2
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
        static string version = "0.8.0"; //latest version as of May 2019
        //future update? A: yes
        static string custompath;

        static void Main(string[] args)
        {
            //standard colors that don't look gay
            Console.Title = "p5 Template | Created by Greenman";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //start();
            versioncheck();
        }

        static void versioncheck() //used to update the version number
        {
            WebClient client = new WebClient();
            string x = "";
            try
            {
                x = client.DownloadString("https://api.cdnjs.com/libraries/p5.js?fields=version");
            }
            catch (Exception e)
            {
                if (e.GetType().Name == "WebException")
                {
                    Console.WriteLine("A network error occured. Please check your internet connection and try again.");

                    Console.WriteLine("\nPress any key to exit");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            client.Dispose(); //no memleak
            var result = Regex.Match(x, @"\d.\d+.\d+"); //pattern for version numbers

            version = result.Groups[0].Value;

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
            Console.WriteLine("2) No (choose directory)");

            //OLD user input code
            //char op1 = Console.ReadKey().KeyChar; 
            /*if (op1 == '2')
                error();
            setsize();*/

            uninput(2, new Action[]{setsize, customdir}); //user input that handles exceptions and semantic errors :)
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
            uninput(2, new Action[] { delegate () { slib = true; }, delegate () { slib = false; } }); //I still don't want to make extra functions
            writeT(); //not a mistake. the above choice does not redirect
        }

        static void writeT() //time to write the files
        {
            Console.Clear();
            Console.WriteLine("Creating files...");

            //these strings look like a mess but it works so I don't care
            String htmlfile;
            htmlfile = $"<html>\n    <head>\n        <script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/{version}/p5.min.js\"></script>\n"; 
            if (dlib)
            {
                htmlfile += $"        <script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/{version}/addons/p5.dom.min.js\"></script>\n"; 
            }
            if (slib)
            {
                htmlfile += $"        <script src=\"https://cdnjs.cloudflare.com/ajax/libs/p5.js/{version}/addons/p5.sound.min.js\"></script>\n";
            }
            htmlfile += "        <script src=\"sketch.js\"></script>\n    </head>\n</html>";
            try
            {
                File.WriteAllText(custompath + "index.html", htmlfile);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while creating the file.");
                Console.WriteLine("Possible causes:");
                Console.WriteLine("- The target directory was deleted");
                Console.WriteLine("- You don't have write access in the specified directory");
                Console.WriteLine("- A file with the name 'index.html' or 'sketch.js' exists and is currently opened");
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Finished HTML file!");

            String sketchfile;
            sketchfile = $"function setup(){{\n    createCanvas({w},{h});\n}}\n\nfunction draw(){{\n    background(0);\n}}"; //template literal strings are nice
            try
            {
                File.WriteAllText(custompath + "sketch.js", sketchfile);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while creating the file.");
                Console.WriteLine("Possible causes:");
                Console.WriteLine("- The target directory was deleted");
                Console.WriteLine("- You don't have write access in the specified directory");
                Console.WriteLine("- A file with the name 'index.html' or 'sketch.js' exists and is currently opened");
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.WriteLine("Finished JS file!");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        //uninput means user number input
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
                    catch
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

        static void customdir() //this will probably be removed once I add directory support
        {
            bool relativep = false;
            Console.Clear();
            Console.WriteLine("Is the directory you want to use a relative path or an absolute path?");
            Console.WriteLine("1) Absolute");
            Console.WriteLine("2) Relative");
            uninput(2, new Action[] { delegate { relativep = false; }, delegate { relativep = true;  } });
            Console.Clear();
            Console.WriteLine("Please type the path you want to place the files into.");
            if (relativep)
                Console.Write("Relative Path: ");
            if (!relativep)
                Console.Write("Absolute Path: ");
            string tempcpath = Console.ReadLine();

            if (tempcpath.Contains("/"))
            {
                tempcpath = tempcpath.Replace("/", "\\");
            }

            if (relativep)
            {
                tempcpath = Environment.CurrentDirectory + "\\" + tempcpath + "\\";
                tempcpath = tempcpath.Replace("\\\\", "\\");
                if (Directory.Exists(tempcpath))
                {
                    custompath = tempcpath;
                    Console.WriteLine(custompath);
                    Console.ReadKey();
                } else
                {
                    Console.WriteLine("Directory does not exist!");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    customdir();
                }
            } else
            {
                tempcpath = tempcpath.Replace("\\\\", "\\");
                if (Directory.Exists(tempcpath))
                {
                    custompath = tempcpath + "\\";
                    Console.WriteLine(custompath);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Directory does not exist!");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    customdir();
                }
            }
            setsize();
        }
    }
}
