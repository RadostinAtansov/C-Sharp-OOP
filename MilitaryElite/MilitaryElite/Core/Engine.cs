﻿namespace MilitaryElite.Core
{

    using MilitaryElite.Core.Contracts;
    using MilitaryElite.Exceptions;
    using MilitaryElite.Interfaces;
    using MilitaryElite.IO.Contracts;
    using MilitaryElite.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private ICollection<ISoldier> soldiers;

        private Engine()
        {
            this.soldiers = new List<ISoldier>();
        }

        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string command;

            while ((command = this.reader.ReadLine()) != "End")
            {
                string[] cmdArgs = command
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string soldierType = cmdArgs[0];
                int id = int.Parse(cmdArgs[1]);
                string firstName = cmdArgs[2];
                string lastName = cmdArgs[3];

                ISoldier soldier = null;


                if (soldierType == "Private")
                {
                    soldier = AddPrivate(cmdArgs, id, firstName, lastName);
                }
                else if (soldierType == "LieutenantGeneral")
                {
                    soldier = AddGeneral(cmdArgs, id, firstName, lastName);

                }
                else if (soldierType == "Engineer")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    string corps = cmdArgs[5];

                    try
                    {
                        soldier = AddSoldier(cmdArgs, id, firstName, lastName, salary, corps);
                    }
                    catch (InvalidCorpsException ice)
                    {

                        continue;

                    }

                }
                else if (soldierType == "Commando")
                {
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    string corps = cmdArgs[5];

                    try
                    {
                        ICommando commando = AddCommando(cmdArgs, id, firstName, lastName, salary, corps);

                        soldier = commando;

                    }
                    catch (InvalidCorpsException ice)
                    {

                        continue;
                    }

                }
                else if (soldierType == "Spy")
                {
                    int codeNumber = int.Parse(cmdArgs[4]);

                    soldier = new Spy(id, firstName, lastName, codeNumber);
                }

                if (soldier != null)
                {
                    this.soldiers.Add(soldier);
                }
            }

            foreach (var soldier in this.soldiers)
            {
                this.writer.WriteLine(soldier.ToString());
            }
        }

        private static ICommando AddCommando(string[] cmdArgs, int id, string firstName, string lastName, decimal salary, string corps)
        {
            ICommando commando = new Commando(id, firstName, lastName, salary, corps);

            string[] missionArgs = cmdArgs
                .Skip(6)
                .ToArray();

            for (int i = 0; i < missionArgs.Length; i += 2)
            {
                try
                {
                    string missionCodeName = missionArgs[i];
                    string missionState = missionArgs[i + 1];

                    IMission mission = new Mission(missionCodeName, missionState);

                    commando.AddMission(mission);
                }
                catch (InvalidStateException)
                {
                    continue;
                }

            }

            return commando;
        }

        private static ISoldier AddSoldier(string[] cmdArgs, int id, string firstName, string lastName, decimal salary, string corps)
        {
            ISoldier soldier;
            Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

            string[] repairArgs = cmdArgs
                .Skip(6)
                .ToArray();

            for (int i = 0; i < repairArgs.Length; i+=2)
            {
                string partName = repairArgs[i];
                int hoursWorked = int.Parse(repairArgs[i + 1]);

                IRepair repair = new Repair(partName, hoursWorked);

                engineer.AddRepair(repair);
            }
            soldier = engineer;
            return soldier;
        }

        private ISoldier AddGeneral(string[] cmdArgs, int id, string firstName, string lastName)
        {
            ISoldier soldier;
            decimal salary = decimal.Parse(cmdArgs[4]);
            LieutenantGeneral general = new LieutenantGeneral(id, firstName, lastName, salary);

            foreach (var pid in cmdArgs.Skip(5))
            {
                ISoldier privateToAdd = this.soldiers
                    .First(s => s.Id == int.Parse(pid));

                general.AddPrivate(privateToAdd);
            }
            soldier = general;
            return soldier;
        }

        private static ISoldier AddPrivate(string[] cmdArgs, int id, string firstName, string lastName)
        {
            ISoldier soldier;
            decimal salary = decimal.Parse(cmdArgs[4]);
            soldier = new Private(id, firstName, lastName, salary);
            return soldier;
        }
    }
}
