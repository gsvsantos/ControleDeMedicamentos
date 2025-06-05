const formPrescricao = document.getElementById('form-prescricao')
const formMedicamento = document.getElementById('form-medicamento')
const cRMMedico = document.getElementById('cRMMedico');
const pacienteId = document.getElementById('pacienteId');
const medicamentoId = document.getElementById('medicamentoId');
const dosagemMedicamento = document.getElementById('dosagemMedicamento');
const periodoMedicamento = document.getElementById('periodoMedicamento');
const quantidadeMedicamento = document.getElementById('quantidadeMedicamento');
const cRMMedicoInvalidInput = document.getElementById('cRMMedicoInvalidInput');
const dosagemInvalidInput = document.getElementById('dosagemInvalidInput');
const periodoInvalidInput = document.getElementById('periodoInvalidInput');
const quantidadeInvalidInput = document.getElementById('quantidadeInvalidInput');
const temMedicamentos = formPrescricao.dataset.temMedicamentos === "true";

function isSiglaCRMValid() {
    const siglasValidas = [
        "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG",
        "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SE", "SP", "TO"
    ];

    let sigla = cRMMedico.value.match(/[A-Za-z]{2}$/)[0].toUpperCase();

    return siglasValidas.includes(sigla);
}

function isCRMMedicoValid() {
    let isValid = true;

    if (cRMMedico.value.trim() === "") {
        cRMMedicoInvalidInput.textContent = 'C.R.M. Médico é obrigatório';
        isValid = false;
    }
    else if (!cRMMedico.value.match(/^\d{6}\s?-?[A-Za-z]{2}$/)) {
        cRMMedicoInvalidInput.textContent = 'C.R.M. Médico Inválido (Ex: \'123456-RS\' ou \'123456 RS\')';
        isValid = false;
    }
    else if (!isSiglaCRMValid()) {
        cRMMedicoInvalidInput.textContent = 'Sigla Estadual Inválida';
        isValid = false;
    }
    else {
        cRMMedicoInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isDosagemValid() {
    let isValid = true;

    if (dosagemMedicamento.value.trim() === "") {
        dosagemInvalidInput.textContent = 'Dosagem é obrigatório';
        isValid = false;
    }
    else if (dosagemMedicamento.value.length < 10 || dosagemMedicamento.value.length > 50) {
        dosagemInvalidInput.textContent = 'Dosagem deve ter entre 10 e 50 caracteres';
        isValid = false;
    }
    else {
        dosagemInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isPeriodoValid() {
    let isValid = true;

    if (periodoMedicamento.value.trim() === "") {
        periodoInvalidInput.textContent = 'Período é obrigatório';
        isValid = false;
    }
    else if (periodoMedicamento.value.length < 10 || periodoMedicamento.value.length > 50) {
        periodoInvalidInput.textContent = 'Período deve ter entre 10 e 100 carateres';
        isValid = false;
    }
    else {
        periodoInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isQtdMedicamentoValid() {
    let isValid = true;

    if (quantidadeMedicamento.value.trim() === "") {
        quantidadeInvalidInput.textContent = 'Quantidade é obrigatória';
        isValid = false;
    }
    else if (quantidadeMedicamento.value <= 0) {
        quantidadeInvalidInput.textContent = 'A quantidade inicial não pode ser zero ou negativa.';
        isValid = false;
    }
    else {
        quantidadeInvalidInput.textContent = '';
        isValid = true;
    }

    return isValid;
}

function pacienteIdHasChild() {
    let hasChild = false;

    for (let item of pacienteId.children) {
        if (item.value != '' && !item.hasAttribute('disabled'))
            hasChild = true;
    }

    return hasChild;
}

function medicamentoIdHasChild() {
    let hasChild = false;

    for (let item of medicamentoId.children) {
        if ((item.value != 'Selecione um Medicamento' || item.value != '') && !item.hasAttribute('disabled'))
            hasChild = true;
    }

    return hasChild;
}

function isValorEmpty(valor) {
    let isEmpty = false;

    if (valor.value == null || valor.value == '' || valor.value == undefined)
        isEmpty = true;

    return isEmpty;
}

function atualizarValorOptionPaciente() {
    let mensagemErro = '';

    if (!pacienteIdHasChild()) {
        mensagemErro = 'Nenhum Paciente Encontrado';
    }
    else if (isValorEmpty(cRMMedico) || !isCRMMedicoValid()) {
        mensagemErro = 'Digite o C.R.M. Médico Acima';
    }
    else if (isValorEmpty(pacienteId)) {
        mensagemErro = 'Selecione um Paciente';
    }
    else if (temMedicamentos) {
        mensagemErro = 'Adicione Mais Medicamentos ou Finalize :)';
    }
    else if (isValorEmpty(medicamentoId)) {
        mensagemErro = 'Selecione um Medicamento ao Lado';
    }
    else if (isValorEmpty(dosagemMedicamento) || !isDosagemValid()) {
        mensagemErro = 'Digite a Dosagem ao Lado';
    }
    else if (isValorEmpty(periodoMedicamento) || !isPeriodoValid()) {
        mensagemErro = 'Digite o Período ao Lado';
    }
    else if (isValorEmpty(quantidadeMedicamento) || !isQtdMedicamentoValid()) {
        mensagemErro = 'Informe a Quantidade ao Lado :)';
    }
    else if (isDosagemValid() && isPeriodoValid() && isQtdMedicamentoValid()) {
        mensagemErro = 'Agora é Só Adicionar :)';
    }

    optionPacienteInterativa.textContent = mensagemErro;
    console.log(mensagemErro);
}

function atualizarValorOptionMedicamento() {
    let mensagemErro = '';

    if (!medicamentoIdHasChild()) {
        mensagemErro = 'Nenhum Medicamento Encontrado';
    }
    else if (isValorEmpty(medicamentoId)) {
        mensagemErro = 'Selecione um Medicamento';
    }
    else if (isValorEmpty(dosagemMedicamento) || !isDosagemValid()) {
        mensagemErro = 'Digite a Dosagem Abaixo';
    }
    else if (isValorEmpty(periodoMedicamento) || !isPeriodoValid()) {
        mensagemErro = 'Digite o Período Abaixo';
    }
    else if (isValorEmpty(quantidadeMedicamento) || !isQtdMedicamentoValid()) {
        mensagemErro = 'Informe a Quantidade para Finalizar :)';
    }
    else {
        mensagemErro = 'Agora é Só Adicionar :)';
    }

    optionMedicamentoInterativa.textContent = mensagemErro;
    console.log(mensagemErro);
}

if (cRMMedico) {
    cRMMedico.addEventListener('blur', () => {
        isCRMMedicoValid();
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    });
}

if (pacienteId) {
    pacienteId.addEventListener('blur', () => {
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    });
}

if (medicamentoId) {
    medicamentoId.addEventListener('blur', () => {
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    })
}

if (dosagemMedicamento) {
    dosagemMedicamento.addEventListener('blur', () => {
        isDosagemValid();
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    })
}

if (periodoMedicamento) {
    periodoMedicamento.addEventListener('blur', () => {
        isPeriodoValid();
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    })
}

if (quantidadeMedicamento) {
    quantidadeMedicamento.addEventListener('blur', () => {
        isQtdMedicamentoValid();
        atualizarValorOptionPaciente();
        atualizarValorOptionMedicamento();
    })
}

const btnLimpar = document.querySelector('button[type="reset"]');

function limparMensagensErro() {
    cRMMedicoInvalidInput.textContent = '';
    dosagemInvalidInput.textContent = '';
    periodoInvalidInput.textContent = '';
    quantidadeInvalidInput.textContent = '';
}

if (btnLimpar) {
    btnLimpar.addEventListener('click', limparMensagensErro);
}

if (formPrescricao) {
    formPrescricao.addEventListener('submit', (e) => {
        const button = e.submitter;
        if (button && button.getAttribute('formnovalidate') !== null) return;

        if (button.value === 'cadastrar') {
            if (!(temMedicamentos && isSiglaCRMValid() && isCRMMedicoValid())) {
                e.preventDefault();
                alert("Cadastro falhou. Por favor, verifique os campos.");
            }
        }
    });
}

if (formMedicamento) {
    formMedicamento.addEventListener('submit', (e) => {
        const button = e.submitter;
        if (button && button.getAttribute('formnovalidate') !== null) return;

        if (button.value === 'adicionarMedicamento') {
            if (!(isDosagemValid() && isPeriodoValid() && isQtdMedicamentoValid())) {
                e.preventDefault();
                alert("Medicamento inválido. Por favor, verifique os campos.");
            }
        }
    });
}

document.addEventListener('DOMContentLoaded', () => {
    atualizarValorOptionPaciente();
    atualizarValorOptionMedicamento();
})