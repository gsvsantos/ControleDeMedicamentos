const btnSelecaoMultipla = document.getElementById('btnSelecaoMultipla');
const btnExcluirSelecionados = document.getElementById('btnExcluirSelecionados');
const colunaCheckboxes = document.querySelectorAll('.checkbox-table');
const btnCheckboxes = document.querySelectorAll('.item-checkbox');
const btnCbSelectAll = document.getElementById('cb-select-all');
const btnLimpar = document.querySelector('button[type="reset"]');
const modalConfirmacao = document.getElementById('modal-confirmacao');
const btnCloseModal = document.getElementById('btnFecharModal');
const btnCancelar = document.getElementById('btnCancelar');
const btnConfirmar = document.getElementById('btnConfirmar');

let mostrarBotoes = false;

function mostrarBotoesExclusaoMultipla() {
    mostrarBotoes = !mostrarBotoes;

    if (mostrarBotoes) {
        btnCbSelectAll.style.display = 'table-cell';
        btnCbSelectAll.checked = false;

        for (const btnCheckbox of btnCheckboxes) {
            btnCheckbox.style.display = 'block';
        }
        for (const colunaCheckbox of colunaCheckboxes) {
            colunaCheckbox.style.display = 'table-cell';
        }
    }
    else {
        btnCbSelectAll.style.display = 'none';

        for (const btnCheckbox of btnCheckboxes) {
            btnCheckbox.style.display = 'none';
            btnCheckbox.checked = false;
        }
        for (const colunaCheckbox of colunaCheckboxes) {
            colunaCheckbox.style.display = 'none';
        }
    }
}

let selecionarTodosBotoes = false;

function selecionarTodos() {
    selecionarTodosBotoes = !selecionarTodosBotoes;

    if (selecionarTodosBotoes) {
        for (const btnCheckbox of btnCheckboxes) {
            btnCheckbox.checked = true;
        }
    }
    else {
        for (const btnCheckbox of btnCheckboxes) {
            btnCheckbox.checked = false;
        }
    }
}

function mostrarExcluirMultiplo() {
    let algumCheckboxChecked = false;

    for (const btnCheckbox of btnCheckboxes) {
        if (btnCheckbox.checked === true) {
            algumCheckboxChecked = true;
            break;
        }
    }

    if (algumCheckboxChecked) {
        btnExcluirSelecionados.style.display = 'flex';
    } else {
        btnExcluirSelecionados.style.display = 'none';
    }
}

if (btnSelecaoMultipla) {
    btnSelecaoMultipla.addEventListener('click', mostrarBotoesExclusaoMultipla);
}

if (btnCbSelectAll) {
    btnCbSelectAll.addEventListener('click', () => { selecionarTodos(); mostrarExcluirMultiplo() });
}

if (btnCheckboxes) {
    for (const btnCheckbox of btnCheckboxes) {
        btnCheckbox.addEventListener('click', mostrarExcluirMultiplo);
    }
}
function mostrarModal(){
    modalConfirmacao.style.display= 'flex';
}

function esconderModal(){
    modalConfirmacao.style.display= 'none';
}

if (btnExcluirSelecionados){
    btnExcluirSelecionados.addEventListener('click', mostrarModal);
}

if (btnCloseModal){
    btnCloseModal.addEventListener('click', esconderModal)
}

if (btnCancelar){
    btnCancelar.addEventListener('click', esconderModal)
}

function submitar(){
    const idsSelecionados = []; 

    btnCheckboxes.forEach(btnCheckbox => {
        if (btnCheckbox.checked) {
            idsSelecionados.push(btnCheckbox.dataset.id);
        }
    });

    if (idsSelecionados.length === 0) {
        alert('Por favor, selecione ao menos um fornecedor para excluir.');
        esconderModal();
        return; 
    }

    const hrefAtual = window.location.pathname.split('/')[1];
    console.log("hrefAtual: ", hrefAtual)

    console.log("idsSelecionados: ", idsSelecionados)
    const idsJson = JSON.stringify(idsSelecionados);
    console.log("idsJson: ", idsJson)
    const url = `excluirmultiplo`;
    console.log("url: ", url)

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: idsJson
    })
        .then(() => {
            esconderModal();
            mostrarBotoesExclusaoMultipla();
            mostrarExcluirMultiplo();
            idsSelecionados.forEach(id => {
            const row = document.querySelector(`input[data-id="${id}"]`).closest('tr');
            if (row) {
                row.remove();
            }
        });
    });
}

if (btnConfirmar){
    btnConfirmar.addEventListener('click', submitar)
}