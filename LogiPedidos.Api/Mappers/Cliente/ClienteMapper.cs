using AutoMapper;
using LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;
using LogiPedidosBackend.LogiPedidos.Api.DTOs.Endereco;
using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using LogiPedidosBackend.LogiPedidos.Domain.Utils;

namespace LogiPedidosBackend.LogiPedidos.Api.Mappers.Cliente;

public class ClienteMapper : Profile
{
    public ClienteMapper()
    {
        // Cliente -> ClienteReadDto
        CreateMap<Domain.Entities.Cliente, ClienteReadDto>();

        // ClienteCreateDto -> Cliente
        CreateMap<ClienteCreateDto, Domain.Entities.Cliente>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id será gerado no banco
            .ForMember(dest => dest.DataCadastro, opt => opt.Ignore()) // gerenciado internamente
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore());

        // ClienteUpdateDto -> Cliente
        CreateMap<ClienteUpdateDto, Domain.Entities.Cliente>()
            .ForMember(dest => dest.DataCadastro, opt => opt.Ignore()) // não altera dataCadastro
            .ForMember(dest => dest.DataAtualizacao, opt => opt.MapFrom(src => DateTimeProvider.NowBrasilia()))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // EnderecoDto <-> Endereco (duas vias)
        CreateMap<EnderecoDto, Endereco>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}