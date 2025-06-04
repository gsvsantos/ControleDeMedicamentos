const form = document.getElementById('form-funcionario');
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cPF = document.getElementById('cPF');
const nomeInvalidInput = document.getElementById('nomeInvalidInput');
const telefoneInvalidInput = document.getElementById('telefoneInvalidInput');
const cPFInvalidInput = document.getElementById('cPFInvalidInput');

function isNomeFuncionarioValid() {
    let isValid = true;

    if (nome.value.trim() === "") {
        nomeInvalidInput.textContent = "Nome é obrigatório"
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

function isCPFValid() {
    let isValid = true;

    if (cPF.value.trim() === "") {
        cPFInvalidInput.textContent = 'C.P.F. é obrigatório';
        isValid = false;
    }
    else if (!cPF.value.match(/^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$/)) {
        cPFInvalidInput.textContent = 'Formato de C.P.F. Inválido (Ex: 999.999.999-99)';
        isValid = false;
    }
    else {
        cPFInvalidInput.textContent = '';
        isValid = true
    }

    return isValid;
}

if (nome)
    nome.addEventListener('blur', isNomeFuncionarioValid);

if (telefone)
    telefone.addEventListener('blur', isTelefoneValid);

if (cPF)
    cPF.addEventListener('blur', isCPFValid);

const btnLimpar = document.querySelector('button[type="reset"]');

function limparMensagensErro() {
    nomeInvalidInput.textContent = '';
    telefoneInvalidInput.textContent = '';
    cPFInvalidInput.textContent = '';
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

        if (isNomeFuncionarioValid() && isTelefoneValid() && isCPFValid()) {
            console.log("Formulário validado e enviado!");
            form.submit();
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}