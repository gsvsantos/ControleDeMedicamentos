const form = document.getElementById('form-fornecedor');
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cnpj = document.getElementById('cnpj');
const nomeInvalidInput = document.getElementById('nomeInvalidInput');
const telefoneInvalidInput = document.getElementById('telefoneInvalidInput');
const cnpjInvalidInput = document.getElementById('cnpjInvalidInput');

function isNomeFornecedorValid() {

    let isValid = true;

    if (nome.value.trim() === "") {
        nomeInvalidInput.textContent = 'Nome é obrigatório';
        isValid = false;
    }
    else if (nome.value.length < 3 || nome.value.length > 100) {
        nomeInvalidInput.textContent = 'Nome deve conter entre 3 e 100 caracteres';
        isValid = false;
    }
    else {
        nomeInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isTelefoneValid() {
    let isValid = true;

    if (telefone.value.trim() === "") {
        telefoneInvalidInput.textContent = 'Telefone é obrigatório';
        isValid = false;
    }
    else if (!telefone.value.match(/^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$/)) {
        telefoneInvalidInput.textContent = 'Formato de Telefone Inválido (Ex: (XX) XXXX-XXXX ou XXXXX-XXXX)';
        isValid = false;
    }
    else {
        telefoneInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

function isCNPJValid() {
    let isValid = true;

    if (cnpj.value.trim() === "") {
        cnpjInvalidInput.textContent = 'C.N.P.J. é obrigatório';
        isValid = false;
    }
    else if (!cnpj.value.match(/^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$/)) {
        cnpjInvalidInput.textContent = 'Formato de C.N.P.J. Inválido (Ex: 99.999.999/9999-99)';
        isValid = false;
    }
    else {
        cnpjInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

if (nome)
    nome.addEventListener('blur', isNomeFornecedorValid);

if (telefone)
    telefone.addEventListener('blur', isTelefoneValid);

if (cnpj)
    cnpj.addEventListener('blur', isCNPJValid);

const btnLimpar = document.querySelector('button[type="reset"]');

function limparMensagensErro() {
    nomeInvalidInput.textContent = '';
    telefoneInvalidInput.textContent = '';
    cnpjInvalidInput.textContent = '';
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

        if (isNomeFornecedorValid() && isTelefoneValid() && isCNPJValid()) {
            console.log("Formulário validado e enviado!");
            form.submit();
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}