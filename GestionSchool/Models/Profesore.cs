using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionSchool.Models;

public partial class Profesore
{
    public int DocumentoIdentidad { get; set; }
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Profesion { get; set; }
    public string? ImagenUrl { get; set; }
    [NotMapped] //No genere cambios en la base de datos
    public IFormFile ImagenFile { get; set; }

   
}



