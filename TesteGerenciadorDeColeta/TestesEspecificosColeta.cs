namespace TesteGerenciadorDeColeta
{
    using Moq;
    using Newtonsoft.Json.Schema;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using APIColetaDeLixo.Controllers;
    using APIColetaDeLixo.Interfaces.Services;
    using APIColetaDeLixo.Models;

    public class TestesEspecificosColeta
    {
        private readonly Mock<IColetaService> _serviceMock;
        private readonly ColetaController _controller;

        public TestesEspecificosColeta()
        {
            _serviceMock = new Mock<IColetaService>();
            _controller = new ColetaController(_serviceMock.Object);
        }

        private JSchema GetColetaSchema()
        {
            return JSchema.Parse(@"{
            'type': 'object',
            'properties': {
                'Id': {'type': 'integer'},
                'DataColeta': {'type': ['string', 'null'], 'format': 'date-time'},
                'Local': {'type': ['string', 'null']},
                'TipoResiduo': {'type': ['string', 'null']},
                'UsuarioId': {'type': 'integer'},
                'Usuario': {'type': ['object', 'null']}
            },
            'required': ['Id', 'UsuarioId']
        }");
        }

        [Fact]
        public async Task AddColeta_ReturnsCreatedColeta_WithValidStatusCodeAndJsonSchema()
        {
            var coleta = new ColetaModel { Id = 1, Local = "Teste", TipoResiduo = "Papel", UsuarioId = 2 };
            _serviceMock.Setup(service => service.AddColeta(It.IsAny<ColetaModel>())).ReturnsAsync(coleta);

            var result = await _controller.AddColeta(coleta);
            var jsonResult = JObject.FromObject(result);
            var schema = GetColetaSchema();

            Assert.NotNull(result);
            Assert.IsType<ColetaModel>(result);

            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta não está em conformidade com o schema.");
        }

        [Fact]
        public async Task GetAllColetas_ReturnsAllColetas_WithValidStatusCodeAndJsonSchema()
        {
            var coletas = new List<ColetaModel>
        {
            new ColetaModel { Id = 1, Local = "Teste1", TipoResiduo = "Plástico", UsuarioId = 2 },
            new ColetaModel { Id = 2, Local = "Teste2", TipoResiduo = "Vidro", UsuarioId = 3 }
        };
            _serviceMock.Setup(service => service.GetAllColetas()).ReturnsAsync(coletas);

            var result = await _controller.GetAllColetas();
            var jsonResult = JArray.FromObject(result);
            var schema = GetColetaSchema();

            Assert.Equal(2, result.Count);
            foreach (var item in jsonResult)
            {
                Assert.True(item.IsValid(schema), "O JSON de resposta de cada coleta não está em conformidade com o schema.");
            }
        }

        [Fact]
        public async Task GetColeta_ReturnsColeta_WithValidStatusCodeAndJsonSchema()
        {
            var coleta = new ColetaModel { Id = 1, Local = "Teste", TipoResiduo = "Metal", UsuarioId = 3 };
            _serviceMock.Setup(service => service.GetColeta(1)).ReturnsAsync(coleta);

            var result = await _controller.GetColeta(1);
            var jsonResult = JObject.FromObject(result);
            var schema = GetColetaSchema();

            Assert.NotNull(result);
            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta da coleta não está em conformidade com o schema.");
        }

        [Fact]
        public async Task UpdateColeta_ReturnsUpdatedColeta_WithValidStatusCodeAndJsonSchema()
        {
            var coleta = new ColetaModel { Id = 1, Local = "Atualizado", TipoResiduo = "Orgânico", UsuarioId = 4 };
            _serviceMock.Setup(service => service.UpdateColeta(It.IsAny<ColetaModel>())).ReturnsAsync(coleta);

            var result = await _controller.UpdateColeta(coleta);
            var jsonResult = JObject.FromObject(result);
            var schema = GetColetaSchema();

            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta da coleta atualizada não está em conformidade com o schema.");
        }

        [Fact]
        public async Task DeleteColeta_ReturnsOk_WhenColetaDeleted_WithValidStatusCode()
        {
            _serviceMock.Setup(service => service.DeleteColeta(1)).ReturnsAsync(true);

            var result = await _controller.DeleteColeta(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Coleta excluída com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeleteColeta_ReturnsNotFound_WhenColetaNotFound_WithValidStatusCode()
        {
            _serviceMock.Setup(service => service.DeleteColeta(1)).ReturnsAsync(false);

            var result = await _controller.DeleteColeta(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Coleta não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetColetaByUsuario_ReturnsColetasForUser_WithValidStatusCodeAndJsonSchema()
        {
            var coletas = new List<ColetaModel>
        {
            new ColetaModel { Id = 1, Local = "UserLocal1", UsuarioId = 2 },
            new ColetaModel { Id = 2, Local = "UserLocal2", UsuarioId = 2 }
        };
            _serviceMock.Setup(service => service.GetColetaByUsuario(2)).ReturnsAsync(coletas);

            var result = await _controller.GetColetaByUsuario(2);
            var jsonResult = JArray.FromObject(result);
            var schema = GetColetaSchema();

            Assert.NotNull(result);
            foreach (var item in jsonResult)
            {
                Assert.True(item.IsValid(schema), "O JSON de resposta de cada coleta do usuário não está em conformidade com o schema.");
            }
        }
    }

}
