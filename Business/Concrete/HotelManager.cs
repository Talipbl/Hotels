using Business.Abstracts;
using Core.Utilities.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Concrete.Utilities.Results;
using Core.Concrete.Utilities.Results.Abstracts;
using Core.CrossCuttingConcerns;
using Core.Utilities.Results;
using Entity.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstracts;
using System.Xml;
using Core.Utilities.Results.Abstracts;
using FluentValidation.Results;

namespace Business.Concrete
{
    public class HotelManager : IHotelService
    {
        IHotelDal _hotelDal;

        public HotelManager(IHotelDal hotelDal)
        {
            _hotelDal = hotelDal;
        }

        public IDataResult<List<Hotel>> Read(string path)
        {
            List<Hotel> hotels = new List<Hotel>();
            var result =  ReadDocument(path, hotels);
            return new DataResult<List<Hotel>>(hotels, result.Success, result.Message);
        }

        public IResult WriteAsJson(string path, List<Hotel> hotels)
        {
            path = CreateFilePath(path,".json");
            return _hotelDal.WriteAsJson(path, hotels);
        }

        public IResult WriteAsXml(string path, List<Hotel> hotels)
        {
            path = CreateFilePath(path, ".xml");
            return _hotelDal.WriteAsXml(path, hotels);
        }
        private static string CreateFilePath(string path,string type)
        {
            var endIndex = path.LastIndexOf('\\');
            var name = ((path.Substring(endIndex)).Remove(0, 1)).Split('.')[0];
            var newPath = $"{path.Remove(endIndex)}\\{name}{type}";
            return newPath;
        }
        private static IResult ReadDocument(string path, List<Hotel> hotels)
        {
            using (var reader = new StreamReader($"{path}"))
            {
                Hotel hotel;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var startIndex = line.IndexOf('"');
                    var endIndex = line.LastIndexOf('"');
                    if (startIndex > -1)
                    {
                        var address = line.Substring(startIndex, endIndex - startIndex);
                        var values = (line.Remove(startIndex, endIndex - startIndex)).Split(',');
                        hotel = new Hotel()
                        {
                            Name = values[0],
                            Address = address,
                            Star = int.Parse(values[2]),
                            Contact = values[3],
                            PhoneNumber = values[4],
                            Url = values[5]
                        };
                        try
                        {
                            ValidationTool.Validate(new HotelValidator(), hotel);
                        }
                        catch (Exception exp)
                        {
                            return new Result(false, exp.Message);
                        }                        
                        hotels.Add(hotel);
                    }
                }
            }
            return new Result(true,Messages.FileUploaded);
        }
    }
}
