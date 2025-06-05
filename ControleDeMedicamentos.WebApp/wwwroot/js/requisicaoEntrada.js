const form = document.getElementById('form-rentrada');
const funcionarioId = document.getElementById('funcionarioId');
const medicamentoId = document.getElementById('medicamentoId');
const quantidadeMedicamento = document.getElementById('quantidadeMedicamento')
const quantidadeInvalidInput = document.getElementById('quantidadeInvalidInput');

function funcionarioIdHasChild() {
    let hasChild = false;

    for (let item of funcionarioId.children) {
        if (item.value != '' && !item.hasAttribute('disabled'))
            hasChild = true;
    }

    return hasChild;
}

function medicamentoIdHasChild() {
    let hasChild = false;

    for (let item of medicamentoId.children) {
        if (item.value != '' && !item.hasAttribute('disabled'))
            hasChild = true;
    }

    return hasChild;
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

function isValorEmpty(valor) {
    let isEmpty = false;

    if (valor.value == null || valor.value == '' || valor.value == undefined)
        isEmpty = true;

    return isEmpty;
}

function atualizarValorOptionFuncionario() {
    let mensagemErro = '';

    if (!funcionarioIdHasChild()) {
        mensagemErro = 'Nenhum Funcionário Encontrado';
    }
    else if (isValorEmpty(funcionarioId)) {
        mensagemErro = 'Selecione um Funcionário';
    }
    else if (isValorEmpty(medicamentoId)) {
        mensagemErro = 'Selecione um Medicamento Abaixo';
    }
    else if (isValorEmpty(quantidadeMedicamento) || !isQtdMedicamentoValid()) {
        mensagemErro = 'Informe a Quantidade';
    }
    else {
        mensagemErro = 'Agora é Só Finalizar :)';
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
    else if (isValorEmpty(quantidadeMedicamento) || !isQtdMedicamentoValid()) {
        mensagemErro = 'Informe a Quantidade';
    }
    else if (isValorEmpty(funcionarioId)) {
        mensagemErro = 'Selecione um Funcionário Acima';
    }
    else {
        mensagemErro = 'Agora é Só Finalizar :)';
    }

    optionMedicamentoInterativa.textContent = mensagemErro;
    console.log(mensagemErro);
}

if (funcionarioId) {
    funcionarioId.addEventListener('blur', () => {
        atualizarValorOptionFuncionario();
        atualizarValorOptionMedicamento();
    });
}

if (medicamentoId) {
    medicamentoId.addEventListener('blur', () => {
        atualizarValorOptionFuncionario();
        atualizarValorOptionMedicamento();
    })
}

if (quantidadeMedicamento) {
    quantidadeMedicamento.addEventListener('blur', () => {
        isQtdMedicamentoValid();
        atualizarValorOptionFuncionario();
        atualizarValorOptionMedicamento();
    })
}

const btnLimpar = document.querySelector('button[type="reset"]');

function limparMensagensErro() {
    nomeInvalidInput.textContent = '';
    qtdEstoqueInvalidInput.textContent = '';
    descricaoInvalidInput.textContent = '';
    optionInterativa.textContent = 'Digite o Nome do Medicamento Acima';
}

if (btnLimpar) {
    btnLimpar.addEventListener('click', limparMensagensErro);
}

if (form) {
    form.addEventListener('submit', (e) => {
        const button = e.submitter;

        if (button && button.getAttribute('formnovalidate') !== null) {
            return;
        }

        e.preventDefault();
        if (button.value === 'cadastrar' && isQtdMedicamentoValid) {
            console.log("Formulário validado e enviado!");
            form.submit();
        }
        else {
            console.log("Cadastro falhou. Por favor, verifique os campos.");
            alert("Cadastro falhou. Por favor, verifique os campos.")
        }
    })
}


document.addEventListener('DOMContentLoaded', () => {
    atualizarValorOptionFuncionario();
    atualizarValorOptionMedicamento();
})