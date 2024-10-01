Training Data Processor

Overview

This is a .NET console application designed to process training data from a JSON file. It performs three tasks based on the given requirements:
1. List each completed training with a count of how many people have completed that training.
2. List all people who completed specified trainings in a given fiscal year.
3. Find all people whose trainings have either expired or are about to expire soon, based on a given date.
The input data is provided in JSON format, and the output for each task is also generated in JSON format.

Tasks

1. Task 1: Count of Completed Trainings
•	The application reads the list and counts how many people have completed each training. If a person completed the same training multiple times, only the most recent completion is considered.
•	The result is written to Task1Output.json.

2. Task 2: List of Trainings Completed in a Given Fiscal Year
•	The application filters the list of completions for specific trainings within a defined fiscal year (July 1 of the previous year to June 30 of the given year). Only the most recent completion for each person is considered.
•	The result is written to Task2Output.json.

3. Task 3: Expired or Soon-to-Expire Trainings
•	The application checks which training completions are either expired or will expire within one month of a given reference date. The date is checked from the day after its expiration date.
•	The result is written to Task3Output.json.


Input File: 
The application expects an input file named trainings.txt in JSON format. This file should contain a list of people and their respective training completions and expiration dates.

Output files:
The outputs are stored in a folder titled Outputs.

•	Task1Output.json: Contains the count of how many people completed each training.

•	Task2Output.json: Lists the people who completed specific trainings within the given fiscal year.

•	Task3Output.json: Lists the people whose trainings have expired or will expire soon.

Note: The application follows the specified conditions for processing the training data. For Task 1 and Task 2, when a person has completed the same training multiple times, only their most recent completion is taken into account. For Task 3, a training is considered expired starting the day after its expiration date, ensuring accuracy in determining expired or soon-to-expire trainings.
