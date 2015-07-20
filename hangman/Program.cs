using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class Program
    {
        static void Main(string[] args)
        { 

            Console.Title = "Hangman ver 0.9 - Athanasios Emmanouilidis";

            String[] arrayOfSecretWords = File.ReadAllLines("listOfWords.txt");
            int lives = 3;

            while (lives > 0) {
                gameInit(arrayOfSecretWords, lives);
            }

        }

        static string ReplaceAll(string input, char target)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(target);
            }

            return sb.ToString();
        }

        static void printGuessedWordWithTabs(String word)
        {
            for (int character = 0; character <= word.Length - 1; character++)
            {
                Console.Write(word[character] + " ");
            }
        }

        static void printInvalidGuesses(List<Char> list)
        {

            foreach (Char x in list)
            {
                Console.Write(x + ", ");
            }

        }


        static void gameInit(String[] arrayOfSecretWords, int lives) 
        {
            Console.Beep(1400, 100); Console.Beep(1500, 100); Console.Beep(1600, 100); Console.Beep(1400, 100); Console.Beep(1500, 100); Console.Beep(1600, 100);

            Boolean win = false;
            List<Char> listOfValidGuesses = new List<Char>();
            List<Char> listOfInvalidGuesses = new List<Char>();
            // Choose randomly a secret word from the array
            String randomSecretWord = arrayOfSecretWords[new Random().Next(0, arrayOfSecretWords.Length)];
            String wordGuessedTillNow = ReplaceAll(randomSecretWord, '_');
            int totalTries = randomSecretWord.Length * 2;

            Console.WriteLine("\n-----------------------------------------------\n");
            Console.WriteLine("Welcome to Hangman ver 0.9.\n");
            Console.WriteLine("Copyright 2015 - Athanasios Emmanouilidis\n");
            Console.WriteLine("http://athanasiosem.github.io\n");
            Console.WriteLine("Please send bug reports and comments to\nathanasiosem@inbox.com\n");
            Console.WriteLine("-----------------------------------------------\n");
            Console.WriteLine("You have " + lives + " lives.\n");
            Console.WriteLine("Secret word is: ");

            while (win == false && (totalTries != 0) && (lives >= 0))
            {

                printGuessedWordWithTabs(wordGuessedTillNow);

                Console.WriteLine("\n\nInvalid guesses:"); printInvalidGuesses(listOfInvalidGuesses);

                Console.WriteLine("\n\nPlease enter a letter:\n"); 
                Char inputString = Console.ReadKey().KeyChar;

                Console.WriteLine("\nYou wrote '" + inputString + "'.\n");

                if (listOfInvalidGuesses.Contains(inputString))
                {
                    Console.WriteLine("You already entered this letter and its not correct !\n");
                    Console.Beep(500,500);

                }
                else if (listOfValidGuesses.Contains(inputString))
                {
                    Console.WriteLine("You already entered this letter and its correct !\n");
                    Console.Beep(1000, 500);

                }

                if (randomSecretWord.Contains(inputString))
                {
                    Console.WriteLine("'" + inputString + "' exists in the word!\n");
                    Console.Beep(1000, 500);
                    Console.WriteLine("\nTries left: " + totalTries);
                    listOfValidGuesses.Add(inputString);

                    for (var i = 0; i < randomSecretWord.Length; i++)
                    {
                        if (inputString == randomSecretWord[i])
                        {
                            StringBuilder sb = new StringBuilder(wordGuessedTillNow);
                            sb[i] = inputString;
                            wordGuessedTillNow = sb.ToString();

                        }
                    }

                    if (!wordGuessedTillNow.Contains('_'))
                    {
                        win = true;
                    }

                }
                else
                {
                    Console.WriteLine("'" + inputString + "' does not exist in the word!\n");
                    Console.Beep(500, 500);
                    if (!listOfInvalidGuesses.Contains(inputString)) {
                        listOfInvalidGuesses.Add(inputString);
                        totalTries--;
                    }
                    Console.WriteLine("\nTries left: " + totalTries);

                }

            }

            printGuessedWordWithTabs(wordGuessedTillNow);
            if (win == true) { Console.WriteLine("\n\nYou Won!!!!! ");
            Console.Beep(1000, 300); Console.Beep(1100, 300); Console.Beep(1200, 300);

            gameInit(arrayOfSecretWords, lives);
            }
            else if(win == false)
            {

                Console.WriteLine("\n\nYou lost! The word was '" + randomSecretWord + "'. \n.");
                Console.Beep(500, 300); Console.Beep(500, 300); Console.Beep(500, 300);
                lives--;
                if (lives == 0) 
                {
                    Console.WriteLine("Game over!");
                    Console.Beep(500, 300); Console.Beep(500, 300); Console.Beep(500, 300);
                    System.Environment.Exit(1);
                }
                gameInit(arrayOfSecretWords, lives);

            }
            Console.ReadLine();
        }

    }
}