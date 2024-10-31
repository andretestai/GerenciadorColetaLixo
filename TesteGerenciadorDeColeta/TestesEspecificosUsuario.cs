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

    public class TestesEspecificosUsuario
    {
        private readonly Mock<IUsuarioService> _serviceMock;
        private readonly UsuarioController _controller;

        public TestesEspecificosUsuario()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _controller = new UsuarioController(_serviceMock.Object);
        }

        private JSchema GetUsuarioSchema()
        {
            return JSchema.Parse(@"{
            'type': 'object',
            'properties': {
                'Id': {'type': ['integer', 'null']},
                'Nome': {'type': ['string', 'null']},
                'Email': {'type': ['string', 'null']},
                'Telefone': {'type': ['string', 'null']},
                'Coletas': {
                    'type': 'array',
                    'items': {
                        'type': 'object',
                        'properties': {
                            'Id': {'type': 'integer'},
                            'DataColeta': {'type': ['string', 'null'], 'format': 'date-time'},
                            'Local': {'type': ['string', 'null']},
                            'TipoResiduo': {'type': ['string', 'null']},
                            'UsuarioId': {'type': 'integer'}
                        },
                        'required': ['Id', 'UsuarioId']
                    }
                }
            },
            'required': ['Id', 'Nome', 'Email']
        }");
        }

        [Fact]
        public async Task AddUsuario_ReturnsCreatedUsuario_WithValidStatusCodeAndJsonSchema()
        {
            var usuario = new UsuarioModel { Id = 1, Nome = "John Doe", Email = "john@example.com", Telefone = "123456789" };
            _serviceMock.Setup(service => service.AddUsuario(It.IsAny<UsuarioModel>())).ReturnsAsync(usuario);

            var result = await _controller.AddUsuario(usuario);
            var jsonResult = JObject.FromObject(result);
            var schema = GetUsuarioSchema();

            Assert.NotNull(result);
            Assert.IsType<UsuarioModel>(result);

            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta não está em conformidade com o schema.");
        }

        [Fact]
        public async Task GetAllUsuarios_ReturnsAllUsuarios_WithValidStatusCodeAndJsonSchema()
        {
            var usuarios = new List<UsuarioModel>
        {
            new UsuarioModel { Id = 1, Nome = "John Doe", Email = "john@example.com", Telefone = "123456789" },
            new UsuarioModel { Id = 2, Nome = "Jane Doe", Email = "jane@example.com", Telefone = "987654321" }
        };
            _serviceMock.Setup(service => service.GetAllUsuarios()).ReturnsAsync(usuarios);

            var result = await _controller.GetAllUsuarios();
            var jsonResult = JArray.FromObject(result);
            var schema = GetUsuarioSchema();

            Assert.Equal(2, result.Count);
            foreach (var item in jsonResult)
            {
                Assert.True(item.IsValid(schema), "O JSON de resposta de cada usuário não está em conformidade com o schema.");
            }
        }

        [Fact]
        public async Task GetUsuario_ReturnsUsuario_WithValidStatusCodeAndJsonSchema()
        {
            var usuario = new UsuarioModel { Id = 1, Nome = "John Doe", Email = "john@example.com", Telefone = "123456789" };
            _serviceMock.Setup(service => service.GetUsuario(1)).ReturnsAsync(usuario);

            var result = await _controller.GetUsuario(1);
            var jsonResult = JObject.FromObject(result);
            var schema = GetUsuarioSchema();

            Assert.NotNull(result);
            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta do usuário não está em conformidade com o schema.");
        }

        [Fact]
        public async Task UpdateUsuario_ReturnsUpdatedUsuario_WithValidStatusCodeAndJsonSchema()
        {
            var usuario = new UsuarioModel { Id = 1, Nome = "Jane Doe", Email = "jane.doe@example.com", Telefone = "987654321" };
            _serviceMock.Setup(service => service.UpdateUsuario(It.IsAny<UsuarioModel>())).ReturnsAsync(usuario);

            var result = await _controller.UpdateUsuario(usuario);
            var jsonResult = JObject.FromObject(result);
            var schema = GetUsuarioSchema();

            Assert.True(jsonResult.IsValid(schema), "O JSON de resposta do usuário atualizado não está em conformidade com o schema.");
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsOk_WhenUsuarioDeleted_WithValidStatusCode()
        {
            _serviceMock.Setup(service => service.DeleteUsuario(1)).ReturnsAsync(true);

            var result = await _controller.DeleteUsuario(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Usuário excluído com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsNotFound_WhenUsuarioNotFound_WithValidStatusCode()
        {
            _serviceMock.Setup(service => service.DeleteUsuario(1)).ReturnsAsync(false);

            var result = await _controller.DeleteUsuario(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Usuário não encontrado.", notFoundResult.Value);
        }
    }
}
