﻿namespace Sandbox.Selenium
{
    using FluentAssertions;
    using OpenQA.Selenium.Chrome;
    using System.Diagnostics;

    [Collection("Chrome")] // By default xUnit categorize by different class
    [DebuggerDisplay("")]
    public class BasicTest : LongLivedMarshalByRefObject
    {
        private const string Url = "http://localhost:3000";

        [Fact]
        public void FirstTest()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);

            driver.Title.Should().BeEquivalentTo("My website title");

            driver.Close();
        }

        [Fact]
        public void UseDriverWithUsing()
        {
            using var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);

            driver.Title.Should().BeEquivalentTo("My website title");
        }
    }
}
