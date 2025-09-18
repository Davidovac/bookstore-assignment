﻿namespace BookstoreApplication.DTOs
{
    public class PublisherDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Website { get; set; }
    }
}
