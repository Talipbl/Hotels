
using Business.Concrete;
using DataAccess.Concrete;

HotelManager hotelManager = new HotelManager(new HotelDal());
hotelManager.Read(@"hotels.csv");
