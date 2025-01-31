 async function getFetching(url) {
    try {
        const response = await fetch(url);
        const data = await response.json();
        return data;
    }
    catch (error) {
        console.error(error);
    }
}

export default async function loadProducts(container) {
    try {
        const url = new URL('/Dashboard/GetProductos', window.location.origin).href
        const data = await getFetching(url);
        console.log(data);
        const list = document.createElement('ul');
        list.classList.add('list-group', 'mb-3');

        if (!Array.isArray(data) || !data.length) {
            console.log('1. No data found');
            return;
        }

        container.innerHTML = '';

        data.forEach(categoria => {
            if (!categoria.products.length) {
                console.log('2. No products found');
                return;
            }   
            const title = document.createElement('h3');
            title.classList.add('text-center', 'mb-3');
            title.innerText = categoria.category;
            list.appendChild(title);

            categoria.products.forEach(product => {
                const item = document.createElement('li');
                const buttonOrder = document.createElement('button')
                item.appendChild(buttonOrder);
                item.classList.add('list-group-item', 'd-flex', 'justify-content-between', 'align-items-center');
                buttonOrder.innerText = `${product.name}`;
                list.appendChild(item);
            });
        })

        container.appendChild(list);

    } catch (error) {
        console.error(error)
        
    }
}

