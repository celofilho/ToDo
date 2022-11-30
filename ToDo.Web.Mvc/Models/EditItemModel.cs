using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Mvc.Models
{
    public class EditItemModel
    {
        public Guid Id { get; set; }

        [StringLength(256, MinimumLength = 5)]
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
