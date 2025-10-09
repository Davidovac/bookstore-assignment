﻿namespace BookstoreApplication.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("You do not have permission to perform this action.")
        {
        }
    }
}
