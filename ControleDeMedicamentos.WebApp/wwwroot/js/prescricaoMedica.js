const acaoAutomaticaTrigger = document.getElementById('acaoAutomaticaTrigger');
const form = document.getElementById('form-prescricao')

const cRMMedico = document.getElementById('cRMMedico');
const pacienteId = document.getElementById('pacienteId');

const medicamentoId = document.getElementById('medicamentoId');
const dosagemMedicamento = document.getElementById('dosagemMedicamento');
const periodoMedicamento = document.getElementById('periodoMedicamento');
const quantidadeMedicamento = document.getElementById('quantidadeMedicamento');

function submit(nomeCampo) {
    acaoAutomaticaTrigger.value = nomeCampo;
    form.submit();
}

cRMMedico.addEventListener('change', () => submit('informarCRM'));
pacienteId.addEventListener('change', () => submit('selecionarPaciente'));

medicamentoId.addEventListener('change', () => submit('selecionarMedicamento'));
dosagemMedicamento.addEventListener('change', () => submit('informarDosagem'));
periodoMedicamento.addEventListener('change', () => submit('informarPeriodo'));
quantidadeMedicamento.addEventListener('change', () => submit('informarQuantidade'));
