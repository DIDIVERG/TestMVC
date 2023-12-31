﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace WebApplication10.DataAccessLayer.Models;

public class SearchResult
{
    [Key]
    public int Id { get; set; }
    [Url]
    public string Url { get; set; }
    public string Snippet { get; set; }
    
    [JsonIgnore]
    [InverseProperty(nameof(Query.Results))]
    public IEnumerable<Query> Queries { get; set; }
}