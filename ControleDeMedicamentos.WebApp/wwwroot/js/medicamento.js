const form = document.getElementById('form-medicamento');
const nome = document.getElementById('nome');
const qtdEstoque = document.getElementById('qtdEstoque');
const descricao = document.getElementById('descricao');
const fornecedorId = document.getElementById('fornecedorId');
const nomeInvalidInput = document.getElementById('nomeInvalidInput');
const qtdEstoqueInvalidInput = document.getElementById('qtdEstoqueInvalidInput');
const descricaoInvalidInput = document.getElementById('descricaoInvalidInput')
const optionInterativa = document.getElementById('optionInterativa');

function isNomeMedicamentoValid() {
    let isValid = true;

    if (nome.value.trim() === "") {
        nomeInvalidInput.textContent = 'Nome é obrigatório';
        isValid = false;
    }
    else if (nome.value.length < 3 || nome.value.length > 100) {
        nomeInvalidInput.textContent = 'Nome deve conter entre 3 e 100 caracteres';
        isValid = false;
    }
    else if (nome.value.match(/[^a-zA-ZÀ-ÿ\s]/)) {
        nomeInvalidInput.textContent = 'O nome não pode conter números ou caracters especiais'
        isValid = false;
    }
    else {
        nomeInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isQtdEstoqueValid() {
    let isValid = true;

    if (qtdEstoque.value.trim() === "") {
        qtdEstoqueInvalidInput.textContent = 'Quantidade é obrigatória';
        isValid = false;
    }
    else if (qtdEstoque.value <= 0) {
        qtdEstoqueInvalidInput.textContent = 'A quantidade inicial não pode ser zero ou negativa.';
        isValid = false;
    }
    else {
        qtdEstoqueInvalidInput.textContent = '';
        isValid = true;
    }

    return isValid;
}

function isDescricaoValid() {;
    let isValid = true;

    if (descricao.value.trim() === "") {
        descricaoInvalidInput.textContent = 'Descrição é obrigatória';
        isValid = false;
    }
    else if (descricao.value.length < 5 || descricao.value.length > 255) {
        descricaoInvalidInput.textContent = 'Descrição deve conter entre 5 e 255 caracteres';
        isValid = false;
    }
    else {
        descricaoInvalidInput.textContent = '';
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

function atualizarValorOption() {
    let mensagemErro = '';

    if (isValorEmpty(nome) || !isNomeMedicamentoValid())
    {
        mensagemErro = 'Digite o Nome do Medicamento Acima';
    }
    else if ((qtdEstoque.value == '' || qtdEstoque.value == '0') || !isQtdEstoqueValid())
    {
        mensagemErro = 'Digite a Quantidade a Adicionar Acima';
    }
    else if (isValorEmpty(descricao) || !isDescricaoValid())
    {
        mensagemErro = 'Digite a Descrição do Medicamento Acima';
    }
    else if (fornecedorId.value == '')
    {
        mensagemErro = 'Selecione um Fornecedor';
    }
    else
    {
        mensagemErro = 'Agora é Só Finalizar :)';
    }

    optionInterativa.textContent = mensagemErro;
    console.log(mensagemErro);
}

if (nome) {
    nome.addEventListener('blur', () => {
        isNomeMedicamentoValid();
        atualizarValorOption();
    });
}

if (qtdEstoque) {
    qtdEstoque.addEventListener('blur', () => {
        isQtdEstoqueValid();
        atualizarValorOption();
    });
}

if (descricao) {
    descricao.addEventListener('blur', () => {
        isDescricaoValid();
        atualizarValorOption();
    });
}

if (fornecedorId) {
    fornecedorId.addEventListener('blur', () => {
        atualizarValorOption();
    });
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

        if (isNomeMedicamentoValid() && isQtdEstoqueValid() && isDescricaoValid()) {
            console.log("Formulário validado e enviado!");
            form.submit();
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}

document.addEventListener('DOMContentLoaded', atualizarValorOption)