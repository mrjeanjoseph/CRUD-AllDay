using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace AsyncProgramming
{
    public class ProgramInit
    {
        public static void RunToMain()
        {
            //Run this
            ListingTwoThirteen();
            Console.ReadLine();

            //No longer running
            ListingTwoEight();
            ListingTwoSeven();
            Console.ReadLine();

        }
        public async Task<string> DownloadPage(string uri)
        { // Listing 2-15, using async/await features
            using (WebClient client = new WebClient())
            {
                string stringHtml = await client.DownloadStringTaskAsync(uri);
                return stringHtml;
            }
        }

        static void ListingTwoFourteen()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(1000);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            AsyncFactory.InternalGetIntAsync(cts.Token).ContinueWith((task) =>
            {
                Console.WriteLine("Elapsed time: {0}ms", watch.Elapsed.TotalMilliseconds);
                watch.Stop();

                //Now we get the response, we display the CancellationTokenSource
                //So that it is not going to signal
                cts.Dispose();

                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine(task.Result);
                else if (task.Status == TaskStatus.Canceled)
                    Console.WriteLine("The task has been cancelled");
                else
                    Console.WriteLine("An error has been occurred. Details:\n" +
                        task.Exception.InnerException.Message);

                Console.ReadLine();
            });

        }

        static void ListingTwoThirteen()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(1000);

            AsyncFactory.GetIntAsyncBogusOper(cts.Token).ContinueWith((task) =>
            {
                //Now we get the response
                //So we display the CancellationTokenSource
                //So that it is not going to signal
                cts.Dispose();

                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine(task.Result);
                else if (task.Status == TaskStatus.Canceled)
                    Console.WriteLine("The task has been cancelled");
                else
                    Console.WriteLine("An error has been occurred. Details:\n" +
                        task.Exception.InnerException.Message);
            });
            Console.ReadLine();
        }

        static void ListingTwoEleven()
        {
            var intTask = AsyncFactory.GetIntAsyncTwo();

            if (intTask.IsCompleted)
                Console.WriteLine("Completed Instantly: {0}", intTask.Result);
            else
            {
                intTask.ContinueWith((task) =>
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                        Console.WriteLine(task.Result);
                    else if (task.Status == TaskStatus.Canceled)
                        Console.WriteLine("The task has been cancelled");
                    else
                        Console.WriteLine("An error has been occurred. Details:\n" +
                            task.Exception.InnerException.Message);
                });
            }
            Console.ReadLine();
        }

        static void ListingTwoTen()
        {
            AsyncFactory.GetIntAsync(default).ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine(task.Result);
                else if (task.Status == TaskStatus.Canceled)
                    Console.WriteLine("The task has been canceled");
                else
                {
                    Console.WriteLine("An error has been occurred. Details:");
                    Console.WriteLine(task.Exception.InnerException.Message);
                }
            });
        }

        static void ListingTwoEight()
        {
            //First Async operation
            var httpTwitterClient = new HttpClient();
            Task<string> twitterTask = httpTwitterClient.GetStringAsync("https://twitter.com");

            //Second async opration
            var httpGoogleClient = new HttpClient();
            Task<string> googleTask = httpGoogleClient.GetStringAsync("https://google.com");

            Task<string[]> task = Task.WhenAll(twitterTask, googleTask);

            task.ContinueWith(stringArray =>
            {
                //All of the tasks have been completed.
                //Reaching out to the result property will not block
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    for (int i = 0; i < 10; i++)
                        Console.WriteLine(stringArray.Result[i].Substring(0, 100));
                }
                else if (task.Status == TaskStatus.Canceled)
                    Console.WriteLine("The task has been completed. ID: {0}", task.Id);
                else
                {
                    Console.WriteLine("An error has been occurred. Detail:");
                    foreach (var ex in task.Exception.InnerExceptions)
                        Console.WriteLine(ex.Message);
                }
            });
            Console.ReadLine();
        }

        static void ListingTwoSeven()
        {
            //First Async operation
            var httpTwitterClient = new HttpClient();
            Task<string> twitterTask = httpTwitterClient.GetStringAsync("https://twitter.com");

            //Second async opration
            var httpGoogleClient = new HttpClient();
            Task<string> googleTask = httpGoogleClient.GetStringAsync("https://google.com");

            Task.Factory.ContinueWhenAll(new[] { twitterTask, googleTask }, (tasks) =>
            {
                //All of the tasks have been completed.
                //Reaching out to the result property will not block
                foreach (var task in tasks)
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                        Console.WriteLine(task.Result.Substring(0, 100));
                    else if (task.Status == TaskStatus.Canceled)
                        Console.WriteLine("The task has been completed. ID: {0}", task.Id);
                    else
                    {
                        Console.WriteLine("An error has been occurred. Detail:");
                        Console.WriteLine(task.Exception.InnerException.Message);
                    }
                }
            });
            Console.ReadLine();
        }

        static void ListingTwoSix()
        {
            var doworktask = DoWorkAsync();

            if (doworktask.IsCompleted)
                Console.WriteLine(doworktask.Result);
            else
            {
                doworktask.ContinueWith(task =>
                {
                    Console.WriteLine(task.Result);
                }, TaskContinuationOptions.NotOnFaulted);

                doworktask.ContinueWith(task =>
                {
                    Console.WriteLine(task.Exception.InnerException.Message);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        public void ListingTwoFive()
        {
            DoWorkAsync().ContinueWith(task =>
            {
                Console.WriteLine(task.Result);
            }, TaskContinuationOptions.NotOnFaulted);

            DoWorkAsync().ContinueWith(task =>
            {
                Console.WriteLine(task.Exception.InnerException.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        static Task<string> DoWorkAsync()
        {
            return Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                return "Hello, can you C# with Async/Await?";
            });
        }

    }
}
