const form = document.getElementById('form-fornecedor');
 
const nome = document.getElementById('nome');
const telefone = document.getElementById('telefone');
const cnpj = document.getElementById('cnpj')

nome.addEventListener('blur', () => {
    if (nome.value === "")
        document.getElementById('nomeInvalido').innerText = 'Nome é obrigatório';
    else if (nome.value.length < 3 || nome.value.length > 100)
        document.getElementById('nomeInvalido').innerText = 'Nome deve conter entre 3 e 100 caracteres';
    else if (nome.value.match(/[^a-zA-ZÀ-ÿ\s]/))
        document.getElementById('nomeInvalido').innerText = 'O nome não pode conter números ou caracters especiais'
    else
        document.getElementById('nomeInvalido').innerText = '';
})

telefone.addEventListener('blur', () => {
    if (telefone.value === "")
        document.getElementById('telefoneInvalido').innerText = 'Telefone é obrigatório';
    else if (!telefone.value.match(/^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$/))
        document.getElementById('telefoneInvalido').innerText = 'Número de Telefone Inválido\n';
    else
        document.getElementById('telefoneInvalido').innerText = '';
})

cnpj.addEventListener('blur', () => {
    if (cnpj.value === "")
        document.getElementById('cnpjInvalido').innerText = 'C.N.P.J. é obrigatório';
    else if (!cnpj.value.match(/^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$/))
        document.getElementById('cnpjInvalido').innerText = 'C.N.P.J. inválido\n';
    else
        document.getElementById('cnpjInvalido').innerText = '';
})