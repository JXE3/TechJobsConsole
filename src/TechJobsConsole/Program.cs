using System;

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;



namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");

            actionChoices.Add("list", "List");

            actionChoices.Add("end", "End");


            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();

            
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            int columnChoicesCount = columnChoices.Count;

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            bool continueExecution = true;

            // Allow user to search/list until they manually quit with ctrl+c
            while (continueExecution)
            {
                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("end"))
                {
                    continueExecution = false;
                    break;
                }


                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                   
                    string columnChoice = GetUserSelection("Search", columnChoices);

                  
                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;
                



                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        // Console.WriteLine("Search all fields not yet implemented.");

                    /*
                        List<string> columnChoicesKeysList = new List<string>();
                        foreach (string columnChoicesKey in columnChoices.Keys)
                        {
                            Console.WriteLine(columnChoicesKey);
                            Console.ReadLine();
                            columnChoicesKeysList.Add(columnChoicesKey);
                        }
                    */

                        searchResults = JobData.FindByKey(columnChoices, searchTerm);
                        PrintJobs(searchResults);

                    }
                    else
                    {
                        

                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            List<string> jobsHdg = new List<string>();
            List<string> jobsDtl = new List<string>();

            foreach (Dictionary<string, string> dict1 in someJobs)
            {


                foreach (string key1 in dict1.Keys)
                {
                    jobsHdg.Add(key1);
                }

                foreach (string value1 in dict1.Values)
                {
                    jobsDtl.Add(value1);
                }

            }


                
            

            for (int i=0; i < jobsHdg.Count; i++) 
            {
                if (i % 5 == 0) 
                {
                    Console.WriteLine("************************************************************");
                    Console.WriteLine(" ");
                }
                Console.WriteLine(jobsHdg[i] + ": " + jobsDtl[i]);
                
              
            }
                
            
            Console.ReadLine();

            //    Console.WriteLine(key1);
            //    Console.ReadLine();
            // Console.WriteLine("printJobs is not implemented yet");
        }


        
        
    }
}
