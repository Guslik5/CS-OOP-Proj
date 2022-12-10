﻿using Banks.Commands;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Moduls;

namespace Banks.Entities;

public class Bank
{
    private readonly List<User> _listUsers = new List<User>();
    public Bank(string name, List<IConfig> configs, decimal percentComission, decimal defaultPercentDeposite, decimal limitCreditAccount, decimal maxSumIfUserNotTrusted)
    {
        if (percentComission < 0 || defaultPercentDeposite < 0 || limitCreditAccount < 0)
        {
            throw new PerсentException("Invalid percent");
        }

        if (configs.Count() != 3)
        {
            throw new ConfigException("Invalid config");
        }

        MaxSumIfUserNotTrusted = maxSumIfUserNotTrusted;
        LimitCreditAccount = limitCreditAccount;
        DefaultPercentDeposite = defaultPercentDeposite;
        PercentComission = percentComission;
        Configs = configs;
        Name = name;
    }

    public List<IConfig> Configs { get; }
    public decimal PercentComission { get; }
    public decimal MaxSumIfUserNotTrusted { get; }
    public decimal LimitCreditAccount { get; }
    public decimal DefaultPercentDeposite { get; }
    public string Name { get; }
    public List<User> ListUsers => _listUsers;
}