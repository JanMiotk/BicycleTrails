using DirectoriesCreator.Interfaces;
using DownloadDataFromTraseo.Configuration;
using DownloadDataFromTraseo.FileOperations;
using DownloadDataFromTraseo.Interfaces;
using DownloadDataFromTraseo.Parser;
using Models;
using Ninject;
using Parser.Interfaces;
using Parser.Parser;
using System;

namespace DownloadDataFromTraseo
{
    class Program : IProgram
    {
        static void Main(string[] args)
        {
            var kernel = new Config().GetKernel();

            var program = kernel.Get<IProgram>();

            program.Run(kernel.Get<IDirectoryService>(), kernel.Get<ITrailDataDownloader>(), kernel.Get<IGPXTrailDataDownloader>(),
                kernel.Get<ITrailAdditionalInformationDataDownloader>(), kernel.Get<IFileCombining>()) ;

            Console.WriteLine("End of Operations");
            Console.ReadKey();
        }

        public void Run(IDirectoryService directoryService, ITrailDataDownloader trailDataDownloader, IGPXTrailDataDownloader gPXTrailDataDownloader
            , ITrailAdditionalInformationDataDownloader trailAdditionalInformationDataDownloader, IFileCombining fileCombining )
        {
            directoryService.CreateDirectories();
            trailDataDownloader.DownloadData();
            gPXTrailDataDownloader.DownloadData();
            trailAdditionalInformationDataDownloader.DownloadData();
            fileCombining.CombineFiles();
        }
    }
}
