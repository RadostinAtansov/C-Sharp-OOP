

using CarRacing.Models.Cars;

namespace Cars.Models
{
    public class TunedCar : Car
    {
        public TunedCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, 65, 7.5)
        {
        }
    }
}
