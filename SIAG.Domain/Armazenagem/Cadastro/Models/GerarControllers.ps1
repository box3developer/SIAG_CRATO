# Caminhos das pastas
$modelFolder = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.Domain\Armazenagem\Cadastro\Models"
$controllerFolder = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.API\Controllers\Armazenagem\Cadastro"

# Função para extrair a chave primária do modelo
function Get-PrimaryKey {
    param (
        [string]$modelPath
    )

    $modelContent = Get-Content -Path $modelPath

    # Procurar a propriedade marcada com [Key]
    $primaryKeyLine = $modelContent | Select-String -Pattern '\[Key\]' -Context 0, 2

    if ($primaryKeyLine) {
        # Pegar o tipo e o nome da propriedade
        $propertyLine = $primaryKeyLine.Context.PostContext | Select-String -Pattern '^\s*public\s+([\w\?]+)\s+(\w+)' | ForEach-Object {
            [PSCustomObject]@{
                Type = $_.Matches.Groups[1].Value
                Name = $_.Matches.Groups[2].Value
            }
        }
        return $propertyLine
    } else {
        Write-Host "[WARN] Nenhuma chave primária encontrada no modelo: $modelPath"
        return $null
    }
}

# Criar o controller com base no modelo
function Create-Controller {
    param (
        [string]$modelPath,
        [string]$controllerPath
    )

    $modelName = [System.IO.Path]::GetFileNameWithoutExtension($modelPath)
    $dtoName = "${modelName}DTO"

    # Obter a chave primária
    $primaryKey = Get-PrimaryKey -modelPath $modelPath

    if (-not $primaryKey) {
        Write-Host "[ERROR] Não foi possível determinar a chave primária para o modelo: $modelName"
        return
    }

    $primaryKeyType = $primaryKey.Type

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
            IBaseService<IBaseRepository<${modelName}, ${primaryKeyType}>, ${modelName}, ${primaryKeyType}, ${dtoName}>,
            IBaseRepository<${modelName}, ${primaryKeyType}>,
            ${modelName},
            ${primaryKeyType},
            ${dtoName}
        >
    {
        public ${modelName}Controller(IBaseService<IBaseRepository<${modelName}, ${primaryKeyType}>, ${modelName}, ${primaryKeyType}, ${dtoName}> service) : base(service)
        {
        }
    }
}
"@

    Set-Content -Path $controllerPath -Value $controllerContent
    Write-Host "[INFO] Controller criado: $controllerPath"
}

# Iterar sobre os modelos
Get-ChildItem -Path $modelFolder -Filter "*.cs" | ForEach-Object {
    $modelPath = $_.FullName
    $modelName = $_.BaseName
    $controllerPath = Join-Path -Path $controllerFolder -ChildPath "${modelName}Controller.cs"

    if (-Not (Test-Path -Path $controllerPath)) {
        Write-Host "[INFO] Criando controller para o modelo: $modelName"
        Create-Controller -modelPath $modelPath -controllerPath $controllerPath
    } else {
        Write-Host "[INFO] Controller já existe para o modelo: $modelName"
    }
}
