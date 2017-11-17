using ServidorWS.Dto;
using ServidorWS.Mapping;
using ServidorWS.Models;
using ServidorWS.Repository;
using ServidorWS.Services;
using ServidorWS.XML;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ServidorWS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            //Inversão de Controle - Injeção das Dependências
            container.RegisterType<DbContext, AlunosContexto>();
            container.RegisterType(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            container.RegisterType<IAlunosService, AlunosService>();
            container.RegisterType<IImportacaoService, ImportacaoService>();
            container.RegisterType<IMapper<AlunoXML, Aluno>, AlunoXMLMapper>();
            container.RegisterType<IMapper<ImportacaoDto, Importacao>, ImportacaoMapper>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}