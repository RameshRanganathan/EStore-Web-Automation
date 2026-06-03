Title: Test Store Automation Framework – Setup & Execution Guide

Introduction:  
This document explains how to set up and run the automated test framework for Automation Test Store. The framework is built with C#, NUnit, Selenium WebDriver, Reqnroll (SpecFlow), and ExtentReports.

Prerequisites:

.NET 6.0 SDK or later

Visual Studio 2022 (or JetBrains Rider)

Chrome browser (latest)

ChromeDriver (matching your Chrome version)

Setup Steps:

Clone the repository:
git clone https://github.com/your-org/test-store-automation.git

Restore NuGet packages:
dotnet restore

Ensure ChromeDriver is available in your PATH or configure it in DriverFactory.cs.

Execution:

Run all NUnit tests:
dotnet test

Run BDD scenarios:
dotnet test --filter TestCategory=BDD

Reports are generated in bin/Debug/net6.0/ExtentReport.html.

Project Structure:

Pages → Page Object classes (BooksPage, LoginPage, etc.)

BDD/FeatureFiles → Gherkin feature files

BDD/StepDefinitions → Step definition classes

Tests → NUnit test classes

Utils → DriverFactory, ExtentReportManager, PageBase

Supported Features:

Product category tests (Books, Apparel, Fragrance, HairCare, MakeUp, Men, SkinCare)

Registration tests (positive and negative)

Login tests (authentication, validation, negative scenarios)

ExtentReports integration for HTML reporting

PageBase abstraction for reusable methods