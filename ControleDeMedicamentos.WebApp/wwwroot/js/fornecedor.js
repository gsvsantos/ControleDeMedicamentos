function isNomeValid() {
    const nomeInput = document.getElementById('nome');
    const nomeInvalidInput = document.getElementById('nomeInvalidInput');

    let isValid = true;

    if (nomeInput.value.trim() === "") {
        nomeInvalidInput.innerText = 'Nome é obrigatório';
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
function isCNPJValid() {
    const cnpjInput = document.getElementById('cnpj');
    const cnpjInvalidInput = document.getElementById('cnpjInvalidInput');

    let isValid = true;

    if (cnpjInput.value.trim() === "") {
        cnpjInvalidInput.innerText = 'C.N.P.J. é obrigatório';
        isValid = false;
    }
    else if (!cnpjInput.value.match(/^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$/)) {
        cnpjInvalidInput.innerText = 'Formato de C.N.P.J. Inválido (Ex: 99.999.999/9999-99)';
        isValid = false;
    }
    else {
        cnpjInvalidInput.innerText = '';
        isValid = true
    }

    return isValid;
}

const form = document.getElementById('form-fornecedor');
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cnpj = document.getElementById('cnpj');

if (nome)
    nome.addEventListener('blur', isNomeValid);
if (telefone)
    telefone.addEventListener('blur', isTelefoneValid);
if (cnpj)
    cnpj.addEventListener('blur', isCNPJValid);

if (form) {
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        if (isNomeValid() && isTelefoneValid() && isCNPJValid()) {
            console.log("Formulário validado e enviado!");
            form.submit();
        }
        else {
            console.log("Validação falhou. Por favor, verifique os campos.");
            alert("Validação falhou. Por favor, verifique os campos.")
        }
    })
}