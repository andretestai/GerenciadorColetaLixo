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

    public class TestesColeta
    {
        private readonly Mock<IColetaService> _serviceMock;
        private readonly ColetaController _controller;

        public TestesColeta()
        {
            _serviceMock = new Mock<IColetaService>();
            _controller = new ColetaController(_serviceMock.Object);
        }

        [Fact]
        public async Task TesteAddColeta()
        {
            var model = new ColetaModel();
            _serviceMock.Setup(s => s.AddColeta(model)).ReturnsAsync(model);

            var result = await _controller.AddColeta(model);

            Assert.NotNull(result);
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task TesteGetAllColetas()
        {
            var modelList = new List<ColetaModel> { new ColetaModel(), new ColetaModel() };
            _serviceMock.Setup(s => s.GetAllColetas()).ReturnsAsync(modelList);

            var result = await _controller.GetAllColetas();

            Assert.NotNull(result);
            Assert.Equal(modelList.Count, result.Count);
        }

        [Fact]
        public async Task TestesGetColeta()
        {
            var model = new ColetaModel { Id = 1 };
            _serviceMock.Setup(s => s.GetColeta(1)).ReturnsAsync(model);

            var result = await _controller.GetColeta(1);

            Assert.NotNull(result);
            Assert.Equal(model.Id, result.Id);
        }

        [Fact]
        public async Task TestesUpdateColeta()
        {
            var model = new ColetaModel();
            _serviceMock.Setup(s => s.UpdateColeta(model)).ReturnsAsync(model);

            var result = await _controller.UpdateColeta(model);

            Assert.NotNull(result);
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task DeleteColeta_OkWhenDelete()
        {
            _serviceMock.Setup(s => s.DeleteColeta(1)).ReturnsAsync(true);

            var result = await _controller.DeleteColeta(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Coleta excluída com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeleteColeta_NotFound()
        {
            _serviceMock.Setup(s => s.DeleteColeta(1)).ReturnsAsync(false);

            var result = await _controller.DeleteColeta(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Coleta não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetColetaByUsuarioTeste()
        {
            var modelList = new List<ColetaModel> { new ColetaModel(), new ColetaModel() };
            _serviceMock.Setup(s => s.GetColetaByUsuario(1)).ReturnsAsync(modelList);

            var result = await _controller.GetColetaByUsuario(1);

            Assert.NotNull(result);
            Assert.Equal(modelList.Count, result.Count);
        }
    }
}
