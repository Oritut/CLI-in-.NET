using System;
using System.CommandLine;
using System.IO;
using System.Linq;

namespace fib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var bundleCommand = new Command("bundle", "Bundle code files to a single file");
            bundleCommand.AddAlias("bun");

            var bundleOption = new Option<string>("--output", "File path and name");
            bundleCommand.AddOption(bundleOption);
            bundleOption.AddAlias("-o");

            var languageOption = new Option<string>("--language", "File type to bundle") { IsRequired=true};
            bundleCommand.AddOption(languageOption);
            languageOption.AddAlias("-l");

            var noteOption = new Option<bool>("--note", "File path and name");
            bundleCommand.AddOption(noteOption);
            noteOption.AddAlias("-n");

            var sortOption = new Option<bool>("--sort", "File pash and name");
            bundleCommand.AddOption(sortOption);
            sortOption.AddAlias("-s");

            var removeOption = new Option<bool>("--remove", "File pash and name");
            bundleCommand.AddOption(removeOption);
            removeOption.AddAlias("-r");

            var authorOption = new Option<string>("--author", "File pash and name");
            bundleCommand.AddOption(authorOption);
            authorOption.AddAlias("-a");

            bundleCommand.SetHandler((output, remove, sort, language, author, note) =>
            {
             
                    string[] extanLang = { "java", "sql", "c", "html" };
                  
                    if (language.ToString().ToLower()== "all")
                    {
                        List<string> listAll = new List<string>();
                    string extenFile;
                        foreach (string file in Directory.GetFiles(".", "*.*", SearchOption.AllDirectories))
                        {
                            if (output == null)
                            {
                                Console.WriteLine("you didnt put a name of file");
                                Environment.Exit(1);
                            }
                         extenFile = Path.GetExtension(file).ToLower();
                            if (extanLang.Contains(extenFile))
                            {
                                listAll.Add(file);
                            }

                        }
                        if (listAll.Count == 0)
                        {
                            Console.WriteLine("no files");
                            return;
                        }
                    string outputFile = output + ".txt";
                    using (StreamWriter writer = new StreamWriter(outputFile))
                    {
                        foreach(string file in listAll)
                        {
                            writer.WriteLine(File.ReadAllText(file));
                        }
                    }

               }
        
            }, bundleOption, languageOption, sortOption, noteOption, authorOption, noteOption);

            var rspCommand = new Command("create-rsp", "Create response file");
            rspCommand.AddAlias("rspC");

            var rootCommand = new RootCommand("Root command for file Bundler CLI");
            rootCommand.AddCommand(bundleCommand);

            rootCommand.InvokeAsync(args).Wait();
        }
    }
}

