#nullable disable
using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers;

public class HotelsController : BaseController
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IQuartoRepository _quartoRepository;
    private readonly IMapper _mapper;

    public HotelsController(IHotelRepository hotelRepository,
                            IQuartoRepository quartoRepository,         
                            IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _quartoRepository = quartoRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<HotelViewModel>>(await _hotelRepository.ObterTodos()));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var hotelViewModel = _mapper.Map<HotelViewModel>(await _hotelRepository.ObterHotelQuartos(id));

        if (hotelViewModel == null) 
            return NotFound();

        return View(hotelViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HotelViewModel hotelViewModel)
    {
        hotelViewModel = await PopularQuartos(hotelViewModel);

        if (!ModelState.IsValid) 
            return View(hotelViewModel);

        if (hotelViewModel.FotosUpload != null)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if(!await UploadArquivo(hotelViewModel.FotosUpload, imgPrefixo))
                return View(hotelViewModel);

            hotelViewModel.Fotos = imgPrefixo + hotelViewModel.FotosUpload.FileName;
        }

        await _hotelRepository.Adicionar(_mapper.Map<Hotel>(hotelViewModel));

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var hotelViewModel = await ObterHotelQuartos(id);

        if (hotelViewModel == null) 
            return NotFound();

        return View(hotelViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, HotelViewModel hotelViewModel)
    {
        if (id != hotelViewModel.Id) 
            return NotFound();

        var hotelAtualizacao = await ObterHotelQuartos(id);
        hotelViewModel.Fotos = hotelAtualizacao.Fotos;

        if (!ModelState.IsValid) 
            return View(hotelViewModel);

        if (hotelViewModel.FotosUpload != null)
        {
            var imgPrefixo = Guid.NewGuid() + "_";

            if(!await UploadArquivo(hotelViewModel.FotosUpload, imgPrefixo))
                return View(hotelViewModel);

            hotelViewModel.Fotos = imgPrefixo + hotelViewModel.FotosUpload.FileName;
        }

        hotelAtualizacao.Nome = hotelViewModel.Nome;
        hotelAtualizacao.CNPJ = hotelViewModel.CNPJ;
        hotelAtualizacao.Descricao = hotelViewModel.Descricao;
        hotelAtualizacao.Endereco = hotelViewModel.Endereco;

        await _hotelRepository.Atualizar(_mapper.Map<Hotel>(hotelAtualizacao));

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var hotelViewModel = await ObterHotelQuartos(id);

        if (hotelViewModel == null)
            return NotFound();

        return View(hotelViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var hotelViewModel = await ObterHotelQuartos(id);

        if (hotelViewModel == null)
            return NotFound();

        await _hotelRepository.Remover(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<HotelViewModel> ObterHotelQuartos(Guid id) => 
        _mapper.Map<HotelViewModel>(await _hotelRepository.ObterHotelQuartos(id));

    public async Task<HotelViewModel> PopularQuartos(HotelViewModel hotelViewModel)
    {
        var quartosViewModels = _mapper.Map<IEnumerable<QuartoViewModel>>(await _quartoRepository.ObterQuartosPorHotel(hotelViewModel.Id));

        hotelViewModel.Quartos = quartosViewModels;

        return hotelViewModel;
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