// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import  loadProducts  from "./getFetching.js";


const container = document.getElementById('container');
const buttons = document.querySelectorAll('.tableButtons');
const button1 = document.getElementById('button1');
const span1 = document.getElementById('span1');
let isMenuOpen = false;


const area = document.createElement('div');
area.classList.add('row', 'justify-content-center', 'mt-4', 'd-flex', 'flex-column', 'align-items-center');

const menuArea = document.createElement('div');


const botonMenu = document.createElement('button');
botonMenu.classList.add('btn', 'btn-primary', 'mb-2');
botonMenu.innerText = 'Ver Menú';


const botonVerProductos = document.createElement('button');
botonVerProductos.classList.add('btn');
botonVerProductos.classList.add('btn-primary');
botonVerProductos.innerText = 'Ver Productos Consumidos';



initEventListeners();

function initEventListeners() {

    buttons.forEach((button) => {
        button.addEventListener('click', () => handleTableClick(button))
    })

    //Ver Menú

    botonMenu.addEventListener('click', async () => {
        if (!isMenuOpen) {
            menuArea.innerHTML = '';
            await loadProducts(menuArea);
        }
        else {
            menuArea.innerHTML = '';
        }
        console.log(isMenuOpen)
        isMenuOpen = !isMenuOpen;
    });
}

function handleTableClick(button) {
    const tableState = button.getAttribute('data-state');
    const tableId = button.getAttribute('data-id');
    UpdateTableState(tableState, tableId);
}

function UpdateTableState(tableState, tableId) {
    button1.classList.remove('d-none');
    span1.innerText =
        `Está trabajando sobre la mesa: ${tableId}, 
    esta mesa se encuentra: ${tableState == "False" ? "Disponible" : "Ocupada"}`;

    if (tableState === 'False') {
        button1.innerText = 'Abrir Mesa'
        button1.classList.remove('btn-danger');
        button1.classList.add('btn-success');
        clearUi();
    }
    else {
        button1.innerText = 'Cerrar Mesa';
        button1.classList.remove('btn-success');
        button1.classList.add('btn-danger');
        createArea();
    }
}

function createArea() {
    clearUi();
    area.appendChild(botonMenu);
    area.appendChild(botonVerProductos);
    container.appendChild(area);
    container.appendChild(menuArea);
}

function clearUi() {
    area.innerHTML = '';
    menuArea.innerHTML = '';
} 







