
// const prevBtn = document.getElementById('prevBtn');
// const nextBtn = document.getElementById('nextBtn');
// const currentPage = document.getElementById('currentPage');
import '/js/components/product-card.js';
import { ProductClient } from "../core/client/product-client.js";

// eslint-disable-next-line no-unused-vars
import { Product } from "../core/models/product.js";
import { LoadingIndicator } from "../core/loading-indicator.js";


const contentContainer = document.getElementById("content");
export async function init()
{
    await loadProducts();
}

async function loadProducts() {


    const urlParams = new URLSearchParams(location.search);
    LoadingIndicator.show(contentContainer);
    try
    {
        const client = new ProductClient();
        const productsPage = await client.getProducts(
                                            urlParams.get('q'),
                                            urlParams.get('page'),
                                            urlParams.get('size'));

        renderProducts(productsPage.products)

        console.log(productsPage.size + " products loaded successfully");
        pagination(productsPage.hasMore, productsPage.page);
    } catch (error) {
        console.error("Failed to load products:", error);
    }
}

/**
 *
 * @param {Product[]} products
 */
function renderProducts(products)
{

    const section = document.createElement('section');
    section.id = "products";
    section.innerHTML = products.map((product, index) => {

        const loading = index <= 3 ? 'eager' : 'lazy';

        return `<product-card
            id="${product.id}"
            title="${product.title}"
            brand="${product.brand}"
            thumbnail="${product.thumbnail}"
            rating="${product.rating}"
            inStock="${product.inStock}"
            ${product.hasDiscount ? 'hasDiscount="true"' : ''}
            originalPrice="${product.originalPrice}"
            discountPrice="${product.discountPrice}"
            discountPercentage="${product.discountPercentage}"
            loading="${loading}">
        </product-card>`
    }).join('');

    LoadingIndicator.hide(contentContainer);

    contentContainer.innerHTML = section.outerHTML;
}

function pagination(hasMore, pageNumber) {

    prevBtn.disabled = pageNumber === 1;
    nextBtn.disabled = !hasMore;
    currentPage.textContent = pageNumber;
}
