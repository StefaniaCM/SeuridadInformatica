using System;
using System.Collections.Generic;

namespace GestionSchool.Models;

public partial class Login
{
    public int? Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;
}
