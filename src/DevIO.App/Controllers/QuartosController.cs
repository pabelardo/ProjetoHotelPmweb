#nullable disable
using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers;

public class QuartosController : BaseController
{
    private readonly IQuartoRepository _quartoRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public QuartosController(IQuartoRepository quartoRepository,
                             IHotelRepository hotelRepository,
                             IMapper mapper)
    {
        _quartoRepository = quartoRepository;
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<QuartoViewModel>>(await ObterQuartosHotels()));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var quartoViewModel = await ObterQuarto(id);

        if (quartoViewModel == null)
        {
            return NotFound();
        }

        return View(quartoViewModel);
    }

    public async Task<IActionResult> Create()
    {
        var quartoViewModel = await PopularHotels(new QuartoViewModel());

        return View(quartoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(QuartoViewModel quartoViewModel)
    {
        quartoViewModel = await PopularHotels(quartoViewModel);

        if (!ModelState.IsValid)
            return View(quartoViewModel);

        if (quartoViewModel.FotosUpload != null)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if(!await UploadArquivo(quartoViewModel.FotosUpload, imgPrefixo))
                return View(quartoViewModel);

            quartoViewModel.Fotos = imgPrefixo + quartoViewModel.FotosUpload.FileName;
        }

        await _quartoRepository.Adicionar(_mapper.Map<Quarto>(quartoViewModel));

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var quartoViewModel = await ObterQuarto(id);

        if (quartoViewModel == null)
        {
            return NotFound();
        }

        await PopularHotels(quartoViewModel);

        return View(quartoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, QuartoViewModel quartoViewModel)
    {
        if (id != quartoViewModel.Id)
            return NotFound();

        var quartoAtualizacao = await ObterQuarto(id);
        quartoViewModel.Fotos = quartoAtualizacao.Fotos;

        if (!ModelState.IsValid)
            return View(quartoViewModel);

        if (quartoViewModel.FotosUpload != null)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if(!await UploadArquivo(quartoViewModel.FotosUpload, imgPrefixo))
                return View(quartoViewModel);

            quartoViewModel.Fotos = imgPrefixo + quartoViewModel.FotosUpload.FileName;
        }

        quartoAtualizacao.HotelId = quartoViewModel.HotelId;
        quartoAtualizacao.Nome = quartoViewModel.Nome;
        quartoAtualizacao.NumeroOcupantes = quartoViewModel.NumeroOcupantes;
        quartoAtualizacao.NumeroDeAdultos = quartoViewModel.NumeroDeAdultos;
        quartoAtualizacao.NumeroDeCriancas = quartoViewModel.NumeroDeCriancas;
        quartoAtualizacao.Preco = quartoViewModel.Preco;

        await _quartoRepository.Atualizar(_mapper.Map<Quarto>(quartoAtualizacao));

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var quartoViewModel = await ObterQuarto(id);

        if (quartoViewModel == null)
        {
            return NotFound();
        }

        return View(quartoViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var quartoViewModel = await ObterQuarto(id);
        
        if (quartoViewModel == null)
        {
            return NotFound();
        }

        await _quartoRepository.Remover(id);

        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<QuartoViewModel>> ObterQuartosHotels() => 
        _mapper.Map<IEnumerable<QuartoViewModel>>(await _quartoRepository.ObterQuartosHotels());

    private async Task<QuartoViewModel> ObterQuarto(Guid id) => 
        _mapper.Map<QuartoViewModel>(await _quartoRepository.ObterQuartoHotel(id));

    private async Task<QuartoViewModel> PopularHotels(QuartoViewModel quartoViewModel)
    {
        var hotels = _mapper.Map<IEnumerable<HotelViewModel>>(await _hotelRepository.ObterTodos());

        quartoViewModel.Hotels = hotels;

        return quartoViewModel;
    }

    private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
    {
        if (arquivo.Length <= 0) 
            return false;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", string.Concat(imgPrefixo, arquivo.FileName));

        if (System.IO.File.Exists(path))
        {
            ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            return false;
        }

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return true;
    }
}