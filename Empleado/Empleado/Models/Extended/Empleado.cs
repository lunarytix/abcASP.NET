using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace Empleado.Models.Extended
{
    [MetadataType(typeof(EmpleadoMetadata)) ]
    public class Empleado
    {


    }

    
    public class EmpleadoMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " porfavor inserte el valor nombre")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " porfavor inserte el valor Apellido Paterno")]
        public string ApPaterno { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " Departamento Vacio") ]
        public string Departamento { get; set;}

        

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FecNac { get; set; }
    }

}