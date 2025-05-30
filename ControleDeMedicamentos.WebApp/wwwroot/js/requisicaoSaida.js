const acaoAutomaticaTrigger = document.getElementById('acaoAutomaticaTrigger');
const form = document.getElementById('form-rsaida')

const pacienteId = document.getElementById('pacienteId');
const prescricaoMedicaId = document.getElementById('prescricaoMedicaId');

function submit(nomeCampo) {
    acaoAutomaticaTrigger.value = nomeCampo;
    form.submit();
}

pacienteId.addEventListener('change', () => submit('selecionarPaciente'));
prescricaoMedicaId.addEventListener('change', () => submit('selecionarPrescricao'));