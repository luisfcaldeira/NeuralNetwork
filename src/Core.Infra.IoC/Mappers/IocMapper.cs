using AutoMapper;
using Core.Infra.Services.Persistences;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Entities.Commons.Fields.Numerics;
using MyNeuralNetwork.Domain.Entities.Nets;
using MyNeuralNetwork.Domain.Entities.Nets.Layers;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons;
using MyNeuralNetwork.Domain.Entities.Nets.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Networks;
using MyNeuralNetwork.Domain.Interfaces.Networks.Circuits.Forward;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Activations;
using MyNeuralNetwork.Domain.Interfaces.Neurons.Parts;
using MyNeuralNetwork.Domain.Interfaces.Services.Persistences;
using System;

namespace Core.Infra.IoC.Mappers
{
    public class IocMapper
    {
        private IServiceProvider _services;
        private IServiceProvider _provider;

        public IocMapper()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    // IHostEnvironment env = hostingContext.HostingEnvironment;

                    //configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    //IConfigurationRoot configurationRoot = configuration.Build();

                })
                .ConfigureServices((_, services) => {
                    services.AddTransient<INeuralNetworkPersistence, NeuralNetworkPersistenceService>();
                    
                    services.AddTransient<IMapper>((x) => {
                        var mapperConfiguration = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<NeuralDoubleValue, double>().ConvertUsing(f => f.Value);
                            cfg.CreateMap<double, NeuralDoubleValue>().ConvertUsing(f => new NeuralDoubleValue(f));

                            cfg.CreateMap<ICircuitForward, string>().ConvertUsing(c => c.GetType().Name);   

                            cfg.CreateMap<NeuralFloatValue, float>().ConvertUsing(f => f.Value);
                            cfg.CreateMap<float, NeuralFloatValue>().ConvertUsing(f => new NeuralFloatValue(f));

                            cfg.CreateMap<Neuron, NeuronDto>();

                            cfg.CreateMap<IActivator, string>().ConstructUsing(c => c.GetType().FullName);

                            cfg.CreateMap<ISynapseManager, SynapseManagerDto>();

                            cfg.CreateMap<INeuralNetwork, NeuralNetworkDto>();

                            cfg.CreateMap<Layer, LayerDto>();
                            cfg.CreateMap<Synapse, SynapseDto>().ForMember(s => s.TargetGuid, opt => opt.MapFrom((x) => x.NeuronSource.Index));
                        });
                        var executionPlan = mapperConfiguration.BuildExecutionPlan(typeof(NeuralNetwork), typeof(NeuralNetworkDto));

                        return new Mapper(mapperConfiguration);
                    });
                })
                .Build();

            //host.RunAsync();

            _services = host.Services;
            _provider = _services.CreateScope().ServiceProvider;
        }


        public T Get<T>()
        {
            return _services.GetService<T>();
        }

        public T GetService<T>()
        {
            return _provider.GetService<T>();
        }
    }
}
