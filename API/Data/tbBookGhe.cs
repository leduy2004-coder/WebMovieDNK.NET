using API.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class tbBookGhe
{
    [StringLength(20)]
    public string MaGhe { get; set; }

    [StringLength(20)]
    public string MaBook { get; set; }

    // Khóa ngoại - Mối quan hệ với tbGhe
    [ForeignKey("MaGhe")]
    public virtual tbGhe Ghe { get; set; }

    // Khóa ngoại - Mối quan hệ với tbBookVe
    [ForeignKey("MaBook")]
    public virtual tbBookVe BookVe { get; set; }
}
