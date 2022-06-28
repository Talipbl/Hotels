using Core.Concrete.Utilities.Results;
using Core.Concrete.Utilities.Results.Abstracts;
using Core.Utilities.Constants;
using DataAccess.Abstracts;
using Entity.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccess.Concrete
{
    public class HotelDal : IHotelDal
    {
        public IResult Read()
        {
            throw new NotImplementedException();
        }

        public IResult WriteAsJson(string path, List<Hotel> hotels)
        {
            string jsonValue = JsonConvert.SerializeObject(hotels);
            if (!File.Exists(path))
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(jsonValue);
                }
                return new Result(true, Messages.FileCreated);
            }
            return new Result(false, Messages.FileAlreadyExist);
        }

        public IResult WriteAsXml(string path, List<Hotel> hotels)
        {
            if (!File.Exists(path))
            {
                using (var writer = XmlWriter.Create(path))
                {
                    var columnNames = typeof(Hotel).GetProperties().Select(p => p.Name).ToArray();
                    writer.WriteStartDocument();
                    foreach (var hotel in hotels)
                    {
                        writer.WriteStartElement(columnNames[0]);
                        writer.WriteValue(hotel.Name);
                        writer.WriteStartElement(columnNames[1]);
                        writer.WriteValue(hotel.Address);
                        writer.WriteStartElement(columnNames[2]);
                        writer.WriteValue(hotel.Star);
                        writer.WriteStartElement(columnNames[3]);
                        writer.WriteValue(hotel.Contact);
                        writer.WriteStartElement(columnNames[4]);
                        writer.WriteValue(hotel.PhoneNumber);
                        writer.WriteStartElement(columnNames[5]);
                        writer.WriteValue(hotel.Url);
                    }
                    writer.WriteEndElement();
                }
                return new Result(true, Messages.FileCreated);
            }
            return new Result(false, Messages.FileAlreadyExist);
        }
    }
}
