﻿using System;
using TechTalk.SpecFlow.Specs.Drivers.CucumberMessages;
using TechTalk.SpecFlow.TestProjectGenerator;
using TechTalk.SpecFlow.TestProjectGenerator.Driver;

namespace TechTalk.SpecFlow.Specs.StepDefinitions.CucumberMessages
{
    [Binding]
    public class TestRunStartedSteps
    {
        private readonly VSTestExecutionDriver _vsTestExecutionDriver;
        private readonly CucumberMessagesDriver _cucumberMessagesDriver;
        private readonly SolutionDriver _solutionDriver;
        private readonly TestSuiteSetupDriver _testSuiteSetupDriver;
        private readonly TestSuiteInitializationDriver _testSuiteInitializationDriver;

        public TestRunStartedSteps(VSTestExecutionDriver vsTestExecutionDriver, CucumberMessagesDriver cucumberMessagesDriver, SolutionDriver solutionDriver, TestSuiteSetupDriver testSuiteSetupDriver, TestSuiteInitializationDriver testSuiteInitializationDriver)
        {
            _vsTestExecutionDriver = vsTestExecutionDriver;
            _cucumberMessagesDriver = cucumberMessagesDriver;
            _solutionDriver = solutionDriver;
            _testSuiteSetupDriver = testSuiteSetupDriver;
            _testSuiteInitializationDriver = testSuiteInitializationDriver;
        }

        [When(@"the test suite is executed")]
        [When(@"the test suite was executed")]
        public void WhenTheTestSuiteIsExecuted()
        {
            _testSuiteSetupDriver.EnsureAProjectIsCreated();
            _solutionDriver.CompileSolution(BuildTool.MSBuild);
            _solutionDriver.CheckSolutionShouldHaveCompiled();
            _vsTestExecutionDriver.ExecuteTests();
        }

        [When(@"the test suite is started at '(.*)'")]
        public void WhenTheTestSuiteIsStartedAt(DateTime startTime)
        {
            _testSuiteSetupDriver.EnsureAProjectIsCreated();
            _testSuiteInitializationDriver.OverrideStartupTime = startTime;
        }

        [Then(@"a TestRunStarted message has been sent")]
        public void ThenATestRunStartedMessageHasBeenSent()
        {
            _cucumberMessagesDriver.TestRunStartedMessageShouldHaveBeenSent();
        }

        [Then(@"a TestRunStarted message has been sent with the following attributes")]
        public void ThenATestRunStartedMessageHasBeenSentWithTheFollowingAttributes(Table attributesTable)
        {
            _cucumberMessagesDriver.TestRunStartedMessageShouldHaveBeenSent(attributesTable);
        }
    }
}
