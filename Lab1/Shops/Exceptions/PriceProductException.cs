﻿namespace Shops.Exceptions;

public class PriceProductException : Exception
{
    public PriceProductException(string message)
        : base(message) { }
}