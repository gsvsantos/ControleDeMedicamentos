const acaoAutomaticaTrigger = document.getElementById('acaoAutomaticaTrigger');
const form = document.getElementById('form-rentrada');

const funcionarioId = document.getElementById('funcionarioId');
const medicamentoId = document.getElementById('medicamentoId');
const quantidadeMedicamento = document.getElementById('quantidadeMedicamento')

function submit(nomeCampo){
    acaoAutomaticaTrigger.value = nomeCampo;
form.submit();
}

funcionarioId.addEventListener('change', () => submit('selecionarFuncionario'));
medicamentoId.addEventListener('change', () => submit('selecionarMedicamento'));
quantidadeMedicamento.addEventListener('change', () => submit('informarQuantidade'));