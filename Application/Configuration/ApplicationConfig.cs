using Application.Policy.Handlers;
using DirectoriesCreator;
using DirectoriesCreator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Ninject;
using Serializer;
using Serializer.Interfaces;
using Storage;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public class ApplicationConfig
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
            _kernel.Bind<ISerializer>().To<SerializerService>();
            _kernel.Bind<IDataBaseService>().To<DataBaseService>();
            _kernel.Bind<IFilePath>().To<FilePath>();
            _kernel.Bind<IDirectoryService>().To<DirectoryService>();
            return _kernel;
        }
    }
}
