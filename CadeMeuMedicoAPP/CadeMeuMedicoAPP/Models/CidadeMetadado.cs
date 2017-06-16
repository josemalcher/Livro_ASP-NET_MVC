﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedicoAPP.Models
{
    [MetadataType(typeof(CidadeMetadado))]
    public partial class Cidade
    {

    }

    public class CidadeMetadado
    {
        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        [StringLength(100, ErrorMessage = "O Nome deve possuir no máximo 100 caracteres")]
        public string Nome { get; set; }
    }
}