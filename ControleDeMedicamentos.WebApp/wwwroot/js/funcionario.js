function isNomeValid() {
    const nomeInput = document.getElementById('nome');
    const nomeInvalidInput = document.getElementById('nomeInvalidInput')

    let isValid = true;

    if (nomeInput.value.trim() === "") {
        nomeInvalidInput.innerText = "Nome é obrigatório"
        isValid = false;
    }
    else if (nomeInput.value.length < 3 || nomeInput.value.length > 100) {
        nomeInvalidInput.innerText = 'Nome deve conter entre 3 e 100 caracteres';
        isValid = false;
    }
    else if (nomeInput.value.match(/[^a-zA-ZÀ-ÿ\s]/)) {
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
    const telefoneInput = document.getElementById('telefone');
    const telefoneInvalidInput = document.getElementById('telefoneInvalidInput');

    let isValid = true;

    if (telefoneInput.value.trim() === "") {
        telefoneInvalidInput.innerText = 'Telefone é obrigatório';
        isValid = false;
    }
    else if (!telefoneInput.value.match(/^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$/)) {
        telefoneInvalidInput.innerText = 'Formato de Telefone Inválido (Ex: (XX) XXXX-XXXX ou XXXXX-XXXX)';
        isValid = false;
    }
    else {
        telefoneInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}
function isCPFValid() {
    const cPFInput = document.getElementById('cPF');
    const cPFInvalidInput = document.getElementById('cPFInvalidInput');

    let isValid = true;

    if (cPFInput.value.trim() === "") {
        cPFInvalidInput.innerText = 'C.P.F. é obrigatório';
        isValid = false;
    }
    else if (!cPFInput.value.match(/^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$/)) {
        cPFInvalidInput.innerText = 'Formato de C.P.F. Inválido (Ex: 999.999.999-99)';
        isValid = false;
    }
    else {
        cPFInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}

const form = document.getElementById('form-funcionario');
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cPF = document.getElementById('cPF');

if (nome)
    nome.addEventListener('blur', isNomeValid);
if (telefone)
    telefone.addEventListener('blur', isTelefoneValid);
if (cPF)
    cPF.addEventListener('blur', isCPFValid);

if (form) {
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        if (isNomeValid() && isTelefoneValid() && isCPFValid()) {
            console.log("Formulário validado e enviado!");
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}