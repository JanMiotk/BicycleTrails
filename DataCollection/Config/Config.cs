using DownloadDataFromTraseo.Interfaces;
using DownloadDataFromTraseo.Parser;
using DownloadDataFromTraseo.FileOperations;
using Ninject;
using System;
using System.Collections.Generic;
using Serializer;
using Serializer.Interfaces;
using Parser.Parser;
using DirectoriesCreator.Interfaces;
using DirectoriesCreator;
using Parser.Interfaces;

namespace DownloadDataFromTraseo.Configuration
{
    public class Config
    {
        private IKernel _kernel;

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IKernel GetKernel()
        {
            if (_kernel != null)
            {
                return _kernel;
            }
            _kernel = new StandardKernel();
            _kernel.Bind<IProgram>().To<Program>();
            _kernel.Bind<IFilePath>().To<FilePath>();
            _kernel.Bind<IDirectoryService>().To<DirectoryService>();
            _kernel.Bind<ITrailAdditionalInformationDataDownloader>().To<TrailAdditionalInformationDataDownloader>();
            _kernel.Bind<IFileCombining>().To<FileCombining>();
            _kernel.Bind<ISerializer>().To<SerializerService>();
            _kernel.Bind<ITrailDataDownloader>().To<TrailDataDownloader>();
            _kernel.Bind<IGPXTrailDataDownloader>().To<GPXTrailDataDownloader>();
            return _kernel;
        }
    }
}
