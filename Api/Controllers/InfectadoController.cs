    using System;
    using Api.Data.Collections;
    using Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;

namespace Api.Controllers
{

        [ApiController]
        [Route("[controller]")]

    public class InfectadoController: ControllerBase
    {
      Data.MongoDB _mongoDB;
      IMongoCollection<Infectado> _infectadoCollection;
      public InfectadoController(Data.MongoDB mongoDB)
      {
          _mongoDB = mongoDB;
          _infectadoCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());

      }   

      [HttpPost]
      public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
      {
          var infectado = new Infectado(dto.dataNascimento, dto.Sexo, dto.Latitude,dto.Longitude);
          _infectadoCollection.InsertOne(infectado);
          return StatusCode(201, "Infectado adicionado com sucesso");
      } 
      [HttpGet]
      public ActionResult ObterInfectados()
      {
          var infectados = _infectadoCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
          return Ok(infectados);
      }
    }
}