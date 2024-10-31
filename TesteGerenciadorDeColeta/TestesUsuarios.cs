namespace TesteGerenciadorDeColeta
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using APIColetaDeLixo.Controllers;
    using APIColetaDeLixo.Interfaces.Services;
    using APIColetaDeLixo.Models;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class TestesUsuarios
    {
        private readonly Mock<IUsuarioService> _serviceMock;
        private readonly UsuarioController _controller;

        public TestesUsuarios()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _controller = new UsuarioController(_serviceMock.Object);
        }

        [Fact]
        public async Task AddUsuarioTestes()
        {
            var model = new UsuarioModel();
            _serviceMock.Setup(s => s.AddUsuario(model)).ReturnsAsync(model);

            var result = await _controller.AddUsuario(model);

            Assert.NotNull(result);
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task GetAllUsuariosTestes()
        {
            var modelList = new List<UsuarioModel> { new UsuarioModel(), new UsuarioModel() };
            _serviceMock.Setup(s => s.GetAllUsuarios()).ReturnsAsync(modelList);

            var result = await _controller.GetAllUsuarios();

            Assert.NotNull(result);
            Assert.Equal(modelList.Count, result.Count);
        }

        [Fact]
        public async Task GetUsuarioTestes()
        {
            var model = new UsuarioModel { Id = 1 };
            _serviceMock.Setup(s => s.GetUsuario(1)).ReturnsAsync(model);

            var result = await _controller.GetUsuario(1);

            Assert.NotNull(result);
            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public async Task UpdateUsuarioTestes()
        {
            var model = new UsuarioModel();
            _serviceMock.Setup(s => s.UpdateUsuario(model)).ReturnsAsync(model);

            var result = await _controller.UpdateUsuario(model);

            Assert.NotNull(result);
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task DeleteUsuario_OkWhenDelete()
        {
            _serviceMock.Setup(s => s.DeleteUsuario(1)).ReturnsAsync(true);

            var result = await _controller.DeleteUsuario(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Usuário excluído com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeleteUsuario_NotFound()
        {
            _serviceMock.Setup(s => s.DeleteUsuario(1)).ReturnsAsync(false);

            var result = await _controller.DeleteUsuario(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Usuário não encontrado.", notFoundResult.Value);
        }
    }

}