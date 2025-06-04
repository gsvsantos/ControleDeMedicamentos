const form = document.getElementById('form-paciente');
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cartaoSUS = document.getElementById('cartaoSUS');
const nomeInvalidInput = document.getElementById('nomeInvalidInput');
const telefoneInvalidInput = document.getElementById('telefoneInvalidInput');
const cartaoSUSInvalidInput = document.getElementById('cartaoSUSInvalidInput');

function isNomePacienteValid() {
    let isValid = true;

    if (nome.value.trim() === "") {
        nomeInvalidInput.innerText = 'Nome é obrigatório';
        isValid = false;
    }
    else if (nome.value.length < 3 || nome.value.length > 100) {
        nomeInvalidInput.innerText = 'Nome deve conter entre 3 e 100 caracteres';
        isValid = false;
    }
    else if (nome.value.match(/[^a-zA-ZÀ-ÿ\s]/)) {
        nomeInvalidInput.innerText = 'O nome não pode conter números ou caracters especiais'
        isValid = false;
    }
    else {
        nomeInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}

function isTelefoneValid() {
    let isValid = true;

    if (telefone.value.trim() === "") {
        telefoneInvalidInput.innerText = 'Telefone é obrigatório';
        isValid = false;
    }
    else if (!telefone.value.match(/^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$/)) {
        telefoneInvalidInput.innerText = 'Formato de Telefone Inválido (Ex: (XX) XXXX-XXXX ou XXXXX-XXXX)';
        isValid = false;
    }
    else {
        telefoneInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}

function isCartaoSUSValid() {
    let isValid = true;

    if (cartaoSUS.value.trim() === "") {
        cartaoSUSInvalidInput.innerText = 'Cartão S.U.S. é obrigatório';
        isValid = false;
    }
    else if (!cartaoSUS.value.match(/^\d{3}\.?\d{4}\.?\d{4}\.?\d{4}$/)) {
        cartaoSUSInvalidInput.innerText = 'Formato do Cartão S.U.S. inválido (Ex: 999.9999.9999.9999)';
        isValid = false;
    }
    else {
        cartaoSUSInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}

if (nome)
    nome.addEventListener('blur', isNomePacienteValid);

if (telefone)
    telefone.addEventListener('blur', isTelefoneValid);

if (cartaoSUS)
    cartaoSUS.addEventListener('blur', isCartaoSUSValid);

const btnLimpar = document.querySelector('button[type="reset"]');

function limparMensagensErro() {
    nomeInvalidInput.textContent = '';
    telefoneInvalidInput.textContent = '';
    cartaoSUSInvalidInput.textContent = '';
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

        if (isNomePacienteValid() && isTelefoneValid() && isCartaoSUSValid()) {
            console.log("Formulário validado e enviado!");

            form.submit();
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}