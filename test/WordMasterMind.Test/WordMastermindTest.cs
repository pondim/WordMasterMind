using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordMasterMind.Models;

namespace WordMasterMind;

[TestClass]
public class WordMasterMindTest
{
    private static string GetTestRoot(string? fileName = null)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var startupPath = Path.GetDirectoryName(path: assembly.Location);
        Debug.Assert(condition: startupPath != null, message: nameof(startupPath) + " != null");
        var pathItems = startupPath.Split(separator: Path.DirectorySeparatorChar);
        var pos = pathItems.Reverse().ToList().FindIndex(match: x => string.Equals(a: "bin", b: x));
        var basePath = string.Join(separator: Path.DirectorySeparatorChar.ToString(),
            values: pathItems.Take(count: pathItems.Length - pos - 1));
        return fileName is null ? basePath : Path.Combine(path1: basePath, path2: fileName);
    }


    
    [TestMethod]
    public void TestWordMasterMindWordTooShort()
    {
        var scrabbleDictionary =
            new ScrabbleDictionary(pathToDictionaryJson: GetTestRoot(fileName: "scrabble-dictionary.json"));
        var thrownException = Assert.ThrowsException<ArgumentException>(action: () =>
            new WordMasterMind.Models.WordMasterMind(
                minLength: 5,
                maxLength: 5,
                maxAttempts: 6,
                hardMode: false,
                scrabbleDictionary: scrabbleDictionary,
                // secretWord is valid, but not long enough
                secretWord: "wow"));
        Assert.AreEqual(expected: "Secret word must be between minLength and maxLength",
            actual: thrownException.Message);
    }
    
    [TestMethod]
    public void TestWordMasterMindWordTooLong()
    {
        var scrabbleDictionary =
            new ScrabbleDictionary(pathToDictionaryJson: GetTestRoot(fileName: "scrabble-dictionary.json"));
        var thrownException = Assert.ThrowsException<ArgumentException>(action: () =>
            new WordMasterMind.Models.WordMasterMind(
                minLength: 5,
                maxLength: 5,
                maxAttempts: 6,
                hardMode: false,
                scrabbleDictionary: scrabbleDictionary,
                // secretWord is valid, but too long
                secretWord: "invalid"));
        Assert.AreEqual(expected: "Secret word must be between minLength and maxLength",
            actual: thrownException.Message);
    }

    [TestMethod]
    public void TestWordMasterMindWordNotInDictionary()
    {
        var scrabbleDictionary =
            new ScrabbleDictionary(pathToDictionaryJson: GetTestRoot(fileName: "scrabble-dictionary.json"));
        var thrownException = Assert.ThrowsException<ArgumentException>(action: () =>
            new WordMasterMind.Models.WordMasterMind(
                minLength: 5,
                maxLength: 5,
                maxAttempts: 6,
                hardMode: false,
                scrabbleDictionary: scrabbleDictionary,
                // secretWord is made up word not in dictionary
                secretWord: "fizzbuzz"));
        Assert.AreEqual(expected: "Secret word must be a valid word in the Scrabble dictionary",
            actual: thrownException.Message);
    }


    [TestMethod]
    public void TestWordMasterMind()
    {
        var scrabbleDictionary =
            new ScrabbleDictionary(pathToDictionaryJson: GetTestRoot(fileName: "scrabble-dictionary.json"));
        var mastermind = new Models.WordMasterMind(
            minLength: 5,
            maxLength: 5,
            maxAttempts: 6,
            hardMode: false,
            scrabbleDictionary: scrabbleDictionary);
    }

    [TestMethod]
    public void TestWordMasterMindHardMode()
    {
        var scrabbleDictionary =
            new ScrabbleDictionary(pathToDictionaryJson: GetTestRoot(fileName: "scrabble-dictionary.json"));
        var mastermind = new Models.WordMasterMind(
            minLength: 5,
            maxLength: 5,
            maxAttempts: 6,
            hardMode: false,
            scrabbleDictionary: scrabbleDictionary);
    }
}