using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;

namespace KTGiuaKi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IPhimRepository phimRepository;
         
        public PhimController(IPhimRepository phimRepository)
        {
              this.phimRepository = phimRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetPHIMS()
        {
            try
            {
              
                return Ok(await phimRepository.GetPHIMs());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpGet("{maPhim}")]
        public async Task<ActionResult<tbPhim>> GetPHIM(string maPhim)
        {
            try
            {
                var result = await phimRepository.GetThongTinPhim(maPhim);
                if (result == null)
                    return NotFound();
                else
                    return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpDelete("{maPhim}")]
        public async Task<ActionResult<bool>> DeletePHIM(string maPhim)
        {
            try
            {
                var result = await phimRepository.DeletePHIM(maPhim);
                return result ? Ok(true) : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpPost]
        public async Task<ActionResult<tbPhim>> CreatePhim(tbPhim phim)
        {
            try
            {
                var createdPhim = await phimRepository.AddPHIM(phim);
                return CreatedAtAction(nameof(GetPHIM), new { maPhim = createdPhim.MaPhim }, createdPhim);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpPut]
        public async Task<ActionResult<tbPhim>> UpdatePhim(tbPhim phim)
        {
            try
            {
                var updatedPhim = await phimRepository.UpdatePHIM(phim);
                return Ok(updatedPhim);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }


    }
}
