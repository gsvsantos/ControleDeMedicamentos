const acaoAutomaticaTrigger = document.getElementById('acaoAutomaticaTrigger');
const form = document.getElementById('form-medicamento');

const nome = document.getElementById('nome');
const qtdestoque = document.getElementById('qtdestoque');
const descricao = document.getElementById('descricao');

const fornecedorId = document.getElementById('fornecedorId');

function submit(nomeCampo) {
    acaoAutomaticaTrigger.value = nomeCampo;
    form.submit();
}

nome.addEventListener('change', () => submit('informarNome'));
qtdestoque.addEventListener('change', () => submit('informarQuantidade'));
descricao.addEventListener('change', () => submit('informarDescricao'));

fornecedorId.addEventListener('change', () => submit('selecionarFornecedor'));