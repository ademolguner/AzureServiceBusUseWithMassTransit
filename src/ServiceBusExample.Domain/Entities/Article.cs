﻿namespace ServiceBusExample.Domain.Entities
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IsIndex { get; set; }
    }
}