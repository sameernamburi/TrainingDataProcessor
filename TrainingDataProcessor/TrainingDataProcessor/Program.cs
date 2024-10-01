using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TrainingDataProcessor.Models;
using System.Collections.Generic;

namespace TrainingDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reading the JSON file
            var json = File.ReadAllText("/Users/sameernamburi/Downloads/trainings.txt");
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(json);

            // Task 1
            GenerateTrainingCount(people);

            // Task 2
            GenerateFiscalYearTrainingList(people, 2024); // Sending fiscal year along with list of people as argument

            // Task 3
            GenerateExpiringTrainingList(people);
        }


        // Task 1: Generate the count of completed trainings
        static void GenerateTrainingCount(List<Person> people)
        {
            // Selecting the most recent completion for each training per person
            var mostRecentCompletions = people
                .SelectMany(p => p.Completions
                                 .GroupBy(c => c.Name) 
                                 .Select(g => g.OrderByDescending(c => c.Timestamp).First())
                                 .Select(c => new { PersonName = p.Name, TrainingName = c.Name }))
                .ToList();

            // Counting how many people completed each training
            var trainingCount = mostRecentCompletions
                .GroupBy(c => c.TrainingName)
                .Select(g => new { Training = g.Key, Count = g.Count() })
                .ToList();

            // Writing result to JSON file titled Task1Output
            File.WriteAllText("Task1Output.json", JsonConvert.SerializeObject(trainingCount, Formatting.Indented));
        }


        // Task 2: Generate the list of trainings completed in given fiscal year
        static void GenerateFiscalYearTrainingList(List<Person> people, int fiscalYear)
        {
            // Calculate the start and end of the fiscal year based on the given year (n)
            DateTime fiscalYearStart = new DateTime(fiscalYear-1, 7, 1);
            DateTime fiscalYearEnd = new DateTime(fiscalYear, 6, 30);

            // Filtering data based on training name and fiscal year and taking only most recent one for each person
            var trainingsInFiscalYear = people
        .SelectMany(p => p.Completions
            .GroupBy(c => c.Name) 
            .Select(g => g.OrderByDescending(c => c.Timestamp).First()) 
            .Select(c => new { PersonName = p.Name, TrainingName = c.Name, c.Timestamp }))                                                                         
        .Where(c => new[] { "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training" }
            .Contains(c.TrainingName) &&
            c.Timestamp >= fiscalYearStart && c.Timestamp <= fiscalYearEnd)
        .ToList();

            // Writing result to JSON file titled Task2Output
            File.WriteAllText("Task2Output.json", JsonConvert.SerializeObject(trainingsInFiscalYear, Formatting.Indented));
        }


        // Task 3: Generate the list of people with expired or soon-expiring trainings
        static void GenerateExpiringTrainingList(List<Person> people)
        {
            // Setting the refernce date to check for expired and soon to be expired trainings 
            DateTime referenceDate = new DateTime(2023, 10, 1);

            // Finding completions that are either expired or expiring soon
            var expiringTrainings = people
                .SelectMany(p => p.Completions, (p, c) => new {
                    p.Name,
                    Training = c.Name,
                    Expired = c.Expires.HasValue && c.Expires.Value.AddDays(1) <= referenceDate,
                    ExpiresSoon = c.Expires.HasValue && c.Expires.Value.AddDays(1) > referenceDate && c.Expires.Value <= referenceDate.AddMonths(1),
                    c.Expires
                })
                .Where(c => c.Expired || c.ExpiresSoon)
                .ToList();

            // Writing result to JSON file titled Task3Output
            File.WriteAllText("Task3Output.json", JsonConvert.SerializeObject(expiringTrainings, Formatting.Indented));
        }
    }
}
