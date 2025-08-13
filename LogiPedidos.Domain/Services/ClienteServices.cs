using AutoMapper;
using LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;
using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Repository;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Services;
using Serilog;

namespace LogiPedidosBackend.LogiPedidos.Domain.Services;

public class ClienteServices : IClienteServices
{
    private readonly IMapper _mapper;
    private readonly IClienteRepository _clienteRepository;

    public ClienteServices(IMapper mapper, IClienteRepository clienteRepository)
    {
        _mapper = mapper;
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteReadDto> AddAsync(ClienteCreateDto dto)
    {
        Log.Information("Tentando adicionar cliente...");
        var clienteEntity = _mapper.Map<Cliente>(dto);
        
        var cliente = await _clienteRepository.AddAsync(clienteEntity);

        if (cliente == null)
        {
            Log.Error("Falha ao adicionar cliente");
            throw new Exception("Falha ao adicionar cliente.");
        }
        
        Log.Information("Cliente adicionado com sucesso. Id: {ClienteId}", cliente.Id);
        return _mapper.Map<ClienteReadDto>(cliente);
    }

    public async Task<ClienteReadDto?> GetByIdAsync(Guid id)
    {
        Log.Information("Buscando cliente por id: {ClienteId}", id);
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente == null)
        {
            Log.Warning("Cliente não encontrado. Id: {ClienteId}", id);
            return null;
        }

        Log.Information("Cliente encontrado. Id: {ClienteId}", id);
        return _mapper.Map<ClienteReadDto>(cliente);
    }

    public async Task<IEnumerable<ClienteReadDto>> GetAllAsync()
    {
        Log.Information("Buscando todos os clientes...");
        var clientes = await _clienteRepository.GetAllAsync();
        
        Log.Information("Total de clientes encontrados: {Count}", clientes.Count());
        return _mapper.Map<IEnumerable<ClienteReadDto>>(clientes);
    }

    public async Task<ClienteReadDto?> UpdateAsync(ClienteUpdateDto dto)
    {
        Log.Information("Atualizando cliente Id: {ClienteId}", dto.Id);

        var clienteExistente = await _clienteRepository.GetByIdAsync(dto.Id);
        if (clienteExistente == null)
        {
            Log.Warning("Cliente não encontrado para atualização. Id: {ClienteId}", dto.Id);
            return null;
        }

        // Mapeamento de Cliente e Endereço
        var clienteAtualizado = _mapper.Map(dto, clienteExistente);
        clienteExistente.Endereco = _mapper.Map(dto.Endereco, clienteAtualizado.Endereco);
        
        var updated = await _clienteRepository.UpdateAsync(clienteAtualizado);

        if (!updated)
        {
            Log.Error("Falha ao atualizar cliente. Id: {ClienteId}", dto.Id);
            throw new Exception("Falha ao atualizar cliente.");
        }

        Log.Information("Cliente atualizado com sucesso. Id: {ClienteId}", dto.Id);
        return _mapper.Map<ClienteReadDto>(clienteAtualizado);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Log.Information("Deletando cliente Id: {ClienteId}", id);
        var clienteExistente = await _clienteRepository.GetByIdAsync(id);
        
        if (clienteExistente == null)
        {
            Log.Warning("Cliente não encontrado para deleção. Id: {ClienteId}", id);
            return false;
        }

        var deleted = await _clienteRepository.DeleteAsync(id);
        if (deleted)
            Log.Information("Cliente deletado com sucesso. Id: {ClienteId}", id);
        else
            Log.Error("Falha ao deletar cliente. Id: {ClienteId}", id);

        return deleted;
    }
}
