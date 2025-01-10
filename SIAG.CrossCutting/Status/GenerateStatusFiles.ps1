# Caminho do arquivo com todas as classes de status
$inputFilePath = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.CrossCutting\Status\StatusClasses.cs"

# Diretório onde os arquivos das classes serão salvos
$outputDirectory = "C:\Users\crist\Documents\BOX3\GRENDENE_CRATO\SIAG\SIAG.CrossCutting\Status"

# Namespace para as classes
$namespace = "SIAG.CrossCutting.Status"

# Criar o diretório de saída, se não existir
if (!(Test-Path -Path $outputDirectory)) {
    New-Item -ItemType Directory -Path $outputDirectory -Force
}

# Ler o conteúdo do arquivo de entrada
$content = Get-Content -Path $inputFilePath -Raw

# Separar cada classe pelo padrão "public class"
$classes = $content -split "(?=public class)"

# Verificar se classes foram encontradas
if ($classes.Count -eq 0) {
    Write-Host "Nenhuma classe encontrada no arquivo. Verifique o conteúdo do arquivo."
    exit
}

# Processar cada classe
foreach ($class in $classes) {
    # Ignorar se a string da classe estiver vazia
    if ($class.Trim() -eq "") {
        continue
    }

    # Extrair o nome da classe
    if ($class -match "public class (\w+)\s*{") {
        $className = $matches[1]

        # Criar o conteúdo do arquivo com o namespace
        $classFileContent = @"
namespace $namespace
{
    $class
}
"@

        # Caminho do arquivo de saída
        $outputFilePath = Join-Path -Path $outputDirectory -ChildPath "$className.cs"

        # Escrever o arquivo da classe
        Set-Content -Path $outputFilePath -Value $classFileContent

        Write-Host "Classe '$className' salva em '$outputFilePath'"
    }
}