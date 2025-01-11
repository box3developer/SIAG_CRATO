# Caminho das pastas
$sourcePath = "Cadastro\Repositories"
$destinationPath = "Core\Repositories"

# Criar o diretório de destino, se não existir
if (-Not (Test-Path -Path $destinationPath)) {
    New-Item -ItemType Directory -Path $destinationPath
}

# Loop para processar cada arquivo na pasta de origem
Get-ChildItem -Path $sourcePath -Filter "*.cs" | ForEach-Object {
    $sourceFile = $_.FullName
    $destinationFile = Join-Path -Path $destinationPath -ChildPath $_.Name

    # Copiar o arquivo para o destino
    Copy-Item -Path $sourceFile -Destination $destinationFile

    # Ler o conteúdo do arquivo
    $fileContent = Get-Content -Path $destinationFile

    # Atualizar o namespace genérico de Cadastro para Core
    $fileContent = $fileContent -replace "namespace (.+?)\.Cadastro\.(.+)", "namespace `$1.Core.`$2"

    # Atualizar os usings de Cadastro para Core
    $fileContent = $fileContent -replace "using (.+?)\.Cadastro\.(.+)", "using `$1.Core.`$2"

    # Salvar o conteúdo atualizado
    Set-Content -Path $destinationFile -Value $fileContent

    Write-Host "Arquivo processado: $($_.Name)"
}

Write-Host "Todos os arquivos foram copiados e ajustados."
