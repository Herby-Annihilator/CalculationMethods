using CalculationMethods.Core.Entities;
using CalculationMethods.Core.Services.Repositories;
using CalculationMethods.Infrastructure.Entities.String;
using CalculationMethods.Infrastructure.Services.Repositories.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationMethods.Infrastructure.Services.Repositories.String
{
    public class StringVectorRepository : IVectorRepository<string>
    {
        private string _path;
        private VectorFileOptions _options;
        public StringVectorRepository(string path, VectorFileOptions options)
        {
            _path = path;
            _options = options;
        }
        public bool Delete(IVector<string> vector)
        {
            Guid guid = Guid.NewGuid();
            string tempFile = $"{_path}_{guid}";
            StreamReader reader = new StreamReader(_path);
            StreamWriter writer = new StreamWriter(tempFile);
            string buffer;
            while ((buffer = reader.ReadLine()) != null)
            {
                if (buffer == _options.OpenTag)
                {
                    do
                    {
                        buffer = reader.ReadLine();
                    } while (buffer != null && buffer != _options.CloseTag);
                    if (buffer == null)
                        break;
                    continue;
                }
                writer.WriteLine(buffer);
            }
            writer.Close();
            reader.Close();
            File.Delete(_path);
            File.Move(tempFile, _path);
            return true;
        }

        public IVector<string> Get()
        {
            StreamReader reader = new StreamReader(_path);
            string buffer;
            string[] values;
            List<string> tmp = new List<string>();
            while ((buffer = reader.ReadLine()) != null)
            {
                if (buffer == _options.OpenTag)
                {
                    do
                    {
                        buffer = reader.ReadLine();
                        values = buffer.Split(_options.Delimiter, StringSplitOptions.RemoveEmptyEntries);
                        tmp.AddRange(values);
                    } while (buffer != null && buffer != _options.CloseTag);
                    if (buffer == null)
                        break;
                }
            }
            reader.Close();
            StringVector result = new StringVector(tmp.Count);
            int index = 0;
            foreach (var item in tmp)
            {
                result[index] = item;
                index++;
            }
            return result;
        }

        public void Save(IVector<string> vector)
        {
            int size = vector.Size * (1 + _options.Delimiter.Length) + _options.OpenTag.Length 
                + _options.CloseTag.Length + 4;
            StringBuilder builder = new StringBuilder(size);
            builder.Append(_options.OpenTag + "\r\n");
            for (int i = 0; i < vector.Size; i++)
            {
                builder.Append(vector[i] + " ");
            }
            builder.Append(_options.CloseTag + "\r\n");
            File.AppendAllText(_path, builder.ToString());           
        }

        public bool Update(IVector<string> vector)
        {
            Delete(vector);
            Save(vector);
            return true;
        }
    }
}
