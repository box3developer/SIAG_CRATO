# Caminho das pastas
$modelFolder = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.Domain\Armazenagem\Cadastro\Models"
$controllerFolder = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.API\Controllers\Armazenagem\Cadastro"

# Função para determinar o tipo da chave primária
function Get-PrimaryKeyType {
    param (
        [string]$modelPath
    )
    $content = Get-Content -Path $modelPath -Raw
    if ($content -match "\[Key\]\s*public\s+([a-zA-Z0-9]+)\??\s+([a-zA-Z0-9]+)") {
        return $matches[1]
    } else {
        return "Guid" # Valor padrão se não encontrar [Key]
    }
}

# Gerar controllers
Get-ChildItem -Path $modelFolder -Filter *.cs | ForEach-Object {
    $modelName = $_.BaseName
    $controllerName = "${modelName}Controller.cs"
    $controllerPath = Join-Path -Path $controllerFolder -ChildPath $controllerName

    # Verificar se o controller já existe
    if (-Not (Test-Path -Path $controllerPath)) {
        Write-Host "Criando o controller para o modelo: $modelName"

        # Determinar o tipo da chave primária
        $primaryKeyType = Get-PrimaryKeyType -modelPath $_.FullName

        # Gerar o conteúdo do controller
        $controllerContent = @"
using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ${modelName}Controller : BaseController<
            IBaseService<IBaseRepository<${modelName}, $primaryKeyType>, ${modelName}, $primaryKeyType, ${modelName}DTO>,
            IBaseRepository<${modelName}, $primaryKeyType>,
            ${modelName},
            $primaryKeyType,
            ${modelName}DTO
        >
    {
        public ${modelName}Controller(IBaseService<IBaseRepository<${modelName}, $primaryKeyType>, ${modelName}, $primaryKeyType, ${modelName}DTO> service) : base(service)
        {
        }
    }
}
"@

        # Salvar o arquivo do controller
        $controllerContent | Set-Content -Path $controllerPath
    } else {
        Write-Host "O controller para o modelo $modelName já existe. Pulando..."
    }
}

Write-Host "Script concluído!"
