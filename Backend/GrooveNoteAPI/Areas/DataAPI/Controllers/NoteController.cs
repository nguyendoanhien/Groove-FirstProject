using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GrooveNoteDAL.Models.Note;
using GrooveNoteDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GrooveNoteAPI.Areas.DataAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("DataAPI/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("")]
        public IEnumerable<FullModel> GetNotes()
        {
            var result = _noteService.GetNoteListFullModel();
            return result;
        }

        [HttpGet("{id}")]
        public EditModel GetNote(int id)
        {
            var data = _noteService.GetNoteForEdit(id);
            return data;
        }

        [HttpPut("{id}")]
        public EditModel EditNote(int id, [FromBody] EditModel note)
        {
            if (id != note.Id)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                var isExisting = _noteService.CheckExisting(id);
                if (!isExisting)
                {
                    return null;
                }

                _noteService.EditNote(note);
                return note;
            }

            return null;
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] CreateModel note)
        {
            if (ModelState.IsValid)
            {
                _noteService.AddNote(note);
            }
            else
            {
                return new BadRequestResult();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var isExisting = _noteService.CheckExisting(id);
            if (!isExisting)
            {
                return new NotFoundResult();
            }

            _noteService.DeleteNote(id);
            return Ok();
        }
    }
}