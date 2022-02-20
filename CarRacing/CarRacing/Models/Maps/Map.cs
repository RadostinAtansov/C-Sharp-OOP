using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            string result = string.Empty;
            if (racerOne == null)
            {
                result = $"{racerTwo} wins the race! {racerOne} was not available to race!";
            }
            else if (racerTwo == null)
            {
                result = $"{racerOne} wins the race! {racerTwo} was not available to race!";
            }
            else if (racerOne == null & racerTwo == null)
            {
                result = $"Race cannot be completed because both racers are not available!";
            }
            else
            {
                racerOne.Race();
                racerTwo.Race();
                var oneRacer = CalculateWinningRacer(racerOne);
                var secondRacer = CalculateWinningRacer(racerTwo);
                if (oneRacer > secondRacer)
                {
                    result = $"{racerOne.Username}has just raced against {racerTwo.Username}! {racerOne.Username} is the winner!";
                }
                else
                {
                    result = $"{racerOne.Username} has just raced against {racerTwo.Username}! {racerTwo.Username} is the winner!";
                }
            }
            return result;
        }

        private double CalculateWinningRacer(IRacer racer)
        {
            double calculateWinningRacer;
            if (racer.RacingBehavior == "strict")
            {
                calculateWinningRacer = racer.Car.HorsePower * racer.DrivingExperience * 1.2;
            }
            else
            {
                calculateWinningRacer = racer.Car.HorsePower * racer.DrivingExperience * 1.1;
            }
            return calculateWinningRacer;
        }
    }
}
