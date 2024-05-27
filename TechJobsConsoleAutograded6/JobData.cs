using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Markup;

namespace TechJobsConsoleAutograded6
{
    public class JobData
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates.
         */
        public static List<string> FindAll(string column)
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            return values;
        }

        /**
         * Search all columns for the given term
         */

        //TODO: Complete the FindByValue method
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> JobsReturnedByValue =
                new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                foreach (string aValue in job.Values)
                {
                    //   Console.WriteLine(job);
                    if (aValue.Contains(value))
                        
                    //this line ensures no duplicates . nope it does not ensure that
                    {
                        if (!JobsReturnedByValue.Contains(job))
                            
                        {
                            JobsReturnedByValue.Add(job);
                        }
                    }

                    // Console.WriteLine(); //idk if i need PrintJobs called here again


                    ///  Console.WriteLine(aValue.Key + ": " + aValue.Value);
                    // Console.WriteLine(JobsReturnedByValue);

                    // }
                    //also make sure no duplicates using : if (!values.Contains(aValue) .... values.Add(aVaule) - see lines 32-34
                }
                // if (job.ContainsValue(aValue))
                // {

                //     //create a loop that runs through and breaks out when result is found - NO, I DONT" THINK BREAK IS NECESSARY
            }

            return JobsReturnedByValue;

            //now that new list of dictionaries has been created, create a conditional that ensures no duplicates..
            //do so by creating a loop with a break???? return all jobs and check for equality?
            // return null; //this will no longer return null . return jobs that match the search criteria
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(
            string column,
            string value
        )
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> row in AllJobs)
            {
                string aValue = row[column];

                //TODO: Make search case-insensitive
                if (aValue.Contains(value))
                {
                    jobs.Add(row);
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {
            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(
            string row,
            char fieldSeparator = ',',
            char stringSeparator = '\"'
        )
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
