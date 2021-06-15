using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AirCampusTask.Models;
using AirCampusTask.Repositories;
using Mos.xApi;
using Mos.xApi.Actors;
using Mos.xApi.Objects;
using Xamarin.Forms;
using Mos.xApi;
using Mos.xApi.Client;

namespace AirCampusTask.ViewModels
{
    public class LearningTaskViewModel : ViewModel
    {
        private readonly LearningTaskRepository _repository;
        public LearningTask LearningTask { get; set; }

        public LearningTaskViewModel(LearningTaskRepository repository)
        {
            _repository = repository;
            LearningTask = new LearningTask() {Due = DateTime.Now.AddDays(1)};
        }

        public ICommand Save => new Command(async () =>
        {
            await _repository.AddOrUpdate(LearningTask);
            await Navigation.PopAsync();
        });
        
        public ICommand StartStop => new Command(async () =>
        {
            if (StartStopTask())
            {
                Console.WriteLine("startstoptask true");
                await _repository.AddOrUpdate(LearningTask);
                await Navigation.PopAsync();
            }
            Console.WriteLine("startstoptask false");
        });

        public bool StartStopTask()
        {
            if (LearningTask.Started.Equals(LearningTask.Finished))
            {
                Console.WriteLine("gestart");
                LearningTask.Started = DateTime.Now;
                return true;
            }
            else if(LearningTask.Started > LearningTask.Finished)
            {
                Console.WriteLine("gestopt");
                LearningTask.Finished = DateTime.Now;
                return true;
            }

            testApi();
            Console.WriteLine("afgehandeld");
            return false;
        }

        public async Task testApi()
        {
            var newStatement =
                Statement.Create(
                        Actor.CreateAgent("Jan Kaas").WithMailBox("jankaas@test.nl"),
                        Verb.Create("http://adlnet.gov/expapi/verbs/attempted").AddDisplay("en-US", "attempted"),
                        StatementObject.CreateActivity("http://aircampus.datadienst.nl/xapi/cis/cursus")
                            .AddName("en-US", "Netwerkbeheerder")
                            .AddDescription("en-US", "Een fantastische cursus"))
                    .WithResult(Result.Create()
                        .WithScore(new Score(0.95))
                        .WithSuccess(true)
                        .WithCompletion(true)
                        .WithDuration(TimeSpan.FromSeconds(1234)))
                    .Build();
            
            var json = newStatement.ToJson(true);
            Console.WriteLine(json);
            
            ILrsClient lrsClient = new LrsClient(new Uri("https://aclrs.lrs.io:443/xapi/"));
            lrsClient.SetBasicAuthentication("miteza","mugela");

            Statement statement = await lrsClient.GetStatementAsync(new Guid("d0371e17-4e91-46ba-924f-e78168bf0f02"));

            //var statementResult = await lrsClient.FindStatement(new StatementQuery{ActivityId = new Uri("http://adlnet.gov/expapi/verbs/completed")});

            //var statements = statementResult.Statements;

            // if(statements.More != null){
            //     var moreStatementResult = await lrsClient.FindMoreStatements(statements.More);
            // }

            await lrsClient.SendStatementAsync(newStatement);
        }
        
    }
}