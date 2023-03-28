using AutoMapper;
using MyNeuralNetwork.Domain.Dtos.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Entities.Nets.Networks;
using MyNeuralNetwork.Domain.Interfaces.Services.Persistences;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Core.Infra.Services.Persistences
{
    public class NeuralNetworkPersistenceService : INeuralNetworkPersistence
    {
        public string Path { get; set; } = @"";
        public string FileName { get; set; } = "NeuralNetowrk.txt";
        public string FullPath 
        { 
            get
            {
                return Path + FileName;
            } 
        }

        private readonly IMapper _mapper;

        public NeuralNetworkPersistenceService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public NeuralNetworkDto Load()
        {
            return ReadFromXmlFile<NeuralNetworkDto>(FullPath);
        }

        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                reader?.Close();
            }
        }

        public void Save(NeuralNetwork neuralNetwork)
        {
            var neuralNetworkDto = _mapper.Map<NeuralNetwork, NeuralNetworkDto>(neuralNetwork);
            WriteToXmlFile(Path + FileName, neuralNetworkDto); 
        }

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                writer?.Close();
            }
        }

        private static void PrepareFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            } 

            File.Create(path);
        }
    }
}
